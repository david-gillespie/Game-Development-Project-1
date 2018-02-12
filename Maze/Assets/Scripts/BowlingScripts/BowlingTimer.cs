using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class BowlingTimer : MonoBehaviour {

	public Text timerText;
	public Text pauseText;


	private bool isPaused = true;
	private float elapsedTime;

	void Start () {
		StartNewGame();
	}

	public void StartNewGame()
	{
		elapsedTime = 0;
		timerText.text = "0";
	}

	void StartTimer(){
		isPaused = false;
	}

	void PauseTimer(){
		isPaused = true;
	}

	// Update is called once per frame
	void Update () {
		if (!isPaused)
		{
			if (pauseText.text != "You Win!" && pauseText.text != "You Lose.")
			{
				string seconds = "";
				elapsedTime += Time.deltaTime;
				seconds = elapsedTime.ToString("f1");
				timerText.text = seconds;
			}
			else if (pauseText.text == "You Win!")
			{
				isPaused = true;
			}
		}

	}
}
