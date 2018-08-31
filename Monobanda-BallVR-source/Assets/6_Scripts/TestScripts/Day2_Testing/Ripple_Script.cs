using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple_Script : MonoBehaviour {

	public float deathTime = 3.0f;
	float deathTimer = 0.0f;

	// Use this for initialization
	void Start () {
		if(GetComponent<AudioSource>().clip){
			PlayAudioFaded.FadeInNOut(GetComponent<AudioSource>(), GetComponent<AudioSource>().clip.length/2, GetComponent<AudioSource>().clip.length/2);
		}
		//GetComponent<AudioSource>().Play();

	}
	
	// Update is called once per frame
	void Update () {
		deathTimer += Time.deltaTime;
		if (deathTimer >= deathTime){
			Destroy(this.gameObject);
		}
	}
}
