using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostWalkState : StateGhost
{
    public NavMeshAgent enemyGhost;
    public List<Transform> destination;
    public Animator GhostAni;
    public float walkSpeed, DistanceAmount, sightDistance, IdleTime;
    public bool walking, chasing, searching, stopSearch, Attacked, Idle;
    public Transform player;
    public Vector3 LastSound;
    public GameObject DeadCanva;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    [Header("Ghost")]
    public GameObject BlackSphere;
    public GameObject GhostFrom;

    public GhostChaseState chaseState;

    public override StateGhost RunCurrentState()
    {
        if (chasing)
            return chaseState;
        else
            return this;
    }

    private void Start()
    {    
        randNum = Random.Range(0, destinationAmount);
        currentDest = destination[randNum];
        walking = true;
    }
    private void Update()
    {
        DistanceAmount = enemyGhost.remainingDistance;

        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                chasing = true;
                walking = false;
            }
            else
            {
                chasing = false;
                StartCoroutine("stayIdle");
            }
        }

        if (walking == true)
        {
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            dest = currentDest.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance)
            {
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }

    IEnumerator stayIdle()
    {
       // print("IdleGhost");
        yield return new WaitForSeconds(IdleTime);
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destination[randNum];
        /* GhostAni.ResetTrigger("Idle");
         GhostAni.SetTrigger("Walk");*/
    }

}
