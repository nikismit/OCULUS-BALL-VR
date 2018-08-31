using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

    [Header("Target List")]
    public Transform[] _tarLoc;
    Transform curLoc;
    public int maxLoc = 20;
    public int filledTargets = 1;
    public float lerpSpeed = 1f;

    private float stt, jlen;


    // Use this for initialization
	void Start () {
        _tarLoc = new Transform[maxLoc];
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < _tarLoc.Length; i++)
        {

            if (transform.position == _tarLoc[0].position && _tarLoc[1] != null)
            {
                //Shift the array 
                if (i < maxLoc - 1)
                {
                    _tarLoc[i] = _tarLoc[i + 1];
                }
                else
                {
                    _tarLoc[i] = null;
                }
            }
            else if (transform.position != _tarLoc[0].position)
            {
                //Lerp towards new Location
                stt = Time.time;
                curLoc.position = transform.position;
                jlen = Vector3.Distance(curLoc.position, _tarLoc[0].position);
                float dis = (Time.time - stt) * lerpSpeed;
                float fj = dis / jlen;
                transform.position = Vector3.Lerp(curLoc.position, _tarLoc[0].position, fj);
            }
        }

	}
}
