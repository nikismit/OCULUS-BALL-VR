using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTime : MonoBehaviour {

	public float TimeSinceSceneStart = 0.0f;
	bool pause = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(pause == false){
			TimeSinceSceneStart += Time.deltaTime;
		}
		if(Input.GetKeyDown("p")){
			pause = !pause;
		}
	}
}
