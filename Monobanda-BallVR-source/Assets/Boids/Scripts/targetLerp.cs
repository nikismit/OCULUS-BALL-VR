using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLerp : MonoBehaviour {

	public Transform currLoc;
	public Transform tarLoc;
	public float spd = 0.3f;

	private float stt, len;

	void Update () {


		float dc = (Time.time - stt) * spd;
    float fr = dc / len;
    transform.position = Vector3.Lerp(currLoc.position, tarLoc.position, fr);

	}

	void getLocation(Transform _t){
		//
		if (_t.position != tarLoc.position)
		{
			tarLoc.position = _t.position;
			stt = Time.deltaTime;
			len = Vector3.Distance(currLoc.position,tarLoc.position);
		}
	}
}
