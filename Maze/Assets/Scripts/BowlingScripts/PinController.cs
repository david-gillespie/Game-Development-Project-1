using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour {

	public GameObject pinScoreController;
	bool hasCounted = false;

	void Start () {
		
	}
	
	void Update () {
		
	}

	private void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Untagged" && !hasCounted){
			//Left the ground at least for a moment
			pinScoreController.SendMessage("PinCollision");
			hasCounted = true;
		}
	}

	private void defineScoreController(GameObject scoreController){
		pinScoreController = scoreController;
	}
}
