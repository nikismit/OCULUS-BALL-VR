using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnTrigger : MonoBehaviour {

	
	public GameObject[] ObjectsToGrow = new GameObject[1];

	
	public Vector3 wantedSizes = new Vector3 (2,2,2);
	public float growSpeed = 2.0f;

	public float triggerTime = 2.0f;

	float timer = 0.0f;
	bool addingTime = false;

	public bool growing = false;

	Vector3[] normalSizes = new Vector3[1];
	Vector3[] neededSizes = new Vector3[1];
	

	public Color wantedColor;
	public float colorErrorMargin = 0.1f;

	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;

	

	// Use this for initialization
	void Start () {
		normalSizes = new Vector3[ObjectsToGrow.Length];
		neededSizes = new Vector3[ObjectsToGrow.Length];
		//print(normalSizes.Length + " -> " + ObjectsToGrow.Length);
		int i = 0;
		foreach(GameObject g in ObjectsToGrow){
			normalSizes[i] = g.transform.localScale;
			neededSizes[i].x = normalSizes[i].x * wantedSizes.x;
			neededSizes[i].y = normalSizes[i].y * wantedSizes.y;
			neededSizes[i].z = normalSizes[i].z * wantedSizes.z;
			i++;
		}
		i=0;
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
			int j = 0;
			foreach(GameObject g in ObjectsToGrow){
				if(g.transform.localScale.x < neededSizes[j].x){
					g.transform.localScale += new Vector3(1, 0, 0) * Time.deltaTime * growSpeed;
				}
				if(g.transform.localScale.y < neededSizes[j].y){
					g.transform.localScale += new Vector3(0, 1, 0) * Time.deltaTime * growSpeed;
				}
				if(g.transform.localScale.z < neededSizes[j].z){
					g.transform.localScale += new Vector3(0, 0, 1) * Time.deltaTime * growSpeed;
				}
				j++;
			}
			j=0;
		} else {
			int j = 0;
			foreach(GameObject g in ObjectsToGrow){
				if(g.transform.localScale.x > normalSizes[j].x){
					g.transform.localScale += new Vector3(1, 0, 0) * Time.deltaTime * growSpeed;
				}
				if(g.transform.localScale.y > normalSizes[j].y){
					g.transform.localScale += new Vector3(0, 1, 0) * Time.deltaTime * growSpeed;
				}
				if(g.transform.localScale.z > normalSizes[j].z){
					g.transform.localScale += new Vector3(0, 0, 1) * Time.deltaTime * growSpeed;
				}
				j++;
			}
			j=0;
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
	}

	

}
