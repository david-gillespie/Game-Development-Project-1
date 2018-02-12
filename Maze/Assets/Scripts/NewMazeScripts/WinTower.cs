using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTower : MonoBehaviour {
	public GameObject player;

	private bool hasWon = false;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player") && !hasWon) {
			hasWon = true;
			other.SendMessage ("WinZone");
		}
		else if (other.CompareTag("AI")) {
			if (!hasWon)
			{
				player.SendMessage("LoseZone");
			}
			hasWon = true;
			other.SendMessage("WinZone");
		}
	}
}
