using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceTimer : MonoBehaviour {

	public Text timerText;
	public Text winText;

	private bool isPaused;
	private float elapsedTime;
	void Start () {
		StartNewGame();
	}
	
	private void StartNewGame()
	{
		isPaused = true;
		elapsedTime = 0;
		timerText.text = "0";
	}

	private void StartTimer()
	{
		if (SceneManager.GetActiveScene().name == "Race")
			isPaused = false;
	}

	void Update () {
		if (!isPaused)
		{
			if (winText.text != "You Win!" && winText.text != "You Lose!")
			{
				string seconds = "";
                elapsedTime += Time.deltaTime;
				seconds = elapsedTime.ToString("f1");
                timerText.text = seconds;
			}
		}
	}
}
