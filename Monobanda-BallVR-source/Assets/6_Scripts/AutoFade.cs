﻿// AutoFade.cs
 using UnityEngine;
using UnityEngine.SceneManagement;
 using System.Collections;
 
 public class AutoFade : MonoBehaviour
 {
     private static AutoFade m_Instance = null;
     private Material m_Material = null;
     private string m_LevelName = "";
     private int m_LevelIndex = 0;
     private bool m_Fading = false;

	float unscaledClampedDT()
	{
		return Mathf.Clamp(Time.unscaledDeltaTime ,0.00001f, Time.maximumDeltaTime);
	}
 
     private static AutoFade Instance
     {
         get
         {
             if (m_Instance == null)
             {
                 m_Instance = (new GameObject("AutoFade")).AddComponent<AutoFade>();
             }
             return m_Instance;
         }
     }
     public static bool Fading
     {
         get { return Instance.m_Fading; }
     }
 
     private void Awake()
     {
         DontDestroyOnLoad(this);
         m_Instance = this;
         m_Material = Resources.Load<Material>("Plane_No_zTest");
     }
 
     private void DrawQuad(Color aColor,float aAlpha)
     {
         aColor.a = aAlpha;
         m_Material.SetPass(0);
         GL.PushMatrix();
         GL.LoadOrtho();
         GL.Begin(GL.QUADS);
         GL.Color(aColor);   // moved here, needs to be inside begin/end
         GL.Vertex3(0, 0, -2);
         GL.Vertex3(0, 1, -2);
         GL.Vertex3(1, 1, -2);
         GL.Vertex3(1, 0, -2);
         GL.End();
         GL.PopMatrix();
     }
 
     private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor)
     {
		float t = 0.0f;
		while (t<1.0f)
         {
             yield return new WaitForEndOfFrame();
             t = Mathf.Clamp01(t + unscaledClampedDT() / aFadeOutTime);
             DrawQuad(aColor,t);
         }
         if (m_LevelName != "")
             SceneManager.LoadScene(m_LevelName);
         else
             SceneManager.LoadScene(m_LevelIndex);
         yield return null; // skip first frame
         while (t>0.0f)
         {
             yield return new WaitForEndOfFrame();
             t = Mathf.Clamp01(t - unscaledClampedDT() / aFadeInTime);
             DrawQuad(aColor,t);
			if(t<0.001f){
				Destroy(this.gameObject);
			}
         }
     }
     private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
     {
         m_Fading = true;
         StartCoroutine(Fade(aFadeOutTime, aFadeInTime, aColor));
     }
 
     public static void LoadLevel(string aLevelName,float aFadeOutTime, float aFadeInTime, Color aColor)
     {
         if (Fading) return;
         Instance.m_LevelName = aLevelName;
         Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
     }
     public static void LoadLevel(int aLevelIndex,float aFadeOutTime, float aFadeInTime, Color aColor)
     {
         if (Fading) return;
         Instance.m_LevelName = "";
         Instance.m_LevelIndex = aLevelIndex;
         Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
     }
 }