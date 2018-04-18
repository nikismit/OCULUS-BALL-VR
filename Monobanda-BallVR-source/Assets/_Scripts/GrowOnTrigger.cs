using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnTrigger : MonoBehaviour {

	
	public GameObject ObjectToGrow;

	
	public float sizeMultiplier = 2.0f;
	public float growSpeed = 2.0f;

	public float triggerTime = 2.0f;

	float timer = 0.0f;
	bool addingTime = false;

	public bool growing = false;

	Vector3 normalSize = new Vector3 (1,1,1);
	Vector3 wantedSize = new Vector3 (2,2,2);

	public Color wantedColor;
	public float colorErrorMargin = 0.1f;

	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;

	

	// Use this for initialization
	void Start () {
		normalSize = ObjectToGrow.transform.localScale;
		wantedSize = normalSize * sizeMultiplier;
	}
	
	// Update is called once per frame
	void Update () {

		if(addingTime){
			timer += Time.deltaTime;
		}
		if (timer >= triggerTime){
			growing = true;
			timer = 0.0f;
		}

		
		if(growing){
			if(ObjectToGrow.transform.localScale.x < wantedSize.x){
				ObjectToGrow.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growSpeed;
			}
		} else {
			if(ObjectToGrow.transform.localScale.x > normalSize.x){
				ObjectToGrow.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growSpeed;
			}
		}


	}

	private void OnTriggerEnter(Collider other)
	{
		Color otherColor = other.GetComponent<Renderer>().material.color;
		if(otherColor.r <= wantedColor.r + colorErrorMargin && otherColor.r >= wantedColor.r - colorErrorMargin){
			redGood = true;
		} else {
			redGood = false;
		}

		if(otherColor.b <= wantedColor.b + colorErrorMargin && otherColor.b >= wantedColor.b - colorErrorMargin){
			blueGood = true;
		} else {
			blueGood = false;
		}

		if(otherColor.g <= wantedColor.g + colorErrorMargin && otherColor.g >= wantedColor.g - colorErrorMargin){
			greenGood = true;
		} else {
			greenGood = false;
		}

		if(redGood && greenGood && blueGood){
			//print("color is good!");
			addingTime = true;
			timer = 0.0f;
		} else {
			other.GetComponent<DestroyAtZeroVelocity>().startTimer = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		addingTime = false;
		if(other.GetComponent<DestroyAtZeroVelocity>()){
			other.GetComponent<DestroyAtZeroVelocity>().startTimer = false;
			other.GetComponent<DestroyAtZeroVelocity>().lampActive = false;
		}
		timer = 0.0f;
		this.GetComponent<Light>().enabled = false;
	}

	

}
