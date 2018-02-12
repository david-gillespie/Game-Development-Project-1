using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

	public GameObject player;
	public float lowestBounds;
	public Text scoreText;

	private int score;
	private bool isOver = false;
	private bool firstCollision = false;
	private float lastHit;

	void Update(){
		if (firstCollision && !isOver && player.GetComponent<Rigidbody> ().velocity.magnitude == 0) {
			isOver = true;
			if (score >= 5) {
				player.SendMessage("WinZone");
				scoreText.text = "You got " + score.ToString()+" pins!";
			} else {
				player.SendMessage ("LoseZone");
				scoreText.text = "You only got " + score.ToString()+" :(";
			}
			player.SendMessage ("PauseTimer");
		} else if (!isOver && firstCollision && Time.time - lastHit > 5) {
			isOver = true;
			if (score >= 5) {
				player.SendMessage("WinZone");
				scoreText.text = "You got " + score.ToString()+" pins!";
			} else {
				player.SendMessage ("LoseZone");
				scoreText.text = "You only got " + score.ToString()+" :(";
			}
			player.SendMessage ("PauseTimer");
		}

		if (player.transform.position.y < lowestBounds) {
			isOver = true;
			if (score >= 5) {
				player.SendMessage("WinZone");
				scoreText.text = "You got " + score.ToString()+" pins!";
			} else {
				player.SendMessage ("LoseZone");
				scoreText.text = "You only got " + score.ToString()+" :(";
			}
			player.SendMessage ("PauseTimer");
		}
	}

	private void PinCollision(){
		score++;
		print (score);
		firstCollision = true;
		lastHit = Time.time;
	}
}
