using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour {

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
	public bool timeReached = false;
	public bool timeAdding = false;

	public bool colorGood = false;

	// Use this for initialization
	void Start () {
		theCompared.GetComponent<Renderer>().material.color = Color.white;
		
		startValues = theCompared.transform.localEulerAngles;
		endValues = startValues + endValues;
		wantedColor = comparedTo.GetComponent<Renderer>().material.GetColor("_EmissionColor");
	}
	
	// Update is called once per frame
	void Update () {
		
		currentColor = BallSpawner._currentColor;

		theCompared.GetComponent<Renderer>().material.SetColor("_EmissionColor", currentColor);

		if((currentColor.r >= wantedColor.r - colorErrorMargin && currentColor.r <= wantedColor.r + colorErrorMargin) && 
			(currentColor.g >= wantedColor.g - colorErrorMargin && currentColor.g <= wantedColor.g + colorErrorMargin) &&
			(currentColor.b >= wantedColor.b - colorErrorMargin && currentColor.b <= wantedColor.b + colorErrorMargin)){
			colorGood = true;
		} else {
			colorGood = false;
		}


		if(colorGood == true && BallSpawner._isSpeaking && timeReached == false){
			timeAdding = true;
			currentTime += Time.deltaTime;
		} else if(currentTime > 0.0f && timeReached == false){
			timeAdding = false;
			currentTime -= Time.deltaTime;
		} else {

		}

		if ( currentTime >= NeededTime ){
			timeReached = true;
		}
		if(timeReached){
			theCompared.GetComponent<Renderer>().material.SetColor("_EmissionColor", wantedColor);
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
