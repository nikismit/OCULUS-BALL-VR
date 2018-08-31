using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleChecker : MonoBehaviour {

	public int currentFullGrownPuddles = 0;
	public int MaxFullGrownPuddles = 20;
	float timer = 0.0f;
	public float endingTime = 10.0f;

	bool maxPuddlesReached = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currentFullGrownPuddles >= MaxFullGrownPuddles && maxPuddlesReached == false){
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("WaterPuddle")){
				g.GetComponent<Day2_Puddle_Script>().timer = 0;
				g.GetComponent<Day2_Puddle_Script>().startingScale = g.gameObject.transform.localScale;
				g.GetComponent<Day2_Puddle_Script>().endScale = g.gameObject.transform.localScale*10;
			}
			maxPuddlesReached = true;
		}

		if(maxPuddlesReached){
			timer += Time.deltaTime/endingTime;
			if(timer >= 1.0f){
				GameObject.Find("FPSText").GetComponent<fps3Dtext>().LoadNextScene = true;
				this.transform.Find("Water").GetComponent<Renderer>().enabled = true;
				foreach(GameObject g in GameObject.FindGameObjectsWithTag("WaterPuddle")){
					Destroy(g);
				}

			}
			
		}

	}
}
