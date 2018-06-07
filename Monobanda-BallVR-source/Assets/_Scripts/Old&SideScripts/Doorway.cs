using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour {

	public GameObject[] ObjectsToCheck = new GameObject[1];
	int triggersToCheck = 0;
	int triggersChecked = 0;
	bool portAppear = false;

	public Vector3 relPortMove;
	public float portMoveTime;
	Vector3 startPos;
	Vector3 newPos;


	float moveTimer = 0.0f;

	public SpawnBalls BallSpawner;
	public GameObject theCompared;
	public GameObject comparedTo;
	public float colorErrorMargin = 0.1f;
	
	Vector3 startValues;
	float currentValuePointer;
	public Vector3 endValues;

	public Color currentColor;
	Color wantedColor;
	
	float currentTime = 0.0f;
	public float NeededTime = 5.0f;
	bool timeReached = false;
	bool timeAdding = false;

	bool colorGood = false;

	bool movePlayer = false;
	bool playerOriginUpdated = false;
	float movePlayerTimer = 0.0f;
	Vector3 playerOrigin;
	Vector3 newPlayerPos;
	public GameObject player;

	public string nameSceneToLoad;

	

	// Use this for initialization
	void Start () {
		theCompared.GetComponent<Renderer>().material.color = Color.white;
		
		startValues = theCompared.transform.localEulerAngles;
		endValues = startValues + endValues;
		wantedColor = comparedTo.GetComponent<Renderer>().material.GetColor("_EmissionColor");

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
		newPos = this.transform.position + relPortMove;

		playerOrigin = player.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		
		if(portAppear == false){
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
				portAppear = true;
			}
			triggersChecked = 0;
		}

		if(portAppear == true){
			
			moveTimer += (Time.deltaTime / portMoveTime);
			if(moveTimer < 1.0f){
				this.transform.position = Vector3.Lerp(startPos, newPos, moveTimer);
			} else {

				float _micPitch =  BallSpawner._micPitch;

				bool belowMid = true;
				float lowMid = 1.0f;
				float midHigh = 0.0f;

				if(_micPitch <=0.5f){
					belowMid = true;
					lowMid = _micPitch*2;
					midHigh = 0;
				} else if (_micPitch > 0.5f){
					belowMid = false;
					midHigh = (_micPitch - 0.5f)*2;
					lowMid = 0;
				}
				if(belowMid){
					currentColor = Color.Lerp(BallSpawner.lowPitchColor, BallSpawner.midPitchColor, Mathf.Clamp01(lowMid));
				} else {
					currentColor = Color.Lerp(BallSpawner.midPitchColor, BallSpawner.highPitchColor, Mathf.Clamp01(midHigh));
				}


				BallSpawner.spawningBalls = false;

				theCompared.GetComponent<Renderer>().material.SetColor("_EmissionColor", currentColor);

				if((currentColor.r >= wantedColor.r - colorErrorMargin && currentColor.r <= wantedColor.r + colorErrorMargin) && 
					(currentColor.g >= wantedColor.g - colorErrorMargin && currentColor.g <= wantedColor.g + colorErrorMargin) &&
					(currentColor.b >= wantedColor.b - colorErrorMargin && currentColor.b <= wantedColor.b + colorErrorMargin)){
					colorGood = true;
				} else {
					colorGood = false;
				}


				if(colorGood == true && timeReached == false && VoiceProfile._amplitudeCurrent >= VoiceProfile._amplitudeSilence){
					timeAdding = true;
					currentTime += Time.deltaTime;
				} else if(currentTime > 0.0f && timeReached == false){
					timeAdding = false;
					currentTime -= Time.deltaTime;
				}

				if ( currentTime >= NeededTime ){
					timeReached = true;
				}
				if(timeReached){
					theCompared.GetComponent<Renderer>().material.SetColor("_EmissionColor", wantedColor);
					movePlayer = true;
					if(playerOriginUpdated == false){
						playerOrigin = player.transform.position;
						playerOriginUpdated = true;
					}
				}

				currentValuePointer = currentTime/NeededTime;
				Vector3 currentValue = theCompared.transform.localEulerAngles;
				Vector3 currentValue2 = comparedTo.transform.localEulerAngles;
				currentValue.y = Mathf.Lerp(startValues.y, endValues.y, currentValuePointer);
				currentValue2.y = -Mathf.Lerp(startValues.y, endValues.y, currentValuePointer);
				theCompared.transform.localEulerAngles = currentValue;
				comparedTo.transform.localEulerAngles = currentValue2;
			}
		}

		if(movePlayer == true){
			movePlayerTimer += Time.deltaTime/5;
			if(movePlayerTimer < 1.5f){
				newPlayerPos = newPos;
				newPlayerPos.y = playerOrigin.y;
				player.transform.position = Vector3.Lerp(playerOrigin, newPlayerPos, Mathf.Clamp01(movePlayerTimer));
			} else {
				AutoFade.LoadLevel("Level2", 3,3,Color.white);
			}


		}

	}
}
