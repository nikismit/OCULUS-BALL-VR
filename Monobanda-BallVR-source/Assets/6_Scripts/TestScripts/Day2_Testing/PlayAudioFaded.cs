using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioFaded : MonoBehaviour {

	private static PlayAudioFaded m_Instance = null;

	private static PlayAudioFaded Instance
     {
         get
         {
             if (m_Instance == null)
             {
                 m_Instance = (new GameObject("PlayAudioFaded")).AddComponent<PlayAudioFaded>();
             }
             return m_Instance;
         }
     }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private static IEnumerator Fade(AudioSource source, float fadeInTime, float fadeOutTime){

		float t=0.0f;
		source.volume = 0;
		source.Play();
		while(t<1.0f){
			yield return new WaitForEndOfFrame();
			t+=Time.deltaTime/fadeInTime;
			source.volume = t;
			//print(source.name + " -> " + source.volume + " Time: " + Time.time);
		}
		while(t>0.0f){
			yield return new WaitForEndOfFrame();
			t-=Time.deltaTime/fadeOutTime;
			source.volume = Mathf.Clamp01(t);
			//print(source.volume);
		}
		
	}


	public static void FadeInNOut(AudioSource source, float fadeInTime, float fadeOutTime){

		Instance.StartCoroutine(Fade(source,fadeInTime,fadeOutTime));

	}
}
