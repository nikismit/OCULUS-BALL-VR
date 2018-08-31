using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneAfterTime : MonoBehaviour {

	public string sceneToLoadName;
	public float timeUntilSwitch = 30.0f;
	float timer = 0.0f;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		if(timer >= timeUntilSwitch){
			GameObject.Find("Main Camera").GetComponent<OVRScreenFade>().FadeToNewLevel(sceneToLoadName);
			timer = 0.0f;
		}

	}
}
