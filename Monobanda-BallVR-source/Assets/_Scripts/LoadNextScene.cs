using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour {


	Scene currScene;
	void Start(){
		currScene = SceneManager.GetActiveScene ();
		//Debug.Log(currScene);

	}
	void Update () {

			int buildIndex = currScene.buildIndex;

			if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey ("enter") || Input.GetKeyUp ("n")) {


				if (buildIndex != 2){
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				} else {
					SceneManager.LoadScene(0);
				}
				//OF
				//Verander de naam van de scene die je wilt openen
				//SceneManager.LoadScene("Scene");


			}

	}
}
