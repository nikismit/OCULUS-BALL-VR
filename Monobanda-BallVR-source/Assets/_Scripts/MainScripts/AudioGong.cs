using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGong : MonoBehaviour {

	public GongWave wave;
	public bool playGong = false;
	public ParticleSystem partSys;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(playGong){
			wave.waveGrowing = true;
			playGong = false;
		}


	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.GetComponent<DestroyAtZeroVelocity>()){
			playGong = true;
			partSys.Emit(1000);
		}
	}

}
