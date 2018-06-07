using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBalls : MonoBehaviour {

	public float radius = 1.0f;
	public float power = 2.0f;
	public bool explodeOnContact = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		//print(power + " --> " + radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
			if(hit.GetComponent<Renderer>()){
				hit.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
			}

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
		this.GetComponent<DestroyAtZeroVelocity>().startTimer = true;
		this.GetComponent<DestroyAtZeroVelocity>().timer = this.GetComponent<DestroyAtZeroVelocity>().deathTimer;
	}
}
