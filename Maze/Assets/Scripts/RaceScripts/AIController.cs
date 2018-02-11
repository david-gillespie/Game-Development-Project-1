using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour {

	public Vector3 force;

	private bool shouldMove;
	private Text text;
	private GameObject player;
	private Rigidbody rb;
	private Vector3 playerstartpos;
	private Vector3 startpos;
	void Start () {
		rb = transform.GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player");
		text = Text.FindObjectOfType<Text>();
		playerstartpos = player.transform.position;
		startpos = transform.position;
		shouldMove = false;
	}
	
	void FixedUpdate () {
		if (shouldMove && rb.position.z < 2000) {
			float speed = 1;//Random.Range(0.1f, 10);
			// if its too far over to left/right
			if (Mathf.Abs(transform.position.x) > 30)
			{
				// make it go to the opposite direction
				if (transform.position.x >= 30)
				{
					force = new Vector3(
						Random.Range(-50, 0), 0.0f, Random.Range(100, 175)
					) * speed;
				}
				else
				{
					force = new Vector3(
						Random.Range(0, 50), 0.0f, Random.Range(100, 175)
					) * speed;
				}
			}
			else
			{
				force = new Vector3(
				Random.Range(-100, 100), 0.0f, Random.Range(100, 175)
				) * speed;
			}
			rb.AddForce(force);
		}
		else
		{
			if (player.transform.position != playerstartpos && text.text == "")
			{
				shouldMove = !shouldMove;
			}
		}
	}

	private void WinZone()
	{
		rb.Sleep();
		transform.position = startpos;
		shouldMove = false;
	}
	
}
