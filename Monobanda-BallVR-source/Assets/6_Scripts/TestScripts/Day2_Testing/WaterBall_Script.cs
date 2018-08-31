using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall_Script : MonoBehaviour {

	float timer = 0.0f;
	float floatTimer = 0.0f;
	public bool floatTimerStart = false;
	bool addTime = true;

	public float wobble = 1.0f;
	public float fallTime = 10.0f;
	public bool mirror = true;
	Vector3 currentVertical;
	Vector3 currentPos;
	Vector3 newPosUp;
	Vector3 newPosDown;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		//lowPos = startPos - new Vector3(0,1.0f,0);
		//highPos = startPos + new Vector3(0,1.0f,0);
		if(floatTimerStart){
			floatTimer += Time.deltaTime;
			if(addTime){
				timer += Time.deltaTime;
				if(timer > 1.0f){
					addTime = false;
				}
			} else {
				timer -= Time.deltaTime;
				if(timer < 0.0f){
					addTime = true;
				}
			}
			if(floatTimer < fallTime){
				currentVertical = this.transform.up*wobble;
				newPosUp = this.transform.position + currentVertical;
				newPosDown = this.transform.position - currentVertical;
				currentPos = Vector3.Lerp(newPosDown, newPosUp, timer);
				transform.Find("Visual").transform.position = Vector3.Lerp(transform.Find("Visual").transform.position, currentPos, Time.deltaTime);
				
			}else{
				this.GetComponent<Rigidbody>().useGravity = true;
			}
			if(mirror){
				transform.Find("Mirror").transform.position = this.transform.position + new Vector3(0, this.transform.position.y*-2+transform.Find("Visual").transform.localPosition.y*-1, 0);
				if(transform.Find("Mirror").transform.position.y >= 0){
					transform.Find("Mirror").transform.position = new Vector3(transform.Find("Mirror").transform.position.x, -0.01f, transform.Find("Mirror").transform.position.z);
				}
			} else {
				transform.Find("Mirror").GetComponent<Renderer>().enabled = false;
			}
		}

		
	}

	private void OnDisable()
	{
		addTime = true;
		timer = 0.0f;
		floatTimerStart = false;
		floatTimer = 0.0f;
		transform.Find("Visual").transform.localPosition = new Vector3(0,0,0);
		transform.Find("Mirror").transform.localPosition = new Vector3(0,0,0);
		this.GetComponent<Rigidbody>().useGravity = false;
		//print(this.gameObject.name + "Disabled");
	}
}
