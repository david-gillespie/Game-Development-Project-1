using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour {

	public GameObject pinScoreController;

	void Start () {
		
	}
	
	void Update () {
		
	}

	private void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Untagged"){
			//Left the ground at least for a moment

		}
	}

	private void defineScoreController(GameObject scoreController){
		pinScoreController = scoreController;
	}
}
