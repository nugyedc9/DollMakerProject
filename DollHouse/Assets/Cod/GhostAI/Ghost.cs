using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum StateGhost { Walk,Idle, Search, Hunt,HitP,PHit,ChangePosition , Dead};
public class Ghost : MonoBehaviour, HearPlayer
{

    public NavMeshAgent enemyGhost;
    public List<Transform> destination;
    public Animator GhostAni;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, IdleTime, 
        catchDistance, chaseTime, DistanceAmount, HpGhost, Stun, curStun , lowSpeed;
    public bool walking, chasing, searching,stopSearch , Attacked, getHit;
    public Transform player;
    public Vector3 LastSound;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int FirstDest,destinationAmount;
    public Vector3 rayCastOffset;

    [Header("Player")]
    [SerializeField] PlayerHp P;
    public float DamageGhost;

    [Header("Ghost")]
    public GameObject BlackSphere;
    public GameObject GhostFrom;
    public BoxCollider GhostCloseDistance;

    [Header("Ghost spawn")]
    public Vector3 GhostTransFrom;
    public Vector3 Spawn1, Spawn2, Spawn3;


    [Header("GhostView")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject PlayerPos;
    public LayerMask layerPLayer;
    public LayerMask obstructionMask;
    public LayerMask ground;
    public bool canSeePlayer;

    [Header("audio")]
    public AudioSource AudioGhost;
    public AudioSource FoundPlayer;
    public AudioClip MistGhost;
    public AudioSource ChaseGhost;
    public AudioSource DiedGhost;

    private StateGhost _stateGhost;
    bool Stay, Box, Tun, NearSpawn1, NearSpawn2, NearSpawn3, Poutrange;


    // Start is called before the first frame update
    void Start()
    {
        randNum = Random.Range(FirstDest, destinationAmount);
        currentDest = destination[randNum];
        curStun = Stun;
        PlayerPos = GameObject.FindGameObjectWithTag("Player");
        AudioGhost.enabled = true;  
        FoundPlayer.enabled = false;
        DiedGhost.enabled = false;
        lowSpeed = chaseSpeed;
        playerNearSpawn1();
        _stateGhost = StateGhost.Walk;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FovRountine());
        DistanceAmount = enemyGhost.remainingDistance;

        if(HpGhost < 0)
            HpGhost = 0;
        if(HpGhost == 0)
        {
            StopAllCoroutines();
            _stateGhost = StateGhost.Dead;
        }

        #region BoxcolliderActive
        if (enemyGhost.remainingDistance <= 0.3f)
        {
            GhostCloseDistance.enabled = false;
            Box = false;
        }
        else
        {
            GhostCloseDistance.enabled = true;
            Box = true;
        }
        #endregion


        if(_stateGhost == StateGhost.Walk)
        {
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            dest = currentDest.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            AudioGhost.enabled = true;
            if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance)
            {
                Stay = true;
                Debug.Log("IdleState after walk");
                _stateGhost = StateGhost.Idle;
            }
        }

        if(_stateGhost == StateGhost.Idle)
        { ChaseGhost.enabled = false;
            FoundPlayer.enabled = false;
            StartCoroutine(stayIdle());
        }

