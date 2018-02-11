using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAlley : MonoBehaviour {
	
	public GameObject alleyBlockPrefab;
	public GameObject generateStartPosition;

	void Start () {
		int startPos = (int) generateStartPosition.transform.position.z;
		int endPos = -45;
		long difference = Mathf.Abs(startPos - endPos);

		for(int i = 0;i<difference;i+=2){
			GameObject go = Instantiate (alleyBlockPrefab).gameObject;
			go.transform.position=new Vector3(generateStartPosition.transform.position.x,(i/(difference/2.0f))*(generateStartPosition.transform.position.y),generateStartPosition.transform.position.z + i);
		}
	}

	void Update () {
		
	}
}
