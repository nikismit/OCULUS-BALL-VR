using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2_Puddle_Script : MonoBehaviour {

	public Vector3 startingScale;
	public float toGrowScaleMultiplier;
	[HideInInspector]public Vector3 endScale;
	//Vector3 currentScale;

	public float timeToGrow;
	Color startingColor;
	[HideInInspector]public float timer = 0.0f;
	bool puddleAddedToPool = false;


	// Use this for initialization
	void Start () {
		if(this.GetComponent<AudioSource>()){
			PlayAudioFaded.FadeInNOut(GetComponent<AudioSource>(), GetComponent<AudioSource>().clip.length/2, GetComponent<AudioSource>().clip.length/2);
		}
		this.transform.localScale = startingScale;
		endScale = startingScale*toGrowScaleMultiplier;
		timer = 0.0f;
		if(this.tag == "WaterRipple"){
			startingColor = this.transform.Find("Ripple1").GetComponent<Renderer>().material.GetColor("_TintColor");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime/timeToGrow;

		this.transform.localScale = Vector3.Lerp(startingScale, endScale, timer);

		if(this.tag == "WaterRipple"){
			Color currentColor = this.transform.Find("Ripple1").GetComponent<Renderer>().material.GetColor("_TintColor");
			currentColor.a = startingColor.a-startingColor.a*timer;
			this.transform.Find("Ripple1").GetComponent<Renderer>().material.SetColor("_TintColor", currentColor);
			this.transform.Find("Ripple2").GetComponent<Renderer>().material.SetColor("_TintColor", currentColor);
			this.transform.Find("Ripple3").GetComponent<Renderer>().material.SetColor("_TintColor", currentColor);
			if (timer >= 1.0f){
				Destroy(this.gameObject);
			}
		}
<<<<<<< HEAD
		if(this.tag == "WaterPuddle" && timer>=1.0f && puddleAddedToPool == false){
			GameObject.Find("PuddleChecker").GetComponent<PuddleChecker>().currentFullGrownPuddles +=1;
			puddleAddedToPool = true;
		}
=======
        if (GameObject.Find("PuddleChecker"))
        {
            if (this.tag == "WaterPuddle" && timer >= 1.0f && puddleAddedToPool == false)
            {

                GameObject.Find("PuddleChecker").GetComponent<PuddleChecker>().currentFullGrownPuddles += 1;
                puddleAddedToPool = true;
            }
        }
>>>>>>> Niki

	}
}
