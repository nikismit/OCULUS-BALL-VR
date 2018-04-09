﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour {

	[Header("Ball Options")]
    
    public GameObject _ballPrefab;
    public Material _ballMaterial;
    public PhysicMaterial _ballPhysicMaterial;
	public int _ballPoolAmount;
    public bool _growPool;
    List<GameObject> _balls;
    List<Material> _lMaterial;
    List<PhysicMaterial> _lPhysicMaterial;

	public Transform _spawnLocation;

    // ball size
    public float _growTimeMax;
    public Vector2 _ballsizeMinMax;
	public float defaultMass;
	public bool _massMultiplyBySize;
    public Vector2 _ballBounceMinMax;
	public bool _bounceBasedOnPitch;
	public float _forceAdd;
	
	private float _spawnTimer = 0.0f;
	public float _minSpeakTime = 0.5f;
	

	[Header("Colors")]
	public Color lowPitchColor;
	public Color midPitchColor;
	public Color highPitchColor;


    private GameObject _currentBall;
    private Material _currentMaterial;
    private Color _currentColor;
    private int _currentItem;
    private Rigidbody _currentRigidbody;
    private SphereCollider _currentSphereCollider;

    //microphone variables
	[Header("Mic Options")]
	public GameObject MicManager;
	public float _minPitch;
	public float _maxPitch;
	public float _maxRegisteredAmplitude;
    public float _micPitch;
    private float _micAmplitude;


    private float _timeRecording;
    private bool _isSpeaking;

    
    private float _ballSizeCurrent;
	private float _currentAmplitude;
    
    
    private float _highestAmplitude;

    public static int _currentBallNum = 1;
    


    // Use this for initialization
    void Start () {

        //pooling balls
        _balls = new List<GameObject>();
        _lMaterial = new List<Material>();
        _lPhysicMaterial = new List<PhysicMaterial>();

        for (int i = 0; i < _ballPoolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(_ballPrefab);
            Material mat = new Material(_ballMaterial);
            PhysicMaterial physicmat = new PhysicMaterial(i.ToString()); 

            obj.GetComponent<Renderer>().material = mat;
            obj.GetComponent<SphereCollider>().material = physicmat;
            obj.SetActive(false);
            _balls.Add(obj);
            _lMaterial.Add(mat);
            _lPhysicMaterial.Add(physicmat);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //pitch

        _micPitch =  Mathf.Clamp01(((Mathf.Clamp((float)VoiceProfile._voicePitch, _minPitch, _maxPitch))-_minPitch) / (_maxPitch - _minPitch));




        _micAmplitude = VoiceProfile._amplitudeCurrent;
   
        if ((_micAmplitude >= VoiceProfile._amplitudeSilence) && (!_isSpeaking)) //start speaking SPAWN
        {
            _currentColor = new Color(0, 0, 0, 1);
            _isSpeaking = true;
			_spawnTimer = 0.0f;
            _currentBall = GetPooledBall();
            _currentMaterial = _currentBall.GetComponent<Renderer>().material;
            _currentRigidbody = _currentBall.GetComponent<Rigidbody>();
            _currentSphereCollider = _currentBall.GetComponent<SphereCollider>();
            _currentMaterial.SetColor("_Color", _currentColor);
            _currentBall.transform.position = _spawnLocation.position;
			//_currentBall.name = "Ball" + _currentBallNum;
			//print(_currentBall.name);
			//_currentBallNum +=1;
            _currentRigidbody.isKinematic = true;
        }

        if ((_micAmplitude < VoiceProfile._amplitudeSilence) && (_isSpeaking)) //stop speaking RELEASE
        {
			if(_spawnTimer >= _minSpeakTime){
				_currentRigidbody.isKinematic = false;
				_isSpeaking = false;
				_timeRecording = 0;
				_highestAmplitude = Mathf.Clamp(_highestAmplitude, 0, _maxRegisteredAmplitude);
				//print(_currentBall.name + " - Exit force -> " + this.transform.forward * _forceAdd * _highestAmplitude);
				_currentRigidbody.AddForce(this.transform.forward * _forceAdd * _highestAmplitude);
				_highestAmplitude = 0;
				MicManager.GetComponent<AudioPitch>().ClearMicrophone();
			} else {
				_currentBall.SetActive(false);
				//_currentBallNum -= 1;
			}
        }

        if (_isSpeaking) //WHILE speaking
        {
            if (_micAmplitude > _highestAmplitude)
            {
                _highestAmplitude = _micAmplitude;
            }
			_currentAmplitude = _micAmplitude;
            _currentBall.transform.position = _spawnLocation.position;
            _timeRecording += Time.deltaTime;
            _spawnTimer += Time.deltaTime;
            _ballSizeCurrent = Mathf.Lerp(_ballsizeMinMax.x, _ballsizeMinMax.y, Mathf.Clamp01(_timeRecording / _growTimeMax));
            _currentBall.transform.localScale = new Vector3(_ballSizeCurrent, _ballSizeCurrent, _ballSizeCurrent);

			bool belowMid = true;
			float lowMid = 1.0f;
			float midHigh = 0.0f;

			if(_micPitch <=0.5f){
				belowMid = true;
				lowMid = _micPitch*2;
				midHigh = 0;
			} else if (_micPitch > 0.5f){
				belowMid = false;
				midHigh = (_micPitch - 0.5f)*2;
				lowMid = 0;
			}
			if(belowMid){
				_currentColor = Color.Lerp(lowPitchColor, midPitchColor, Mathf.Clamp01(lowMid));
			} else {
				_currentColor = Color.Lerp(midPitchColor, highPitchColor, Mathf.Clamp01(midHigh));
			}
            _currentMaterial.SetColor("_Color", _currentColor);
			_currentMaterial.SetColor("_EmissionColor", _currentColor);

            if (_massMultiplyBySize)
            {
                _currentRigidbody.mass = defaultMass * _ballSizeCurrent;
            }
            else
            {
                _currentRigidbody.mass = defaultMass;
            }

            if (_bounceBasedOnPitch)
            {
                _currentSphereCollider.material.bounciness = Mathf.Lerp(_ballBounceMinMax.x, _ballBounceMinMax.y, _micPitch);
            }
            else
            {
                _currentSphereCollider.material.bounciness = _ballBounceMinMax.y;
            }
            _currentSphereCollider.material.bounceCombine = PhysicMaterialCombine.Average;
        }
    }


    GameObject GetPooledBall()
    {
        for (int i = 0; i < _balls.Count; i++)
        {
            if (!_balls[i].activeInHierarchy)
            {
                _balls[i].SetActive(true);
                _currentItem = i;
                return _balls[i];
            }
        }
        

        if (_growPool)
        {
            GameObject obj = (GameObject)Instantiate(_ballPrefab);
            _balls.Add(obj);
            _currentItem = _balls.Count - 1;
            return obj;

        }

        // we kunnen hier ook maken dat een eerdere bal wordt weggehaald inplaats van de nieuwe bal
        return null;

    }


}
