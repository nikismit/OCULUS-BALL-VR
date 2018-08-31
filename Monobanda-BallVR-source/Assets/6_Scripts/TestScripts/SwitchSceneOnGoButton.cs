using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneOnGoButton : MonoBehaviour {

	public string sceneToLoadName;
<<<<<<< HEAD
=======
    public OVRScreenFade screenfade_Script;
>>>>>>> Niki

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One)){
<<<<<<< HEAD
			GameObject.Find("Main Camera").GetComponent<OVRScreenFade>().FadeToNewLevel(sceneToLoadName);
=======
            print("Load new level!");
			screenfade_Script.FadeToNewLevel(sceneToLoadName);
>>>>>>> Niki
		}


	}
}
