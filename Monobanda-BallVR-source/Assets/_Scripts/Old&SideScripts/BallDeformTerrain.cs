using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeformTerrain : MonoBehaviour {

	public float meltingSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other)
	{
		//print(other.gameObject.tag);
		if(other.gameObject.tag == "Terrain"){
			//this.GetComponent<Rigidbody>().AddForce(100,0,0);
			this.transform.localScale -= new Vector3(Time.deltaTime*meltingSpeed,Time.deltaTime*meltingSpeed,Time.deltaTime*meltingSpeed);
			if(this.transform.localScale.x <= 0.1f){
				this.gameObject.SetActive(false);
			}
			//print("LOWER!");
			other.gameObject.GetComponent<TerrainDeformer>().LowerTerrain(this.gameObject.transform.position, this.gameObject.transform.localScale);
			//this.gameObject.SetActive(false);
		}
	}

	
}
