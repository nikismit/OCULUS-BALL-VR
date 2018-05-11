using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongWave : MonoBehaviour {

	public bool waveGrowing = false;

	public float secondsToGrow;
	public Vector3 growthMulti;
	public Vector3 origin;
	public  Vector3 goal;
	public float timer = 0.0f;

	// Use this for initialization
	void Start () {
		origin = this.transform.localScale;
		goal.x = this.transform.localScale.x * growthMulti.x;
		goal.y = this.transform.localScale.y * growthMulti.y;
		goal.z = this.transform.localScale.z * growthMulti.z;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(waveGrowing){
			
			if(timer < 1.0f){
				timer += Time.deltaTime/secondsToGrow;
			} else {
				waveGrowing = false;
				timer = 0.0f;
			}
			this.transform.localScale = Vector3.Lerp(origin, goal, timer);
			
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if(other == other.GetComponent<BoxCollider>()){
			if (other.GetComponent<AudioLamp>()){
				if(other.GetComponent<AudioLamp>().enabled == true){
					//print("HIT! -> " + other.gameObject.name);
					other.GetComponent<AudioSource>().Play();
				}
			}
		}
	}

}
