using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampActivate : MonoBehaviour {

	
	public float triggerTime = 2.0f;

	public Color wantedColor;
	public float colorErrorMargin = 0.1f;

	float timer = 0.0f;
	public bool addingTime = false;
	bool redGood = false;
	bool greenGood = false;
	bool blueGood = false;
<<<<<<< HEAD
    
    public bool playBallAudio = false;
	public bool lampLinkedToAudio = false;
    public bool lampStaysOn = false;
    public bool audioPlayed = false;
    public bool needBallSize = false;
    public Vector2 wantedBallsizeMinMax;
    bool sizeGood = false;
=======
	public bool audioPlayed = false;
    public bool audioTriggersLight = true;
>>>>>>> Niki
	private GameObject currentOccupant;

	// Use this for initialization
	void Start () {
		this.GetComponent<Light>().enabled = false;
		wantedColor = this.GetComponent<Light>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(addingTime){
			timer += Time.deltaTime;
		}
<<<<<<< HEAD
        if (currentOccupant)
        {
            if (timer >= triggerTime && addingTime == true)
            {
                if (audioPlayed == false && playBallAudio == true)
                {

                    if (currentOccupant.GetComponent<AudioSource>().clip)
                    {
                        currentOccupant.GetComponent<AudioSource>().Play();
                        audioPlayed = true;
                    }

                }
                currentOccupant.GetComponent<DestroyAtZeroVelocity>().lampActive = true;
                this.GetComponent<Light>().enabled = true;

                timer = 0.0f;
            }
            if (lampLinkedToAudio == true)
            {
                if (currentOccupant != null)
                {
                    if (currentOccupant.GetComponent<AudioSource>())
                    {
                        if (currentOccupant.GetComponent<AudioSource>().isPlaying)
                        {
                            this.GetComponent<Light>().enabled = true;
                        }
                        else
                        {
                            this.GetComponent<Light>().enabled = false;
                        }
=======
		if (timer >= triggerTime){
			if(audioPlayed == false){
				if(currentOccupant.GetComponent<AudioSource>().clip != null){
					currentOccupant.GetComponent<AudioSource>().Play();
					audioPlayed = true;
				}
			}
			currentOccupant.GetComponent<DestroyAtZeroVelocity>().lampActive = true;
			this.GetComponent<Light>().enabled = true;
			
			timer = 0.0f;
		}
        if (audioTriggersLight == true)
        {
            if (currentOccupant != null)
            {
                if (currentOccupant.GetComponent<AudioSource>())
                {
                    if (currentOccupant.GetComponent<AudioSource>().isPlaying)
                    {
                        this.GetComponent<Light>().enabled = true;
                    }
                    else
                    {
                        this.GetComponent<Light>().enabled = false;
>>>>>>> Niki
                    }
                }
            }
        }
		
	}

	private void OnTriggerEnter(Collider other)
	{
		currentOccupant = other.gameObject;
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
			addingTime = true;
			timer = 0.0f;
		} else {
			other.GetComponent<DestroyAtZeroVelocity>().startTimer = true;
		}

	}

	private void OnTriggerExit(Collider other)
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
            this.GetComponent<Light>().enabled = false;
        }
	}

}
