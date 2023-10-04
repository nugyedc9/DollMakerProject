using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour, HearPlayer
{

    public NavMeshAgent enemyGhost;
    public List<Transform> destination;
    public Animator GhostAni;
    public float walkSpeed, chaseSpeed,AfterHitSpeed, minIdleTime, maxIdleTime, IdleTime, sightDistance, 
        catchDistance, chaseTime, DistanceAmount, HpGhost, Stun, curStun;
    public bool walking, chasing, searching,stopSearch , Attacked, getHit;
    public Transform player;
    public Vector3 LastSound;
    public GameObject DeadCanva;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;

    [Header("Player")]
    [SerializeField] PlayerHp P;
    public float DamageGhost;

    [Header("Ghost")]
    public GameObject BlackSphere;
    public GameObject GhostFrom;

    [Header("GhostView")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject PlayerPos;
    public LayerMask layerPLayer;
    public LayerMask obstructionMask;
    public bool canSeePlayer;





    // Start is called before the first frame update
    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destination[randNum];
        curStun = Stun;
        PlayerPos = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FovRountine());
        DistanceAmount = enemyGhost.remainingDistance;

       /* Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.forward , direction, out hit, sightDistance))
        {
            if (!Attacked)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    walking = false;
                    StopAllCoroutines();
                    StartCoroutine("chaseRoutine");
                    /*GhostAni.ResetTrigger("Walk");
                    GhostAni.ResetTrigger("Idle");
                    GhostAni.SetTrigger("Sprint");
                    chasing = true;
                }

            }

        }*/


        #region Chase
        if (chasing == true)
        {
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            searching = false;
            dest = player.position; 
            enemyGhost.destination = dest;
            enemyGhost.speed = chaseSpeed;
            if(enemyGhost.remainingDistance <= catchDistance && enemyGhost.remainingDistance != 0)
            {
                P.Takedamage(DamageGhost);
                StopAllCoroutines();
                StartCoroutine("Attack");
                /*GhostAni.ResetTrigger("Sprint");
                GhostAni.SetTrigger("Jumpscare");
                StartCoroutine(deathRoutine());*/
                chasing = false;
            }
        }
        #endregion

        #region Walk
        if (walking == true )
        {
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            chasing = false;
            dest = currentDest.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance)
            {
                /* GhostAni.ResetTrigger("Walk");
                    GhostAni.SetTrigger("Idle");*/
                enemyGhost.speed = 0;
                StopAllCoroutines();
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
        #endregion

        #region Search
        if (searching == true)  
        {
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            dest = LastSound;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance )
            { 
                enemyGhost.speed = 0;
                StopAllCoroutines();
                StartCoroutine("StartSearch");
                searching=false;
            }

        }
        #endregion

        #region GetHit
        if (getHit == true)
        {
            enemyGhost.speed=0;
            Attacked = false;
            walking = false;
            chasing = false;
            searching = false;
            stopSearch = true;
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            StunTime(1);
            if (curStun <= 0)
            {
                StopAllCoroutines();
                StartCoroutine("Hit");
                curStun = 0;
            }
        }

        if (curStun == 0)
        {
            curStun = Stun;
            getHit = false;
        }
        #endregion
    }

    public void RespondToSound(Sound sound)
    {
        //print(name + " read sound" +  sound.pos);
        if (stopSearch == false)
        {
            LastSound = sound.pos;
            searching = true;
            walking = false;
        }
    }

    #region Ienummerator
    #region Idle
    IEnumerator stayIdle()
    {
        print("IdleGhost");
        IdleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(IdleTime);
        walking = true;
        stopSearch=false;
        Attacked=false;
        randNum = Random.Range(0, destinationAmount);
        currentDest= destination[randNum];
       /* GhostAni.ResetTrigger("Idle");
        GhostAni.SetTrigger("Walk");*/
    }
    #endregion

    #region CheaseRoutine
    IEnumerator chaseRoutine()
    {
       // print("ChaseGhost");
        yield return new WaitForSeconds(chaseTime);
        chasing = false;

        yield return new WaitForSeconds(IdleTime);
        walking= true;
        /*GhostAni.ResetTrigger("Sprint");
        GhostAni.SetTrigger("Walk");*/
    }
    #endregion

    #region StartSearch
    IEnumerator StartSearch()
    {
        print("StartSearch");
        yield return new WaitForSeconds(2);
        walking = true;
        searching = false;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destination[randNum];
    }
    #endregion

    #region Attack
    IEnumerator Attack()
    {
        print("AttackGhost");
        Attacked = true;
        walking = false;
        chasing = false;
        searching = false;
        stopSearch = true;
        enemyGhost.speed = AfterHitSpeed;
        /*GhostAni.ResetTrigger("Sprint");
        GhostAni.SetTrigger("Rawr");*/
        yield return new WaitForSeconds(2);
        walking = true;
        stopSearch= false;
        Attacked = false;
    }
    #endregion

    #region Hit
    IEnumerator Hit()
    {
        print("HitGhost");
        yield return new WaitForSeconds(1);
        stopSearch = false;
        walking = true;
        getHit = false;
    }
    #endregion

    IEnumerator FovRountine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }


    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Orb")
        {
            getHit = true;
            if(getHit) 
            print("GetHit");
            Destroy(collision.gameObject);
        }
    }

    public void StunTime(float St)
    {
        curStun -= St * Time.deltaTime;
    }

    public void FieldOfViewCheck()
    {

        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, layerPLayer);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    if (!Attacked)
                    {
                        walking = false;
                        StopAllCoroutines();
                        StartCoroutine(chaseRoutine());
                        chasing = true;
                    }
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine("stayIdle");
                    canSeePlayer = false;

                }
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine("stayIdle");
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            StopAllCoroutines();
            StartCoroutine("stayIdle");
            canSeePlayer = false;
        }
    }


}
