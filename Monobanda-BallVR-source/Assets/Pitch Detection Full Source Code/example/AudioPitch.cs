﻿using UnityEngine;
using System.Collections;
using PitchDetector;

public class AudioPitch : MonoBehaviour {
//	public GUIText noteText;
    public static int _currentPitch;
    public int _currentpublicpitch;
    private AudioSource _audioSource;

	private Detector pitchDetector;						//Pitch detector object

	private int minFreq, maxFreq; 						//Max and min frequencies window
	public string selectedDevice { get; private set; }	//Mic selected
	private bool micSelected = false;					//Mic flag
    public string selectedMic;

    float[] data;										//Sound samples data
	public int cumulativeDetections= 5; 				//Number of consecutive detections used to determine current note
	int [] detectionsMade;								//Detections buffer
	private int maxDetectionsAllowed=50;				//Max buffer size
	private int detectionPointer=0;						//Current buffer pointer
	public int pitchTimeInterval=100; 					//Millisecons needed to detect tone
	private float refValue = 0.1f; 						// RMS value for 0 dB
	public float minVolumeDB=-17f;						//Min volume in bd needed to start detection

	private int currentDetectedNote =0;					//Las note detected (midi number)
	private string currentDetectedNoteName;				//Note name in modern notation (C=Do, D=Re, etc..)

	private bool listening=true;						//Flag for listening


	private int top_down_margin=20;						//Top and down margin
	private float ratio=0f;					
	//score positions
	private int startMidiNote=35; 						//Lowst midi printable in score
	private int endMidiNote=86; 						//Lowst midi printable in score
														//Conversion array from notes to positions. Minus indicates sharp note
									//si do  do#  re  re#  mi  fa  fa# sol sol#  la  la#
	int[] notePositions = new int[60] {0,  1,  -1,  2,  -2,  3,  4,  -4, 5,   -5, 6,   -6,
									   7,  8,  -8,  9,  -9, 10, 11, -11, 12, -12, 13, -13,
									  14, 15, -15, 16, -16, 17, 18, -18, 19, -19, 20, -20,
									  21, 22, -22, 23, -23, 24, 25, -25, 26, -26, 27, -27,
									  28, 29, -29, 30, -30, 31, 32, -32, 33, -33, 34, -34};
	//There is enough positions...



	void Awake() {
		pitchDetector = new Detector();
		pitchDetector.setSampleRate(AudioSettings.outputSampleRate);
        _audioSource = GetComponent<AudioSource>();
    }

/*
	//Start function for web player (also works on other platforms)
	IEnumerator Start() {
        

		yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
		if (Application.HasUserAuthorization(UserAuthorization.Microphone)) {
			selectedDevice = Microphone.devices[0].ToString();
			micSelected = true;
			GetMicCaps();
			
			//Estimates bufer len, based on pitchTimeInterval value
			int bufferLen = (int)Mathf.Round (AudioSettings.outputSampleRate * pitchTimeInterval / 1000f);
			Debug.Log ("Buffer len: " + bufferLen);
			data = new float[bufferLen];
			
			detectionsMade = new int[maxDetectionsAllowed]; //Allocates detection buffer
		} else {
		}
	}*/


	void Start () {
		selectedDevice = Microphone.devices[0].ToString();
        selectedMic = selectedDevice;
        micSelected = true;
		GetMicCaps();

		//Estimates bufer len, based on pitchTimeInterval value
		int bufferLen = (int)Mathf.Round (AudioSettings.outputSampleRate * pitchTimeInterval / 1000f);
	//	Debug.Log ("Buffer len: " + bufferLen);
		data = new float[bufferLen]; 
        

		detectionsMade = new int[maxDetectionsAllowed]; //Allocates detection buffer
        setUptMic();


    }
	

	void Update () {
		if (listening) {
            _audioSource.GetOutputData(data,0);
			float sum = 0f;
			for(int i=0; i<data.Length; i++)
				sum += data[i]*data[i];
			float rmsValue = Mathf.Sqrt(sum/data.Length);
			float dbValue = 20f*Mathf.Log10(rmsValue/refValue);
			if(dbValue<minVolumeDB) {
			//	noteText.text="Note: <<";
			//	hideNotes();
				return;
			}
			
			pitchDetector.DetectPitch (data);
			int midiant = pitchDetector.lastMidiNote ();
			int midi = findMode ();
            _currentPitch = midi - startMidiNote;
            _currentpublicpitch = _currentPitch;
            //	drawNote(midi);
            //noteText.text="Note: "+pitchDetector.midiNoteToString(midi);
            detectionsMade [detectionPointer++] = midiant;
			detectionPointer %= cumulativeDetections;
		}
		else {
			//noteText.text="Note: -";
		}
	}



	int notePosition(int note) {
		int arrayIndex = note - startMidiNote;
		if (arrayIndex < 0)
			arrayIndex = 0; //this is a super contrabass man!!!
		if (arrayIndex > (endMidiNote - startMidiNote))
			arrayIndex = (endMidiNote - startMidiNote); //This is a megasoprano girl!!

		return notePositions [arrayIndex];
	}




	void setUptMic() {
		GetComponent<AudioSource>().volume = 1f;
		GetComponent<AudioSource>().clip = null;
		GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
		//GetComponent<AudioSource>().mute = false; // Mute the sound, we don't want the player to hear it
		StartMicrophone();
	}

	public void GetMicCaps () {
		Microphone.GetDeviceCaps(selectedDevice, out minFreq, out maxFreq);//Gets the frequency of the device
		if ((minFreq + maxFreq) == 0)
			maxFreq = 44100;
	}
	
	public void StartMicrophone () {
		GetComponent<AudioSource>().clip = Microphone.Start(selectedDevice, true, 10, maxFreq);//Starts recording
		while (!(Microphone.GetPosition(selectedDevice) > 0)){} // Wait until the recording has started
		GetComponent<AudioSource>().Play(); // Play the audio source!
	}
	
	public void StopMicrophone () {
		GetComponent<AudioSource>().Stop();//Stops the audio
		Microphone.End(selectedDevice);//Stops the recording of the device	
	}

	int repetitions(int element) {
		int rep = 0;
		int tester=detectionsMade [element];
		for (int i=0; i<cumulativeDetections; i++) {
			if(detectionsMade [i]==tester)
				rep++;
		}
		return rep;
	}
	
	public int findMode() {
		cumulativeDetections = (cumulativeDetections >= maxDetectionsAllowed) ? maxDetectionsAllowed : cumulativeDetections;
		int moda = 0;
		int veces = 0;
		for (int i=0; i<cumulativeDetections; i++) {
			if(repetitions(i)>veces)
				moda=detectionsMade [i];
		}
		return moda;
	}
}
