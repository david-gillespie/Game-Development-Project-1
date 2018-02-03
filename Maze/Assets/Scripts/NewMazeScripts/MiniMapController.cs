using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset = new Vector3(0,20,0);
	private Rect viewport;

	void Start () {
		Camera cam = GameObject.Find ("MiniMapCamera").GetComponent<Camera>();
		float aspectRatio = Screen.width / Screen.height;
		cam.rect = new Rect (1.0f-(0.3f/aspectRatio)-0.03f,0.67f,0.3f/aspectRatio,0.3f);
	}

	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}

}