using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour {

	public GameObject pinScoreController;
	bool hasCounted = false;

	private void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Untagged" && !hasCounted){
			//Left the ground at least for a moment
			pinScoreController.SendMessage("PinCollision");
			hasCounted = true;
		}
		if (collision.gameObject.tag == "Player") {
			collision.gameObject.SendMessage ("cannotMove");
		}
	}

	private void defineScoreController(GameObject scoreController){
		pinScoreController = scoreController;
	}
}
