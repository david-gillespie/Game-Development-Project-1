using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter2Collision : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            player.SendMessage("Teleporter2Collision");
        }
    }
}
