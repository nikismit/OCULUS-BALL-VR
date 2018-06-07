using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour {

	public GameObject[] ObjectsToCheck = new GameObject[1];
	int triggersToCheck = 0;
	int triggersChecked = 0;
	bool timeToMove = false;

	public Vector3 relMove;
	public float moveTime;
	Vector3 startPos;
	Vector3 newPos;


	float moveTimer = 0.0f;

	// Use this for initialization
	void Start () {
		foreach(GameObject g in ObjectsToCheck){
			if(g.GetComponent<lampActivate>()){
				triggersToCheck++;
			}
			if(g.GetComponent<GrowOnTrigger>()){
				triggersToCheck++;
			}
			if(g.GetComponent<MoveOnTrigger>()){
				triggersToCheck++;
			}
			if(g.GetComponent<RotateOnTrigger>()){
				triggersToCheck++;
			}
		}

		startPos = this.transform.position;
		newPos = this.transform.position + relMove;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(timeToMove == false){
			triggersChecked = 0;
			foreach(GameObject g in ObjectsToCheck){
				if(g.GetComponent<lampActivate>()){
					if(g.GetComponent<lampActivate>().lampActive){
						triggersChecked++;
					}
				}
				if(g.GetComponent<GrowOnTrigger>()){
					if(g.GetComponent<GrowOnTrigger>().growing){
						triggersChecked++;
					}
				}
				if(g.GetComponent<MoveOnTrigger>()){
					if(g.GetComponent<MoveOnTrigger>().moving){
						triggersChecked++;
					}
				}
				if(g.GetComponent<RotateOnTrigger>()){
					if(g.GetComponent<RotateOnTrigger>().rotating){
						triggersChecked++;
					}
				}
			}
			//print(triggersChecked);
			if(triggersChecked >= triggersToCheck){
				timeToMove = true;
			}
			triggersChecked = 0;
		}

		if(timeToMove == true){
			moveTimer += (Time.deltaTime / moveTime);
			if(moveTimer < 1.0f){
				this.transform.position = Vector3.Lerp(startPos, newPos, moveTimer);
			}
		}

	}
}
