using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudioPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(this.GetComponent<AudioSource>().isPlaying == false){
			Destroy(this.gameObject);
		}

	}
}
