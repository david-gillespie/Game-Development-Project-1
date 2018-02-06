using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchPadCollision : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            other.SendMessage("LaunchPlayer");
        }
    }
}
