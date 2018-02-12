using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour {

    public AudioClip audioClip;

    void Start ()   
    {
        GetComponent<AudioSource> ().playOnAwake = false;
        GetComponent<AudioSource> ().clip = audioClip;
    }   

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource> ().Play ();
            //this.gameObject.SetActive(false);
            other.SendMessage("CoinCollected");
        }
    }
}
