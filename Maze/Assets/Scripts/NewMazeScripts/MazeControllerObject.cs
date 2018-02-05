using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeControllerObject : MonoBehaviour {
	private GameObject[] ghosts;
	private Vector3[] ghostVelocity;

	void getGhosts(GameObject[] ghostArray){
		ghosts = ghostArray;
		ghostVelocity = new Vector3[ghostArray.Length];
	}

	void pauseGhosts(){
		for(int i = 0; i< ghostVelocity.Length;i++){
			ghostVelocity [i] = ghosts [i].transform.GetComponent<Rigidbody> ().GetPointVelocity (ghosts [i].transform.position);
			ghosts [i].transform.GetComponent<Rigidbody> ().Sleep ();
			ghosts [i].transform.GetComponent<Rigidbody> ().WakeUp ();
		}
	}

	void resumeGhosts(){
		for (int i = 0; i < ghostVelocity.Length; i++) {
			ghosts [i].transform.GetComponent<Rigidbody> ().velocity = ghostVelocity [i];
		}
	}
}
