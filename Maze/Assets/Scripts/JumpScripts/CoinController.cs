using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	private Vector3 startingPos;
	private float bounce = 0.2f;
	private float speed = 0.005f;
	private int directionUp = 1;

	void Start () {
		startingPos = transform.position;
		if (Random.Range (0, 2) == 1) {
			directionUp = 1;
		} else {
			directionUp = -1;
		}
	}
	
	void Update () {
		if (startingPos.y + bounce < transform.position.y) {
			directionUp = -1;
		}else if(startingPos.y - bounce > transform.position.y){
			directionUp = 1;
		}
		transform.position =new Vector3 (transform.position.x,transform.position.y+(speed*directionUp),transform.position.z);
		transform.Rotate (0, 0, Time.deltaTime*30); //Rotates 30 degrees a second
	}
}
