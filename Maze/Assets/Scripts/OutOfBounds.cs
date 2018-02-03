using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

	public GameObject player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			player.SendMessage ("OutOfBounds");

		}
	}
}
