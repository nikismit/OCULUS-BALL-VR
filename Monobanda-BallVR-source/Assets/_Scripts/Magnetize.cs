using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetize : MonoBehaviour {

	GameObject target;
	GameObject toMagnetize;
	Color neededColor;
	public float magnetPower = 10;
	public float minDistance = 1.0f;
	bool magnetize;
	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;
	float colorErrorMargin = 0.3f;

	// Use this for initialization
	void Start () {
		target = this.transform.parent.gameObject;
		neededColor = target.GetComponent<lampActivate>().wantedColor;
		colorErrorMargin = target.GetComponent<lampActivate>().colorErrorMargin;
	}
	
	// Update is called once per frame
	void Update () {

		if(magnetize == true){
			if(Vector3.Distance(this.transform.position, toMagnetize.transform.position) >= minDistance){
				Quaternion prevRot = toMagnetize.transform.rotation;
				toMagnetize.transform.LookAt(this.transform.position);
				toMagnetize.GetComponent<Rigidbody>().AddForce(toMagnetize.transform.forward * magnetPower * Time.deltaTime);
				toMagnetize.transform.rotation = prevRot;
			}
		}
		
		if(target.GetComponent<lampActivate>().audioPlayed == true){
			magnetize = false;
			toMagnetize = null;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		Color otherColor = other.GetComponent<Renderer>().material.color;
		if(otherColor.r <= neededColor.r + colorErrorMargin && otherColor.r >= neededColor.r - colorErrorMargin){
			redGood = true;
		} else {
			redGood = false;
		}

		if(otherColor.b <= neededColor.b + colorErrorMargin && otherColor.b >= neededColor.b - colorErrorMargin){
			blueGood = true;
		} else {
			blueGood = false;
		}

		if(otherColor.g <= neededColor.g + colorErrorMargin && otherColor.g >= neededColor.g - colorErrorMargin){
			greenGood = true;
		} else {
			greenGood = false;
		}

		if(redGood && greenGood && blueGood){
			magnetize = true;
			toMagnetize = other.gameObject;
		} else {
			magnetize = false;
			toMagnetize = null;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		magnetize = false;
		toMagnetize = null;
	}
}
