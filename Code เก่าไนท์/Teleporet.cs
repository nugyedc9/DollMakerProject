using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporet : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playrg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playrg.SetActive(false);
            player.position = destination.position;
            playrg.SetActive(true);
        }
    }
}
