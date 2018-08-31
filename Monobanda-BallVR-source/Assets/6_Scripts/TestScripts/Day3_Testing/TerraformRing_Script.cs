using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraformRing_Script : MonoBehaviour {

	public float moveSpeed = 5;
	public float pitch = 1.0f;
	public Color currentColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(this.transform.forward*Time.deltaTime*moveSpeed, Space.World);
		pitch = Mathf.Clamp01(pitch);
		this.transform.Find("Ring").GetComponent<Renderer>().material.SetColor("_TintColor", currentColor);
	}
}
