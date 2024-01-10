using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lastpos : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;
    private bool isBeingPulled = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            Vector3 directionToGhost = other.transform.position - transform.position;

            rb.AddForce(-directionToGhost.normalized * 5f, ForceMode.Force);
            isBeingPulled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            isBeingPulled = false;
        }
    }

    private void Update()
    {
        if (!isBeingPulled)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            rb.AddForce(directionToPlayer.normalized * 5f, ForceMode.Force);
        }
    }
}
