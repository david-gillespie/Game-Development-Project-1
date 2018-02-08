using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneration : MonoBehaviour {

	public GameObject coinPrefab;
	public GameObject platform1;
	public GameObject platform2;

	private const float maxHeight = 5f;
	private const float coinOffsetDistance = 3.5f;
	private const int numberOfCoins = 80;
	private float platform1radius;
	private float platform2radius;
	void Start () {
		platform1radius = platform1.transform.localScale.x / 2;
		platform2radius = platform2.transform.localScale.x / 2;
		GenerateCoins();
	}

	private void GenerateCoins()
	{
		platform1radius -= coinOffsetDistance;
		platform2radius -= coinOffsetDistance;
		for (int i = 0; i < numberOfCoins; i++)
		{
			Vector3 positionToSet;
			GameObject newCoin = Instantiate(coinPrefab);
			newCoin.transform.SetParent(transform);
			if (i % 3 == 0)
			{
				positionToSet = setCoinPosition(platform2radius, true);
			}
			else
			{
				positionToSet = setCoinPosition(platform1radius, false);
			}
			newCoin.transform.position = positionToSet;
		}
	}

	private Vector3 setCoinPosition(float platformradius, bool isPlatform2)
	{
		Vector3 result;
		do {
			result = new Vector3(
						(int)Mathf.Floor(Random.Range(-platformradius, platformradius)),
						(int)Mathf.Floor(Random.Range(1, maxHeight)), 
						(int)Mathf.Floor(Random.Range(-platformradius, platformradius)));
			if (isPlatform2)
			{
				result.y += 5;
				result.z += 40;
			}
		} while (Mathf.Abs(result.x) >= ((platformradius - (platformradius + coinOffsetDistance) / 3) - coinOffsetDistance) && 
					Mathf.Abs(result.z) >= ((platformradius - (platformradius + coinOffsetDistance) / 3) - coinOffsetDistance));
		return result;
	}
	
}
