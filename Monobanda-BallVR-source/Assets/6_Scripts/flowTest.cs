using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowTest : MonoBehaviour {

	float timeC = 0;
	public float speed, width, height;

	// Use this for initialization
	void Start () {
		//speed = 1;
		//width = 5;
		//height = 5;
	}

	// Update is called once per frame
	void Update () {
		timeC += Time.deltaTime*speed;

		float x = Mathf.Cos(timeC)*width;
		float y = Mathf.Sin(timeC)*height;
		float z = 0;

		transform.position = new Vector3(x,y,z);

	}
}
