using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneOnGoButton : MonoBehaviour {

	public string sceneToLoadName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One)){
			GameObject.Find("Main Camera").GetComponent<OVRScreenFade>().FadeToNewLevel(sceneToLoadName);
		}


	}
}
