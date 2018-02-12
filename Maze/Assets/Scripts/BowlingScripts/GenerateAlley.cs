using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAlley : MonoBehaviour {
	
	public GameObject alleyBlockPrefab;
	public GameObject generateStartPosition;
	public GameObject bowlingPins;
	public Material[] materials;
	public GameObject scoreController;
	public GameObject player;
	public Camera pinCamera;

	private Vector3 startPosition;

	private float sqrt125;
	private float angleConversion;
	void Start () {
		sqrt125 = Mathf.Sqrt (1.25f);
		angleConversion = sqrt125 / Mathf.Sin (DegreeToRadian(90));

		startPosition = generateStartPosition.transform.position;

		long difference = 200;

		Vector3 nextPosition = startPosition;
		float rotation = -30.0f;
		float variance = 30.0f;
		float newRotation = 0.0f;
		float oldRotation;
		float rotationVariance = 0.0f;
		GameObject go;
		for(int i = 0;i<difference;i++){
			go = Instantiate (alleyBlockPrefab).gameObject;
			go.transform.position = nextPosition;
			go.transform.rotation = Quaternion.Euler (-newRotation,0.0f,rotationVariance);
			go.GetComponent<Renderer>().material = materials[i%2];

			rotationVariance = newVariance (rotationVariance, 40.0f, 5.0f);

			oldRotation = newRotation; // Used to generate next object generation point
			newRotation = Random.Range (0, variance)  + rotation;

			if (i+1 >= difference) {
				nextPosition = findNextCenter (go.transform.position, oldRotation, 0.0f);
				nextPosition.z += 12.0f;
			} else {
				nextPosition = findNextCenter (go.transform.position, oldRotation, newRotation);
			}
		}
		go = Instantiate (bowlingPins);
		PinController[] objects = go.GetComponentsInChildren<PinController> ();
		for(int i = 0; i<objects.Length;i++){
			objects [i].pinScoreController = scoreController;
		}
		go.transform.position = nextPosition;
		scoreController.GetComponent<ScoreController>().lowestBounds = nextPosition.y-10;
		nextPosition.z += 4;
		nextPosition.y += 2;
		pinCamera.transform.rotation = Quaternion.LookRotation (player.transform.position);
		pinCamera.transform.position = nextPosition;
	}

	void FixedUpdate () {
		pinCamera.transform.rotation = Quaternion.LookRotation (player.transform.position-pinCamera.transform.position);
	}

	float RadianToDegree(float radian){
		return (radian / Mathf.PI) * 180;
	}

	float DegreeToRadian(float degree){
		return (degree / 180) * Mathf.PI;
	}

	Vector3 findTopCorner(Vector3 previousPostion,float oldAngle){
		float angleMeasure1 = 30 + oldAngle;
		float height = angleConversion * Mathf.Sin (DegreeToRadian (angleMeasure1));
		float width = angleConversion * Mathf.Sin (DegreeToRadian(90 - angleMeasure1));
		return new Vector3 (previousPostion.x,previousPostion.y+height,previousPostion.z+width);
	}

	//Uses SAA to find next start position
	Vector3 findNextCenter(Vector3 previousPosition,float oldAngle, float newAngle){
		//Triangle is 1, 1/2, sqrt(1.25), so angle measures are 30,60,90 when unrotated
		Vector3 topCorner = findTopCorner(previousPosition,oldAngle);

		float angleMeasure2 = 60 + newAngle;
		float width2 = angleConversion * Mathf.Sin (DegreeToRadian (angleMeasure2));
		float height2 = angleConversion * Mathf.Sin (DegreeToRadian (90 - angleMeasure2));

		Vector3 newCenter = new Vector3 (previousPosition.x,topCorner.y-height2-.03f,topCorner.z+width2+.1f);
		return newCenter;
	}

	float newVariance(float previousVariance,float range, float deviation){
		float randDeviation = Random.Range (-(deviation/2.0f),deviation/2.0f);
		float newVariance = previousVariance+randDeviation;
		if (newVariance > (range/2)) {
			newVariance = (range/2);
		} else if(newVariance<(-(range/2))) {
			newVariance = -(range/2);
		}
		return newVariance;
	}
}
