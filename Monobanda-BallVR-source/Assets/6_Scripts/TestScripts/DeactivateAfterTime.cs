using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour {

	public float timeToInactive = 5.0f;
	float timer = 0.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		if(timer >= timeToInactive){
			this.gameObject.SetActive(false);
			timer = 0.0f;
		}
	}
}
