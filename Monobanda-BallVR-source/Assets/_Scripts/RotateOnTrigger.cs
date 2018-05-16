using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTrigger : MonoBehaviour {


	[System.Serializable]
	public class ObjectToRotate{
		public GameObject RotObject;
		public float delayTime;
		public Vector3 normalRot;
		public Vector3 neededRot;
		public float rotTime;
	}

	public bool childrenToRotate = false;
	public float childRotateDelay = 0.1f;

	public ObjectToRotate[] ObjectsToRotate = new ObjectToRotate[1];
	public Vector3 relativeRotate;
	public float rotateTime = 2.0f;

	public float triggerTime = 2.0f;

	float timer = 0.0f;
	float rotateTimer = 0.0f;
	float fullRotateTime = 0.0f;
	bool addingTime = false;

	[HideInInspector]public bool rotating = false;
	
	public Color wantedColor;
	public float colorErrorMargin = 0.1f;
	public bool destroyWrongBalls = true;

	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;

	

	// Use this for initialization
	void Start () {
		//print(normalRots.Length + " -> " + ObjectsToRotate.Length);

		fullRotateTime = 0.0f;

		if(childrenToRotate){
			ObjectsToRotate = new ObjectToRotate[this.transform.childCount];
			
			int j = this.transform.childCount;
			//print(transform.GetChild(0).gameObject);
			for(int i = 0; i < j; ++i){
				ObjectsToRotate[i] = new ObjectToRotate{
					RotObject = transform.GetChild(i).gameObject,
					delayTime = childRotateDelay
				};
			}
			
		}

		foreach(ObjectToRotate g in ObjectsToRotate){
			g.normalRot = g.RotObject.transform.localEulerAngles;
			g.neededRot.x = g.normalRot.x + relativeRotate.x;
			if(g.neededRot.x < 0){
				g.neededRot.x += 360.0f;
			}
			g.neededRot.y = g.normalRot.y + relativeRotate.y;
			if(g.neededRot.y < 0){
				g.neededRot.y += 360.0f;
			}
			g.neededRot.z = g.normalRot.z + relativeRotate.z;
			if(g.neededRot.z < 0){
				g.neededRot.z += 360.0f;
			}
			fullRotateTime += g.delayTime;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(addingTime){
			timer += Time.deltaTime;
		}
		if (timer >= triggerTime){
			rotating = true;
			timer = 0.0f;
		}

		
		if(rotating){
			if(rotateTimer < fullRotateTime){
				rotateTimer += Time.deltaTime;
			}
			float timeNeeded = 0.0f;
			foreach(ObjectToRotate g in ObjectsToRotate){
				if(rotateTimer >= timeNeeded){
					g.rotTime += Time.deltaTime;
					if(g.rotTime < rotateTime){
						g.RotObject.transform.localEulerAngles += (new Vector3(relativeRotate.x, relativeRotate.y, relativeRotate.z) * Time.deltaTime) / rotateTime;
					}
					
				}
				timeNeeded += g.delayTime;
			}
			timeNeeded = 0.0f;
			
		} else {
			if(rotateTimer>0.0f){
				rotateTimer -= Time.deltaTime;
			}
			float timeNeeded = fullRotateTime;
			foreach(ObjectToRotate g in ObjectsToRotate){
				if(rotateTimer<=timeNeeded){
					if(relativeRotate.x >= 0){
						if(g.RotObject.transform.localEulerAngles.x < g.normalRot.x){
							g.RotObject.transform.localEulerAngles -= (new Vector3(relativeRotate.x, 0, 0) * Time.deltaTime) / rotateTime;
						}
					} else {
						if(g.RotObject.transform.localEulerAngles.x > g.normalRot.x){
							g.RotObject.transform.localEulerAngles -= (new Vector3(relativeRotate.x, 0, 0) * Time.deltaTime) / rotateTime;
						}
					}
					if(relativeRotate.y >= 0){
						if(g.RotObject.transform.localEulerAngles.y < g.normalRot.y){
							g.RotObject.transform.localEulerAngles -= (new Vector3(relativeRotate.y, 0, 0) * Time.deltaTime) / rotateTime;
						}
					} else {
						if(g.RotObject.transform.localEulerAngles.y > g.normalRot.y){
							g.RotObject.transform.localEulerAngles -= (new Vector3(relativeRotate.y, 0, 0) * Time.deltaTime) / rotateTime;
						}
					}
					if(relativeRotate.z >= 0){
						if(g.RotObject.transform.localEulerAngles.z < g.normalRot.z){
							g.RotObject.transform.localEulerAngles -= (new Vector3(relativeRotate.z, 0, 0) * Time.deltaTime) / rotateTime;
						}
					} else {
						if(g.RotObject.transform.localEulerAngles.z > g.normalRot.z){
							g.RotObject.transform.localEulerAngles -= (new Vector3(relativeRotate.z, 0, 0) * Time.deltaTime) / rotateTime;
						}
					}
				}
				timeNeeded-=g.delayTime;
			}
			timeNeeded = fullRotateTime;
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
			if(destroyWrongBalls){
				other.GetComponent<DestroyAtZeroVelocity>().startTimer = true;
			}
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