        if(_stateGhost == StateGhost.Search)
        {
            Debug.Log("SearchState");
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            dest = LastSound;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance)
            {
                Stay = true;
                Debug.Log("IdleState after search");
                _stateGhost = StateGhost.Idle;
            }
        }

        if (Poutrange)
        {
            _stateGhost = StateGhost.Search;
            Poutrange = false;
        }

        if (_stateGhost == StateGhost.Hunt)
        {
            Debug.Log("HuntState");
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            dest = player.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = chaseSpeed;
            GhostAni.SetTrigger("Run");
            ChaseGhost.enabled = true;
            FoundPlayer.enabled = true;
            if (enemyGhost.remainingDistance <= catchDistance && enemyGhost.remainingDistance != 0 && Box)
            {
                Attacked = true;
                P.Takedamage(DamageGhost);
                _stateGhost = StateGhost.HitP;
            }
        }

        if( _stateGhost == StateGhost.HitP)
        {
            stopSearch = true;
            enemyGhost.speed = 0;
            GhostAni.SetTrigger("HitPlayer");
            StartCoroutine(Attack());
        }
        
        if(_stateGhost == StateGhost.PHit)
        {
            float lowSpeed;
            lowSpeed = chaseSpeed;
            lowSpeed -= 0.2f * Time.deltaTime;
            enemyGhost.speed = lowSpeed;
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            if(lowSpeed < 0.5)
            {
                lowSpeed = 0.5f;
            }

           /* StunTime(1);
            Tun = true;
            stopSearch = true;
            if (curStun <= 0)
            {
                StopAllCoroutines();
                StartCoroutine("Hit");
                curStun = 0;
            }*/

        }
        if (curStun == 0)
        {
            curStun = Stun;
        }

        if(_stateGhost == StateGhost.ChangePosition)
        {
            if (NearSpawn1 && !NearSpawn2 && !NearSpawn3)
            {
                playerNearSpawn2();
            }
            else if (!NearSpawn1 && NearSpawn2 && !NearSpawn3)
            {
                //  playerNearSpawn3();
                playerNearSpawn1();
            }
            else if (!NearSpawn1 && !NearSpawn2 && NearSpawn3)
            {
                playerNearSpawn1();
            }
            Debug.Log("IdleState after changepos");
            _stateGhost = StateGhost.Idle;
        }

        if(_stateGhost == StateGhost.Dead)
        {
            getHit = false;
            stopSearch = true;
            chasing = false;
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            GhostAni.SetTrigger("Dead");
            StunTime(1);
            DiedGhost.enabled = true;
            if (curStun <= 0)
            {
                Destroy(gameObject);
            }
        }

        #region Old Code
        /* #region Chase
         if (chasing == true)
         {
             BlackSphere.SetActive(false);
             GhostFrom.SetActive(true);
             searching = false;
             dest = player.position; 
             enemyGhost.destination = dest;
             enemyGhost.speed = chaseSpeed;
             ChaseGhost.enabled = true;
             if(enemyGhost.remainingDistance <= catchDistance && enemyGhost.remainingDistance != 0)
             {
                 P.Takedamage(DamageGhost);
                 StopAllCoroutines();
                 StartCoroutine("Attack");
                /* GhostAni.ResetTrigger("Sprint");
                 GhostAni.SetTrigger("Jumpscare");
                 StartCoroutine(deathRoutine());
                 chasing = false;
             }
         }else ChaseGhost.enabled = false;
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
             AudioGhost.enabled = true;

             if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance)
             {
                 // GhostAni.ResetTrigger("Walk");
                     GhostAni.Play("Idle");
                 enemyGhost.speed = 0;
                 StopAllCoroutines();
                 StartCoroutine(stayIdle());
                 walking = false;
             }
         }
         else AudioGhost.enabled = false;


         /*if(_stateGhost == StateGhost.Mist)
         {

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
             FoundPlayer.enabled = true;
             if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance )
             { 
                 enemyGhost.speed = 0;
                 StopAllCoroutines();
                 StartCoroutine("StartSearch");
                 searching=false;
             }

         }
         else StartCoroutine(WaitFoundSound());
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

         */
        #endregion
    }
    
    public void RespondToSound(Sound sound)
    {
        //Debug.Log(name + " read sound" +  sound.pos);
        if (!stopSearch)
        {
            LastSound = sound.pos;
            _stateGhost = StateGhost.Search;
        }
    }

    #region Ienummerator

    #region Idle
    IEnumerator stayIdle()
    {
       // yield return new WaitForSeconds(IdleTime); 
        if (Stay)
        {
            Debug.Log("stay");
            IdleTime = Random.Range(minIdleTime, maxIdleTime); 
            randNum = Random.Range(FirstDest, destinationAmount);
            currentDest = destination[randNum];
            Stay = false;
        }
        yield return new WaitForSeconds(IdleTime);
        _stateGhost = StateGhost.Walk;
        /*    GhostAni.ResetTrigger("Idle");
            GhostAni.SetTrigger("Walk");*/
    }
    #endregion

    #region CheaseRoutine
    IEnumerator chaseRoutine()
    {
       // print("ChaseGhost");
        yield return new WaitForSeconds(chaseTime);
        chasing = false;
        _stateGhost = StateGhost.Idle;
      /*  GhostAni.ResetTrigger("Sprint");
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
        stopSearch = true;
        enemyGhost.speed = 0;
        yield return new WaitForSeconds(3);
        stopSearch = false;
        Attacked = false;
        _stateGhost = StateGhost.Idle;
        
    }
    #endregion

    #region Hit
    IEnumerator Hit()
    {
       print("HitGhost");
        yield return new WaitForSeconds(1);
        /* stopSearch = false;
         walking = true;
         getHit = false;*/
        Tun = false;
        stopSearch = false;
        _stateGhost = StateGhost.Idle;

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


    #region SpawnPoint

    public void playerNearSpawn1()
    {
        Debug.LogError("Sapwn1");
        GhostTransFrom = Spawn1;
        FirstDest = 0;
        destinationAmount = 3;
        NearSpawn1 = true; NearSpawn2 = false; NearSpawn3 = false;
    }
    public void playerNearSpawn2()
    {
        Debug.LogError("Sapwn2");
        GhostTransFrom = Spawn2;
        FirstDest = 4;
        destinationAmount = 6;
        NearSpawn1 = false; NearSpawn2 = true; NearSpawn3 = false;
    }
   /* public void playerNearSpawn3()
    {
        Debug.LogError("Sapwn3");
        GhostTransFrom.position = Spawn3.position;
        FirstDest = 7;
        destinationAmount = 9;
        NearSpawn1 = false; NearSpawn2 = false; NearSpawn3 = true;
    }*/

    #endregion

    #region CollEnter

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Orb")
        {
            lowSpeed = chaseSpeed;
            lowSpeed -= 0.5f * Time.deltaTime;
            enemyGhost.speed = lowSpeed;
            if (lowSpeed < 0.5)
            {
                lowSpeed = 0.5f;
            }
        }
    }

    #endregion

    public void PlayerHitGhost()
    {
        lowSpeed -= 0.5f * Time.deltaTime;
        enemyGhost.speed = lowSpeed;    
        if (lowSpeed < 0.5)
        {
            lowSpeed = 0.5f;
            _stateGhost = StateGhost.ChangePosition;
        }
    }

    public void StunTime(float St)
    {
        curStun -= St * Time.deltaTime;
    }

    #region Ghostview

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
                    /* canSeePlayer = true;
                     if (!Attacked)
                     {
                         walking = false;
                         StopAllCoroutines();
                         StartCoroutine(chaseRoutine());
                         chasing = true;
                     }*/
                    canSeePlayer = true;
                    if (!Attacked && !Tun && canSeePlayer)
                    {
                        _stateGhost = StateGhost.Hunt;
                        stopSearch = true;
                        Poutrange = false;

                    }
                }
                else
                {
                    canSeePlayer = false;
                }
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ground))
                {
                    /*  canSeePlayer = true;
                      if (!Attacked)
                      {
                          walking = false;
                          StopAllCoroutines();
                          StartCoroutine(chaseRoutine());
                          chasing = true;
                      }*/
                    canSeePlayer = true;
                    if (!Attacked && !Tun && canSeePlayer)
                    {
                        _stateGhost = StateGhost.Hunt;
                        stopSearch = true;
                        Poutrange = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else
        {
            canSeePlayer = false;
        }
    }

    #endregion


}
