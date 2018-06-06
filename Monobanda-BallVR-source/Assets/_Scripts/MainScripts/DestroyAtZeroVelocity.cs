using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtZeroVelocity : MonoBehaviour {

	private Rigidbody rigbody;
	public float deathTimer = 5.0f;
	public float timer = 0.0f;
	public bool startTimer;
	public bool lampActive = false;
    public float growSize = 0.0f;
	public GameObject audioPlayerPrefab;
	public Collider playerCollider;
	GameObject audioPrefab;
	bool launched = false;

	// Use this for initialization
	void Start () {
		rigbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		//Physics.IgnoreCollision(playerCollider, this.GetComponent<Collider>());

		if(rigbody.velocity.x > 0.0f || rigbody.velocity.y > 0.0f || rigbody.velocity.z > 0.0f && launched == false){
			launched = true;
		}
		
		if((rigbody.velocity == new Vector3(0.0f, 0.0f, 0.0f) && launched == true && lampActive == false) || startTimer == true){
			timer += Time.deltaTime;
		} else {
			timer = 0.0f;
		}

		if(timer >= deathTimer){
			timer = 0.0f;
			audioPlayerPrefab.GetComponent<AudioSource>().clip = this.GetComponent<AudioSource>().clip;
			audioPrefab = Instantiate(audioPlayerPrefab,this.transform.position,this.transform.rotation);
			this.gameObject.SetActive(false);
			startTimer = false;
			lampActive = false;
			//SpawnBalls._currentBallNum -=1;
		}

	}
}
