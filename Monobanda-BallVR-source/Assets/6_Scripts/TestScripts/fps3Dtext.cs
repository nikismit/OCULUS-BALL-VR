using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps3Dtext : MonoBehaviour {

	float deltaTime;
	[HideInInspector]public bool LoadNextScene = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		float msec = Mathf.Round(deltaTime * 1000.0f);
		float fps = Mathf.Round(1.0f / deltaTime);
		//float recTime = Mathf.Round(GameObject.Find("SIC").GetComponent<AudioSource>().time);
		this.GetComponent<TextMesh>().text = "MS: " + msec + "\nFPS: " + fps + "\nLoad Next Scene: " + LoadNextScene;
	}
}
