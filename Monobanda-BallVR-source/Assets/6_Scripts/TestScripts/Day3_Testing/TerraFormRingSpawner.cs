using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundInput;

public class TerraFormRingSpawner : MonoBehaviour {

	public SoundInputController SIC;
	public GameObject ring;
	public GameObject spawnLocation;
	public Color highPitchColor = Color.yellow;
	public Color lowPitchColor = Color.red;
	private Color currentColor;

	private float _micPitch;
    private float _micAmplitude;
	private bool _isSpeaking = false;
	[Tooltip("Amount of Rings per second to spawn")]
	public int ringSpawningSpeed = 10;
	public float ringMoveSpeed = 5.0f;
	private float speakingTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		_micPitch =  SIC.inputData.relativeFrequency;
        _micAmplitude = SIC.inputData.amp01;

		if ((_micAmplitude > 0) && (!_isSpeaking)){
			_isSpeaking = true;
		}
		if ((_micAmplitude <= 0) && (_isSpeaking)){
			_isSpeaking = false;
			SIC.NullifyClipData();
		}
		if (_isSpeaking){
			currentColor = Color.Lerp(lowPitchColor, highPitchColor, _micPitch);
			speakingTimer+=Time.deltaTime;
			if(speakingTimer >= 1.0f/ringSpawningSpeed){
				GameObject currentRing = GameObject.Instantiate(ring, spawnLocation.transform.position, this.transform.rotation);
				currentRing.transform.localScale = new Vector3(_micAmplitude*5, _micAmplitude*5, _micAmplitude*5);
				currentRing.GetComponent<TerraformRing_Script>().moveSpeed = ringMoveSpeed;
				currentRing.GetComponent<TerraformRing_Script>().pitch = _micPitch;
				currentRing.GetComponent<TerraformRing_Script>().currentColor = currentColor;
				speakingTimer = 0.0f;
			}
		}

	}
}
