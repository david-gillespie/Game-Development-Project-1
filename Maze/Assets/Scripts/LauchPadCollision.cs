using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchPadCollision : MonoBehaviour {

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            player.SendMessage("LaunchPlayer");
        }
    }
}
