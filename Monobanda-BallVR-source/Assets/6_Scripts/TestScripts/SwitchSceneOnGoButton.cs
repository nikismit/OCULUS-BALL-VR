using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneOnGoButton : MonoBehaviour {

	public string sceneToLoadName;
    public OVRScreenFade screenfade_Script;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One)){
            //print("Load new level!");
			screenfade_Script.FadeToNewLevel(sceneToLoadName);
		}


	}
}
