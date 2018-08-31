using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TargetRayTest : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		Vector3 ray = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position,ray, out hit,Mathf.Infinity))
        {
		  	Debug.DrawLine(transform.position, hit.point);
			Debug.Log(hit.point + " hit point");
            Debug.Log("distance : " + hit.distance);
		}
	}
}
