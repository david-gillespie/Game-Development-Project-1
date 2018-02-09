using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour {

	private Rigidbody rb;
	private GameObject player;
	private Vector3 startpos;
	private bool shouldMove;
	void Start () {
		rb = transform.GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player");
		startpos = player.transform.position;
		shouldMove = false;
	}
	
	void Update () {
		if (shouldMove && rb.position.z < 2000) {
			float speed = Random.Range(1, 1.3f);
			// if its too far over to left/right
			if (Mathf.Abs(transform.position.x) > 30)
			{
				// make it go to the opposite direction
				if (transform.position.x >= 30)
				{
					rb.AddForce(new Vector3(
						Random.Range(-50, 0), 0.0f, Random.Range(10, 15)
					) * speed);
				}
				else
				{
					rb.AddForce(new Vector3(
						Random.Range(0, 50), 0.0f, Random.Range(10, 15)
					) * speed);
				}
			}
			else
			{
				rb.AddForce(new Vector3(
				Random.Range(-100, 100), 0.0f, Random.Range(10, 15)
				) * speed);
			}
		}
		else
		{
			if (player.transform.position != startpos)
			{
				shouldMove = !shouldMove;
			}
		}
	}
	
}
