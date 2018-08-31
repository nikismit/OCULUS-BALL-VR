using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsource_Script : MonoBehaviour {

    public float deathAfterLifeTime = 5.0f;
    float lifetime = 0.0f;

	// Use this for initialization
	void Start () {
        lifetime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        lifetime += Time.deltaTime;

        if (lifetime > deathAfterLifeTime)
        {
            Destroy(this.gameObject);
        }
	}
}
