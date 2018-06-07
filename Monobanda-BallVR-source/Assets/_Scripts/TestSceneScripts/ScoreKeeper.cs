using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

	public int score = 1234567;
	public TextMesh scoreText;
	public TextMesh ballsLeftText;
	public int ballsLeft = 10;
	public SpawnBalls ballSpawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + score;
		ballsLeftText.text = "Balls left: " + ballsLeft;
		if(ballsLeft <= 0){
			ballSpawner.spawningBalls = false;
		}
	}

	
}
