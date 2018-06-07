using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampActivate : MonoBehaviour {

	public Light[] lightsToActivate = new Light[1];
	
	public Color wantedColor;
	public float colorErrorMargin = 0.1f;

	public bool needBallSize = false;
    public Vector2 wantedBallsizeMinMax;

	public float triggerTime = 2.0f;

	float timer = 0.0f;
	bool addingTime = false;
	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;

    
    public bool playBallAudio = false;
	public bool lampLinkedToAudio = false;
    public bool lampStaysOn = false;
	[HideInInspector]public bool audioPlayed = false;
    
    bool sizeGood = false;

	public bool updateAudio = false;
	public bool destroyWrongBalls = true;
	bool audioUpdated = false;
	
	[HideInInspector]public bool lampActive = false;

	private GameObject currentOccupant;

	// Use this for initialization
	void Start () {
		foreach(Light l in lightsToActivate){
			l.enabled = false;
		}
		audioUpdated = false;
		//wantedColor = this.GetComponent<Light>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(addingTime){
			timer += Time.deltaTime;
		}

        if (currentOccupant)
        {
            if (timer >= triggerTime && addingTime == true)
            {
				lampActive = true;
				if (audioPlayed == false && playBallAudio == true)
                {

                    if (currentOccupant.GetComponent<AudioSource>().clip)
                    {
						if(audioUpdated == false){
							foreach (Light l in lightsToActivate){
								l.GetComponent<AudioSource>().clip = currentOccupant.GetComponent<AudioSource>().clip;
								PlayAudioFaded.FadeInNOut(l.GetComponent<AudioSource>(), l.GetComponent<AudioSource>().clip.length/2, l.GetComponent<AudioSource>().clip.length/2);
								//l.GetComponent<AudioSource>().Play();
							}
						}
						audioUpdated = true;
                        audioPlayed = true;
                    }

                }
                currentOccupant.GetComponent<DestroyAtZeroVelocity>().lampActive = true;
                foreach(Light l in lightsToActivate){
					l.enabled = true;
					l.GetComponent<AudioLamp>().enabled = true;
				}

                timer = 0.0f;
            }
            if (lampLinkedToAudio == true)
            {
				foreach(Light l in lightsToActivate){
					if(l.GetComponent<AudioSource>().isPlaying){
						l.enabled = true;
					} else {
						l.enabled = false;
					}

				}
			}
		}
		
		
	}


	void OnTriggerEnter(Collider other)
	{
		
		Color otherColor = other.GetComponent<Renderer>().material.color;
		if(otherColor.r <= wantedColor.r + colorErrorMargin && otherColor.r >= wantedColor.r - colorErrorMargin){
			redGood = true;
		} else {
			redGood = false;
		}

		if(otherColor.b <= wantedColor.b + colorErrorMargin && otherColor.b >= wantedColor.b - colorErrorMargin){
			blueGood = true;
		} else {
			blueGood = false;
		}

		if(otherColor.g <= wantedColor.g + colorErrorMargin && otherColor.g >= wantedColor.g - colorErrorMargin){
			greenGood = true;
		} else {
			greenGood = false;
		}

        if(needBallSize == true)
        {
            if(other.transform.localScale.x >= wantedBallsizeMinMax.x && other.transform.localScale.x <= wantedBallsizeMinMax.y)
            {
                sizeGood = true;
            }
            else
            {
                sizeGood = false;
            }
        }
        else
        {
            sizeGood = true;
        }

		if(redGood && greenGood && blueGood && sizeGood){
			currentOccupant = other.gameObject;
			addingTime = true;
			timer = 0.0f;
			if(updateAudio == true){
				audioUpdated = false;
			}
		} else {
			if(destroyWrongBalls){
				other.GetComponent<DestroyAtZeroVelocity>().startTimer = true;
			}
		}

	}

	void OnTriggerExit(Collider other)
	{
		addingTime = false;
		if(other.GetComponent<DestroyAtZeroVelocity>()){
				other.GetComponent<DestroyAtZeroVelocity>().startTimer = false;
				other.GetComponent<DestroyAtZeroVelocity>().lampActive = false;
		}
		
		audioPlayed = false;
		timer = 0.0f;
        if (lampStaysOn == false)
        {
            foreach(Light l in lightsToActivate){
				l.enabled = false;
                if (l.GetComponent<AudioLamp>())
                {
                    l.GetComponent<AudioLamp>().enabled = false;
                }
			}
        }
	}

}
