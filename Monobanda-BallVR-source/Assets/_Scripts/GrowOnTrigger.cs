using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnTrigger : MonoBehaviour {

	[System.Serializable]
	public class ObjectToGrow{
		public GameObject GrowObject;
		public float delayTime;
		[HideInInspector] public Vector3 normalSize;
		[HideInInspector] public Vector3 neededSize;
	}

	public bool childrenToGrow = false;
	public float childGrowDelay = 0.1f;

	public ObjectToGrow[] ObjectsToGrow = new ObjectToGrow[1];
	public Vector3 sizeMultiplier;
	public float growTime = 2.0f;

	public float triggerTime = 2.0f;

	float timer = 0.0f;
	float growTimer = 0.0f;
	float fullGrowTime = 0.0f;
	bool addingTime = false;

	[HideInInspector]public bool growing = false;
	
	public Color wantedColor;
	public float colorErrorMargin = 0.1f;
	public bool destroyWrongBalls = true;

	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;

	

	// Use this for initialization
	void Start () {
		//print(normalSizes.Length + " -> " + ObjectsToGrow.Length);

		fullGrowTime = 0.0f;

		if(childrenToGrow){
			ObjectsToGrow = new ObjectToGrow[this.transform.childCount];
			
			int j = this.transform.childCount;
			//print(transform.GetChild(0).gameObject);
			for(int i = 0; i < j; ++i){
				ObjectsToGrow[i] = new ObjectToGrow{
					GrowObject = transform.GetChild(i).gameObject,
					delayTime = childGrowDelay
				};
			}
			
		}

		foreach(ObjectToGrow g in ObjectsToGrow){
			g.normalSize = g.GrowObject.transform.localScale;
			g.neededSize.x = g.normalSize.x * sizeMultiplier.x;
			g.neededSize.y = g.normalSize.y * sizeMultiplier.y;
			g.neededSize.z = g.normalSize.z * sizeMultiplier.z;
			fullGrowTime += g.delayTime;
		}
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
			if(growTimer < fullGrowTime){
				growTimer += Time.deltaTime;
			}
			float timeNeeded = 0.0f;
			foreach(ObjectToGrow g in ObjectsToGrow){
				if(growTimer >= timeNeeded){
					if(g.GrowObject.transform.localScale.x < g.neededSize.x){
						g.GrowObject.transform.localScale += (new Vector3(g.neededSize.x, 0, 0) * Time.deltaTime) / growTime;
					}
					if(g.GrowObject.transform.localScale.y < g.neededSize.y){
						g.GrowObject.transform.localScale += (new Vector3(0, g.neededSize.y, 0) * Time.deltaTime) / growTime;
					}
					if(g.GrowObject.transform.localScale.z < g.neededSize.z){
						g.GrowObject.transform.localScale += (new Vector3(0, 0, g.neededSize.z) * Time.deltaTime) / growTime;
					}
				}
				timeNeeded += g.delayTime;
			}
			timeNeeded = 0.0f;
			
		} else {
			if(growTimer>0.0f){
				growTimer -= Time.deltaTime;
			}
			float timeNeeded = fullGrowTime;
			foreach(ObjectToGrow g in ObjectsToGrow){
				if(growTimer<=timeNeeded){
					if(g.GrowObject.transform.localScale.x > g.normalSize.x){
						g.GrowObject.transform.localScale -= (new Vector3(1.0f, 0, 0) * Time.deltaTime) / growTime;
					}
					if(g.GrowObject.transform.localScale.y > g.normalSize.y){
						g.GrowObject.transform.localScale -= (new Vector3(0, 1.0f, 0) * Time.deltaTime) / growTime;
					}
					if(g.GrowObject.transform.localScale.z > g.normalSize.z){
						g.GrowObject.transform.localScale -= (new Vector3(0, 0, 1.0f) * Time.deltaTime) / growTime;
					}
				}
				timeNeeded-=g.delayTime;
			}
			timeNeeded = fullGrowTime;
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
