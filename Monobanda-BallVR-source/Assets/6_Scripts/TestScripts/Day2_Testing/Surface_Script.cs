using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface_Script : MonoBehaviour {

	public GameObject puddle;
	public GameObject ripple;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "WaterBall"){
			if(other.transform.position.y <= this.transform.position.y){
				GameObject currentPuddle = GameObject.Instantiate(puddle, new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z), puddle.transform.rotation);
				GameObject currentRipple = GameObject.Instantiate(ripple, new Vector3(other.transform.position.x, this.transform.position.y, other.transform.position.z), ripple.transform.rotation);
				currentPuddle.GetComponent<AudioSource>().clip = other.transform.parent.gameObject.GetComponent<AudioSource>().clip;
				currentPuddle.GetComponent<Day2_Puddle_Script>().startingScale = other.transform.parent.localScale;
				currentRipple.GetComponent<Day2_Puddle_Script>().startingScale = other.transform.parent.localScale;
				other.transform.parent.gameObject.SetActive(false);
			}
		}
	}

}
