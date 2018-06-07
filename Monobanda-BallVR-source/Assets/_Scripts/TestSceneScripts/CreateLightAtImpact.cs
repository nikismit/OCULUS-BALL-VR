using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLightAtImpact : MonoBehaviour {

	public GameObject lightPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		//GameObject currentLight = 
		Instantiate(lightPrefab, collision.gameObject.transform.position, Quaternion.identity);
		//print(collision.gameObject.name);
		if(collision.gameObject.GetComponent<DestroyAtZeroVelocity>()){
			collision.gameObject.GetComponent<DestroyAtZeroVelocity>().startTimer = true;
		}
	}

	

}
