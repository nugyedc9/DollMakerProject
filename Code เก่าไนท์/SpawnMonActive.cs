using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonActive : MonoBehaviour
{
    public Transform tranSpawn;
    public GameObject SpawnMon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            SpawnMon.SetActive(true);
        }
    }
}
