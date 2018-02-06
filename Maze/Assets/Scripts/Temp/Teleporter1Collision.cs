using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter1Collision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            other.SendMessage("Teleporter1Collision");
        }
    }
}
