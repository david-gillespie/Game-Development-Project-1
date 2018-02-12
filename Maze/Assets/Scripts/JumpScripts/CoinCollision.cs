using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
        {
            other.GetComponent<AudioSource> ().Play ();
            this.gameObject.SetActive(false);
            other.SendMessage("CoinCollected");
        }
    }
}
