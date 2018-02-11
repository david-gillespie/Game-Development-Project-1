using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	private int score;

	private void PinCollision(){
		score++;
		print (score);
	}
}
