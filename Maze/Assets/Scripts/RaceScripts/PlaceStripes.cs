using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceStripes : MonoBehaviour {

	public GameObject stripePrefab;

	private const int startLocation = -1480;
	private const int endLocation   = 1480;
	void Start () {
		PlaceStripe();
	}
	
	private void PlaceStripe()
	{
		for (int i = startLocation; i < endLocation; i += 50)
		{
			GameObject newGhostObject = Instantiate (stripePrefab);
			newGhostObject.transform.SetParent (transform);
			newGhostObject.transform.position = new Vector3 (0, 0.001f, i);
		}
	}
}
