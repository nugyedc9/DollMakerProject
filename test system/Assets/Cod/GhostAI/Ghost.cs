using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{

    public NavMeshAgent enemyGhost;
    public List<Transform> destination;
    public Animator GhostAni;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, IdleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime;
    public bool walking, chasing, searching;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;


    // Start is called before the first frame update
    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destination[randNum];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                /*GhostAni.ResetTrigger("Walk");
                GhostAni.ResetTrigger("Idle");
                GhostAni.SetTrigger("Sprint");*/
                chasing = true;
            }
        }

        if (chasing == true)
        {
            dest = player.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = chaseSpeed;
            if(enemyGhost.remainingDistance <= catchDistance)
            {
                player.gameObject.SetActive(false);
                /*GhostAni.ResetTrigger("Sprint");
                GhostAni.SetTrigger("Jumpscare");
                StartCoroutine(deathRoutine());*/
                chasing = false;
            }
        }

        if(searching == true)
        {
            dest = player.position;
            enemyGhost.destination = dest;
        }


        if(walking == true )
        {
            dest = currentDest.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            if(enemyGhost.remainingDistance <= enemyGhost.stoppingDistance )
            {
                /* GhostAni.ResetTrigger("Walk");
                    GhostAni.SetTrigger("Idle");*/
                enemyGhost.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }

    IEnumerator stayIdle()
    {
        IdleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(IdleTime);
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest= destination[randNum];
       /* GhostAni.ResetTrigger("Idle");
        GhostAni.SetTrigger("Walk");*/
    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destination[randNum];
        /*GhostAni.ResetTrigger("Sprint");
        GhostAni.SetTrigger("Walk");*/
    }
}
