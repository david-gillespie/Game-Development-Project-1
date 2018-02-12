using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollision : MonoBehaviour {

    public AudioClip audioClip;

    void Start ()   
    {
        GetComponent<AudioSource> ().playOnAwake = false;
        GetComponent<AudioSource> ().clip = audioClip;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GetComponent<AudioSource> ().Play ();
            collision.collider.SendMessage("EnemyCollision");
        }
    }
}
