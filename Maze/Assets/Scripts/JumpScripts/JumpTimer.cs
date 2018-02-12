using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class JumpTimer : MonoBehaviour {

	public Text timerText;
	public Text pauseText;
	public Text pointsText;
	public Text endGameText;

	private const float startingTime = 3;
	private bool isPaused;
	private float elapsedTime;
	private GameObject player;
	private int coinsCollected;
	private Vector3 startPos;

	void Start () {
		StartNewGame();
	}
	
	private void StartNewGame()
	{
		elapsedTime = 0;
		coinsCollected = 0;
		startPos = transform.position;
		isPaused = true;
		timerText.text = Convert.ToString(startingTime);
		endGameText.text = "";
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void StartTimer() {
		if (SceneManager.GetActiveScene().name == "Jump")
		{
			isPaused = false;
			InvokeRepeating("ReduceMass", 0.0f, 2.0f);
		}
	}
	
	private void Update () {
		if (pointsText.text != Convert.ToString(coinsCollected))
			pointsText.text = Convert.ToString(coinsCollected);
		if (!isPaused)
		{
			if (pauseText.text != "Game Over!")
			{
				string seconds = "";
				elapsedTime += Time.deltaTime;
				seconds = (startingTime - elapsedTime).ToString("f1");
				timerText.text = seconds;
				if (startingTime - elapsedTime <= 0)
				{
					pauseText.text = "Game Over!";
					endGameText.color = Color.blue;
					player.SendMessage("ChangeButtonScene");
					endGameText.text = "You got " + coinsCollected.ToString() + " points!";
					CancelInvoke("ReduceMass");
				}
			}
			else
				isPaused = true;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
			if (pauseText.text != "Game Over!" && pauseText.text != "Paused")
				pauseText.text = "Paused";
			else if (pauseText.text == "Paused")
				pauseText.text = "";
		}	
	}

	private void CoinCollected()
	{
		coinsCollected++;
	}

	private void ResetPosition()
	{
		transform.GetComponent<Rigidbody>().MovePosition(startPos);
		transform.GetComponent<Rigidbody>().Sleep();
		if (elapsedTime + 5 > startingTime)
			elapsedTime = startingTime;
	}

	private void ReduceMass()
	{
		transform.GetComponent<Rigidbody>().mass -= 0.2f;
	}
}
