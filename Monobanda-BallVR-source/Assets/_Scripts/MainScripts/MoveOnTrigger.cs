using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrigger : MonoBehaviour {

	[System.Serializable]
	public class ObjectToMove{
		public GameObject MoveObject;
		public float delayTime;
		[HideInInspector]public Vector3 normalPos;
		[HideInInspector]public Vector3 neededPos;
	}

	public bool childrenToMove = false;
	public float childMoveDelay = 0.1f;

	public ObjectToMove[] ObjectsToMove = new ObjectToMove[1];
	public Vector3 relativeMove;
	public float moveTime = 2.0f;

	public float triggerTime = 2.0f;

	float timer = 0.0f;
	float moveTimer = 0.0f;
	float fullMoveTime = 0.0f;
	bool addingTime = false;

	[HideInInspector]public bool moving = false;
	
	public Color wantedColor;
	public float colorErrorMargin = 0.1f;
	public bool destroyWrongBalls = true;

	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;

	

	// Use this for initialization
	void Start () {
		//print(normalPoss.Length + " -> " + ObjectsToMove.Length);

		fullMoveTime = 0.0f;

		if(childrenToMove){
			ObjectsToMove = new ObjectToMove[this.transform.childCount];
			
			int j = this.transform.childCount;
			//print(transform.GetChild(0).gameObject);
			for(int i = 0; i < j; ++i){
				ObjectsToMove[i] = new ObjectToMove{
					MoveObject = transform.GetChild(i).gameObject,
					delayTime = childMoveDelay
				};
			}
			
		}

		foreach(ObjectToMove g in ObjectsToMove){
			g.normalPos = g.MoveObject.transform.localPosition;
			g.neededPos.x = g.normalPos.x + relativeMove.x;
			g.neededPos.y = g.normalPos.y + relativeMove.y;
			g.neededPos.z = g.normalPos.z + relativeMove.z;
			fullMoveTime += g.delayTime;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(addingTime){
			timer += Time.deltaTime;
		}
		if (timer >= triggerTime){
			moving = true;
			timer = 0.0f;
		}

		
		if(moving){
			if(moveTimer < fullMoveTime){
				moveTimer += Time.deltaTime;
			}
			float timeNeeded = 0.0f;
			foreach(ObjectToMove g in ObjectsToMove){
				if(moveTimer >= timeNeeded){
					if(relativeMove.x >= 0){
						if(g.MoveObject.transform.localPosition.x < g.neededPos.x){
							g.MoveObject.transform.localPosition += (new Vector3(relativeMove.x, 0, 0) * Time.deltaTime) / moveTime;
						}
					} else {
						if(g.MoveObject.transform.localPosition.x > g.neededPos.x){
							g.MoveObject.transform.localPosition += (new Vector3(relativeMove.x, 0, 0) * Time.deltaTime) / moveTime;
						}
					}
					if(relativeMove.y >= 0){
						if(g.MoveObject.transform.localPosition.y < g.neededPos.y){
							g.MoveObject.transform.localPosition += (new Vector3(0, relativeMove.y, 0) * Time.deltaTime) / moveTime;
						}
					} else {
						if(g.MoveObject.transform.localPosition.y > g.neededPos.y){
							g.MoveObject.transform.localPosition += (new Vector3(0, relativeMove.y, 0) * Time.deltaTime) / moveTime;
						}
					}
					if(relativeMove.z >= 0){
						if(g.MoveObject.transform.localPosition.z < g.neededPos.z){
							g.MoveObject.transform.localPosition += (new Vector3(0,0, relativeMove.z) * Time.deltaTime) / moveTime;
						}
					} else {
						if(g.MoveObject.transform.localPosition.z > g.neededPos.z){
							g.MoveObject.transform.localPosition += (new Vector3(0,0, relativeMove.z) * Time.deltaTime) / moveTime;
						}
					}
				}
				timeNeeded += g.delayTime;
			}
			timeNeeded = 0.0f;
			
		} else {
			if(moveTimer>0.0f){
				moveTimer -= Time.deltaTime;
			}
			float timeNeeded = fullMoveTime;
			foreach(ObjectToMove g in ObjectsToMove){
				if(moveTimer<=timeNeeded){
					if(relativeMove.x >= 0){
						if(g.MoveObject.transform.localPosition.x < g.normalPos.x){
							g.MoveObject.transform.localPosition -= (new Vector3(relativeMove.x, 0, 0) * Time.deltaTime) / moveTime;
						}
					} else {
						if(g.MoveObject.transform.localPosition.x > g.normalPos.x){
							g.MoveObject.transform.localPosition -= (new Vector3(relativeMove.x, 0, 0) * Time.deltaTime) / moveTime;
						}
					}
					if(relativeMove.y >= 0){
						if(g.MoveObject.transform.localPosition.y < g.normalPos.y){
							g.MoveObject.transform.localPosition -= (new Vector3(relativeMove.y, 0, 0) * Time.deltaTime) / moveTime;
						}
					} else {
						if(g.MoveObject.transform.localPosition.y > g.normalPos.y){
							g.MoveObject.transform.localPosition -= (new Vector3(relativeMove.y, 0, 0) * Time.deltaTime) / moveTime;
						}
					}
					if(relativeMove.z >= 0){
						if(g.MoveObject.transform.localPosition.z < g.normalPos.z){
							g.MoveObject.transform.localPosition -= (new Vector3(relativeMove.z, 0, 0) * Time.deltaTime) / moveTime;
						}
					} else {
						if(g.MoveObject.transform.localPosition.z > g.normalPos.z){
							g.MoveObject.transform.localPosition -= (new Vector3(relativeMove.z, 0, 0) * Time.deltaTime) / moveTime;
						}
					}
				}
				timeNeeded-=g.delayTime;
			}
			timeNeeded = fullMoveTime;
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
