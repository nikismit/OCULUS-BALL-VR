using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGiver : MonoBehaviour {

	public bool pointsForMovement = true;
	public bool pointsForColorChange = true;
	Color startColor;
	Color currentColor;
	ScoreKeeper SK;


	// Use this for initialization
	void Start () {
		SK = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
		startColor = this.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentColor = this.GetComponent<Renderer>().material.color;

		if(currentColor != startColor){
			SK.score += 10;
			startColor = currentColor;
		}
		if(this.GetComponent<Rigidbody>().velocity.x > 1 || this.GetComponent<Rigidbody>().velocity.y > 1 || this.GetComponent<Rigidbody>().velocity.z > 1){
			SK.score += 1;
		}

	}
}
