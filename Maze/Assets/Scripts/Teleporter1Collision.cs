using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter1Collision : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            player.SendMessage("Teleporter1Collision");
        }
    }
}
