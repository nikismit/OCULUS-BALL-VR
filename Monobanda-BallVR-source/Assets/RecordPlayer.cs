using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayer : MonoBehaviour {

	public float secondsToMove;
	public Vector3 movement;
	private Vector3 origin;
	private Vector3 goal;
	float timer = 0.0f;

	// Use this for initialization
	void Start () {
		origin = this.transform.position;
		goal = this.transform.position + movement;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(timer < secondsToMove){
			timer += Time.deltaTime;

		} else {
			timer = 0.0f;
		}

		this.transform.position = Vector3.Lerp(origin, goal, timer/secondsToMove);

	}

	private void OnTriggerEnter(Collider other)
	{
		
		if(other == other.GetComponent<BoxCollider>()){
			if (other.GetComponent<DestroyAtZeroVelocity>()){
				if(other.GetComponent<DestroyAtZeroVelocity>().lampActive == true){
					other.GetComponent<AudioSource>().Play();
				}
			}
		}
	}
}
