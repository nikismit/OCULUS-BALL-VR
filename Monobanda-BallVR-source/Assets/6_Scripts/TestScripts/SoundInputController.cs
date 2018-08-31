using System;
using System.Collections;
using System.Threading;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;

using PitchDetector;
using Utility;

namespace SoundInput
{	
	[System.Serializable]
	public class InputData
	{		
		public float frequency;					 // the lastFrequency detect
		public float relativeFrequency;			 // frequency in relation to min and max Freq
		public float amplitude; 				 // volume in db
		public float relativeAmplitude;			 // volume in db minus the minimun db level
		public float amp01;
		public float toneLength;				 // the length in seconds of the current sound				
		public bool makingSound;				 // if the there is a sound registered
		public float[] spectrum = new float[128];// audio spectrum	
		
		// make a shallow copy of object
		public InputData Copy() { return (InputData) this.MemberwiseClone(); }
	}

	[RequireComponent(typeof(AudioSource))]
	public class SoundInputController : MonoBehaviour
	{
		public delegate void MicInputUpdate(InputData data);

		// fields		
		public static SoundInputController instance;
		[Header("Properties")]
		public MicInputUpdate callback;
		private float maxVolume;

		public int wantedMic=1;
		public bool sampleMic;
		public bool sample;
		public bool settingsLocked;
		
		[SerializeField]
		private int cumulativeDetections = 5;

		[SerializeField]
		public SoundSettings settings;

		public float currentAudioTime = 0.0f;

		[Header("Data")]
		[SerializeField]
		public InputData inputData;

		private Detector analyser;
		private new AudioSource audio;
		private AudioClip micClip;
		private float[] data;
		private int [] detectionsMade;
		private float previousFrequency;		
		private int minFreq, maxFreq;
		private int maxDetectionsAllowed = 50;

		private ThreadStart ThreadFuncRef;
		private Thread thread;
		bool currentSound = false;
		bool nullified = false;

		void Awake()
		{			
			analyser = new Detector();
			inputData = new InputData();
			callback = new MicInputUpdate((InputData data)=>{});
			
			instance = this;
			analyser.setSampleRate(AudioSettings.outputSampleRate);
			//StartUp();
		}

		IEnumerator Start()
		{
			yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
			if (Application.HasUserAuthorization(UserAuthorization.Microphone)) 
			{
				if(wantedMic > 0) {
					if(Microphone.devices[wantedMic-1] != null) {
						SelectedDevice = Microphone.devices[wantedMic-1].ToString();
						//Debug.Log("Selected Mic: "+ selectedDevice);

						Microphone.GetDeviceCaps(SelectedDevice, out minFreq, out maxFreq);//Gets the frequency of the device
<<<<<<< HEAD
=======
                        //print("MinFreq: " + minFreq + " - MaxFreq: " + maxFreq);
>>>>>>> Niki
							if ((minFreq + maxFreq) == 0)
								maxFreq = 44100;
					}
				}
				int bufferLen = (int)Mathf.Round (AudioSettings.outputSampleRate / 10.0f);//* 100.0f / 1000.0f);
				//Debug.Log ("Buffer len: " + bufferLen);
				data = new float[bufferLen];
				
				detectionsMade = new int[maxDetectionsAllowed]; //Allocates detection buffer			
				
				audio = GetComponent<AudioSource>();
				
				if(wantedMic > 0) {
					if(sampleMic == true && sample == true)
						SetupMic();
					else if ( sample == true )
						audio.Play();
				}

				maxVolume = settings.maxVolume;

				ThreadFuncRef = new ThreadStart(AnalyserThread);
				thread = new Thread(ThreadFuncRef);
				thread.Start();
			}
			else
				Application.Quit();
		}

		void OnDestroy()
		{
			if(thread != null )
				thread.Abort();
		}

		void OnApplicationQuit()
		{
			if(thread != null )
				thread.Abort();
		}

