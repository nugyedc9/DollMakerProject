using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostChaseState : StateGhost
{
    public NavMeshAgent enemyGhost;
    public Animator GhostAni;
    public float chaseSpeed, catchDistance, chaseTime, sightDistance, IdleTime;
    public bool walking, chasing, searching, stopSearch, Attacked, Idle;
    public Transform player;
    public GameObject DeadCanva;
    Vector3 dest;
    public Vector3 rayCastOffset;
    [Header("Ghost")]
    public GameObject BlackSphere;
    public GameObject GhostFrom;
    [Header("Player")]
    [SerializeField] PlayerHp P;
    public float DamageGhost;

    public GhostWalkState WalkState;

    public override StateGhost RunCurrentState()
    {
        if(walking)
        {
            return WalkState;
        }
        else
        return this;
    }

    private void Update()
    {
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
                StartCoroutine("chaseRoutine");
            }
        }

        if (chasing)
        {
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            searching = false;
            dest = player.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = chaseSpeed;
            if (enemyGhost.remainingDistance <= catchDistance)
            {
                P.Takedamage(DamageGhost);
                /*GhostAni.ResetTrigger("Sprint");
                GhostAni.SetTrigger("Jumpscare");
                StartCoroutine(deathRoutine());*/
            }
        }
    }
    IEnumerator chaseRoutine()
    {
        print("ChaseGhost");
        yield return new WaitForSeconds(chaseTime);
        chasing = false;
        yield return new WaitForSeconds(IdleTime);
        walking = true;
        Idle = false;
        /*GhostAni.ResetTrigger("Sprint");
        GhostAni.SetTrigger("Walk");*/
    }
}