		void Update()
		{		
			if(SelectedDevice == null )
				return;

			audio.GetOutputData( data, 0 );	
			currentAudioTime = audio.time;
			
			float sum  = 0f; // sum value
			for(int i=0; i<data.Length; i++)
				sum += data[i]*data[i];

			if( maxVolume > 0 )
				maxVolume = settings.maxVolume;

			float rmsValue = Mathf.Sqrt( sum / data.Length );
			inputData.amplitude = 20f * Mathf.Log10( rmsValue / 0.1f );
			inputData.relativeAmplitude = inputData.amplitude - settings.minVolume;
			inputData.amp01 = inputData.relativeAmplitude/(settings.maxVolume+Mathf.Abs(settings.minVolume));

			if ( inputData.amplitude > maxVolume)
			{
				inputData.amplitude = maxVolume;
				inputData.relativeAmplitude = maxVolume;
			}

			if (inputData.amplitude < settings.minVolume || sample == false) 
			{
				//sound too low				
				inputData.makingSound = false;
				previousFrequency = inputData.frequency;
				inputData.frequency = 0;
				inputData.toneLength = 0;
				inputData.relativeFrequency = 0;
				inputData.spectrum = new float[512];
			}
			else
			{
				inputData.makingSound = true;
				audio.GetSpectrumData(inputData.spectrum, 1, FFTWindow.Rectangular);
				previousFrequency = inputData.frequency;
				// detect frequency 
				inputData.frequency = analyser.lastFrequency();
				inputData.frequency = inputData.frequency==0? previousFrequency : inputData.frequency;
				inputData.relativeFrequency = ((inputData.frequency-settings.minFreq)/(settings.maxFreq-settings.minFreq));
				inputData.relativeFrequency = Mathf.Clamp01(inputData.relativeFrequency);
				inputData.toneLength += Time.deltaTime;
			}

			

<<<<<<< HEAD
			if(audio.time >= 29.0f && nullified == false){
				//print("Time to Nullify!");
=======
			if(audio.time >= audio.clip.length - 1.0f && nullified == false){
				print("Time to Nullify!");
>>>>>>> Niki
				this.NullifyClipData();
				nullified = true;
			}

			if(audio.time <= 5.0f){
				nullified = false;
			}
			
			callback.Invoke(inputData);
		}

		

		public void StartMic()
		{
			audio.Stop();
			audio.time = 0;
			audio.clip = Microphone.Start(SelectedDevice, true, 30, maxFreq);//Starts recording
			//micClip = audio.clip;
			while (!(Microphone.GetPosition(SelectedDevice) > 0)){} // Wait until the recording has started
			audio.Play(); // Play the audio source!
			sample = true;
		}

		public void StopMic()
		{
			audio.Stop();//Stops the audio
			Microphone.End(SelectedDevice);//Stops the recording of the device	
		}
		
		public void SetTime( float time )
		{
			audio.Stop();
			audio.time = time;
			audio.Play();
		}

		public void SetSample(AudioClip clip)
		{
			audio.Stop();
			Microphone.End( SelectedDevice );
			
			audio.clip = clip;
		}

		public int FindMode()
		{
			cumulativeDetections = (cumulativeDetections >= maxDetectionsAllowed) ? maxDetectionsAllowed : cumulativeDetections;
			int moda = 0;
			int veces = 0;
			for (int i=0; i<cumulativeDetections; i++) {
				if(Repetitions(i)>veces)
					moda=detectionsMade [i];
			}
			return moda;
		}

		public void ToggleRecording()
		{
			maxVolume = maxVolume == 0? settings.maxVolume : 0;
		}

		public void SetLock(bool isLocked)
		{
			settingsLocked = isLocked;
		}

		public void SetupMic()
		{	
			audio.Stop();
			if(Microphone.IsRecording(SelectedDevice)){
				Microphone.End(SelectedDevice);
			}
			audio.clip = null;
			audio.loop = true;
			audio.mute = false;

			StartMic();
		}

		public void NullifyClipData()
		{
<<<<<<< HEAD
			float[] samples = new float[audio.clip.samples * audio.clip.channels];
=======
            print("Nullifying...");
            float[] samples = new float[audio.clip.samples * audio.clip.channels];
>>>>>>> Niki
			audio.clip.GetData(samples, 0);
			int i = 0;
			while (i < samples.Length) {
				samples[i] = samples[i] * 0.0F;
				++i;
			}
			audio.clip.SetData(samples, 0);
		}

		private int Repetitions(int element)
		{
			int rep = 0;
			int tester=detectionsMade [element];
			for (int i=0; i<cumulativeDetections; i++) {
				if(detectionsMade [i]==tester)
					rep++;
			}
			return rep;
		}


		private void AnalyserThread()
		{
			while(thread.IsAlive)
			{
				if(inputData.makingSound == true )
				{
					lock(analyser)
					{
						analyser.DetectPitch (data);
					}
				}
				Thread.Sleep(20);
			}
		} 

		// properties
		public string SelectedDevice { get; private set; }
	}
}
