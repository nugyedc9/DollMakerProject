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
    [SerializeField] public Animator GhostAni;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, IdleTime, 
        catchDistance, chaseTime, DistanceAmount, HpGhost, Stun, curStun , lowSpeed;
    public bool walking, chasing, searching,stopSearch , Attacked, getHit,ded,getAttack;
    public Transform player;
    public Vector3 LastSound;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int FirstDest,destinationAmount;
    public Vector3 rayCastOffset;

    [Header("Player")]
    [SerializeField] PlayerHp HpPlayer;
    public float DamageGhost;

    [Header("Ghost")]
    public GameObject BlackSphere;
    public GameObject GhostFrom;
    public BoxCollider GhostCloseDistance;

    [Header("Ghost spawn")]
    public Transform GhostTransFrom;
    public Transform Spawn1, Spawn2, Spawn3;


    /*[Header("GhostView")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject PlayerPos;
    public LayerMask layerPLayer;
    public LayerMask obstructionMask;
    public LayerMask ground;
    public bool canSeePlayer;*/

    [Header("Ghost vision cone")]
    public Material VisionConeMaterial;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;
    public LayerMask PlayerLayer;
    public int VisionConeResolution = 120;
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;

    [Header("audio")]
    public AudioSource FoundPlayer;
    public AudioSource MistGhost;
    public AudioSource ChaseGhost;
    public AudioSource DiedGhost;
    bool chaseC, DiedC;

    private StateGhost _stateGhost;
    bool Stay, Box, Tun, PAttack, cansee;


    // Start is called before the first frame update
    void Start()
    {
        enemyGhost = GetComponent<NavMeshAgent>();
        randNum = Random.Range(FirstDest, destinationAmount);
        currentDest = destination[randNum];
        curStun = Stun;
       // PlayerPos = GameObject.FindGameObjectWithTag("Player"); 
        FoundPlayer.enabled = false;
        lowSpeed = chaseSpeed;

        #region Vision Cone
        cansee = true;
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
        #endregion

        playerNearSpawn1();
        _stateGhost = StateGhost.Walk;
    }

    // Update is called once per frame
    void Update()
    {
       // StartCoroutine(FovRountine());
        DrawVisionCone();
        DistanceAmount = enemyGhost.remainingDistance;

        if(HpGhost < 0)
            HpGhost = 0;
        if(HpGhost == 0)
        {
            StopAllCoroutines();
            _stateGhost = StateGhost.Dead;
        }

        if(curStun ==0) curStun = Stun;

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
            stopSearch = false;
            dest = currentDest.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance)
            {
                Stay = true;
                //Debug.Log("IdleState after walk");
                _stateGhost = StateGhost.Idle;
            }
        }

        if(_stateGhost == StateGhost.Idle)
        {
            MistGhost.enabled = true;
            ChaseGhost.enabled = false;
            FoundPlayer.enabled = false;
            DiedGhost.enabled = false;
            StartCoroutine(stayIdle());
        } 

        if(_stateGhost == StateGhost.Search)
        {
          //  Debug.Log("SearchState");
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            dest = LastSound;
            enemyGhost.destination = dest;
            enemyGhost.speed = walkSpeed;
            if (enemyGhost.remainingDistance <= enemyGhost.stoppingDistance)
            {
                Stay = true;
               // Debug.Log("IdleState after search");
                _stateGhost = StateGhost.Idle;
            }
        }


        if (_stateGhost == StateGhost.Hunt)
        {
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            ChaseGhost.enabled = true;
            FoundPlayer.enabled = true;
            dest = player.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = chaseSpeed;
            if (!chasing)
            {
                GhostAni.Play("G_Run", 0, 0);
                chasing = true;
            }
           // GhostAni.SetTrigger("Run");
            if (enemyGhost.remainingDistance <= catchDistance && enemyGhost.remainingDistance != 0 && Box)
            {
                Tun = true;
                HpPlayer.Takedamage(DamageGhost);
                if (!Attacked)
                {
                    GhostAni.Play("G_atk", 0, 0);
                    Attacked = true;
                }
              //  GhostAni.SetTrigger("HitPlayer");
                _stateGhost = StateGhost.HitP;
            }
            /*if (!canSeePlayer)
            {
                _stateGhost = StateGhost.Idle;
            }*/
        }

        if( _stateGhost == StateGhost.HitP)
        {
            stopSearch = true;
            if(Attacked)
            {
                HpPlayer.Takedamage(DamageGhost);
                Attacked = false;
            }
            enemyGhost.speed = 0;    
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

        if(_stateGhost == StateGhost.ChangePosition)
        {
            getHit = false;
            getAttack = false;
            DiedGhost.enabled = true;
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            GhostCloseDistance.enabled = false;
            Tun = true;
            HpGhost = 1;
            StartCoroutine(DelayChagePos());
           // Debug.Log("IdleState after changepos");
            lowSpeed = chaseSpeed;
            _stateGhost = StateGhost.Idle;
        }

        if(_stateGhost == StateGhost.Dead)
        {
            getHit = false;
            stopSearch = true;
            chasing = false;
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            if (!ded)
            {
                GhostAni.Play("G_dead", 0, 0);
                ded = true;
            }
           // GhostAni.SetTrigger("Dead");
            StunTime();
            DiedGhost.enabled = true;
            if (curStun <= 0)
            {
                Destroy(gameObject);
            }
        }

        #region Animation play
        if(_stateGhost != StateGhost.Hunt) chasing = false;
        if (_stateGhost != StateGhost.Dead) ded = false;
        if (_stateGhost != StateGhost.HitP) Attacked = false;
        #endregion

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
        Tun = false;
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

   /* IEnumerator FovRountine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }*/

    IEnumerator DelayChagePos()
    {
        enemyGhost.speed = 0;
        yield return new WaitForSeconds(2);
        GhostCloseDistance.enabled = true;
        Tun = false;
        getHit = true;
        cansee = true;
        lowSpeed = chaseSpeed;
        _stateGhost = StateGhost.Idle;
        playerNearSpawn2();
    }

    #endregion


    #region SpawnPoint

    public void playerNearSpawn1()
    {
       // Debug.LogError("Sapwn1");
        enemyGhost.Warp(Spawn1.position);
        FirstDest = 0;
        destinationAmount = 3;
    }
    public void playerNearSpawn2()
    {
        //Debug.LogError("Sapwn2");
        enemyGhost.Warp(Spawn2.position);
        FirstDest = 4;
        destinationAmount = 6; 
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
            lowSpeed -= 1f * Time.deltaTime;
            enemyGhost.speed = lowSpeed;
            if (lowSpeed < 1)
            {
                lowSpeed = 1f; 
            }
        }
    }

    #endregion

    public void PlayerHitGhost()
    {
        if (getHit)
        {
            lowSpeed -= 1.3f * Time.deltaTime;
            enemyGhost.speed = lowSpeed;
            if (!getAttack)
            {
                GhostAni.Play("G_getatk", 0, 0);
                getAttack = true;
            }
            //GhostAni.SetTrigger("Phit");
            PAttack = true;
            if (lowSpeed < 1)
            {
                if (HpGhost == 1) HpGhost = 0;
                cansee = false;
                _stateGhost = StateGhost.ChangePosition;
            }

        }
    }

    public void PAttackTofalse() { PAttack = false; }

    public void StunTime()
    {
        curStun -= 1 * Time.deltaTime;
    }

    #region Ghostview

    /*public void FieldOfViewCheck()
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
                     }

                    canSeePlayer = true;
                    if (!Attacked && !Tun && canSeePlayer)
                    {
                        ChaseGhost.enabled = true;
                        _stateGhost = StateGhost.Hunt;
                        stopSearch = true;
                        FoundPlayer.enabled = true;
                        MistGhost.enabled = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                    _stateGhost = StateGhost.Search;
                }
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ground))
                {
                    canSeePlayer = true;
                    if (!Attacked && !Tun && canSeePlayer)
                    {
                        ChaseGhost.enabled = true;
                        _stateGhost = StateGhost.Hunt;
                        stopSearch = true;
                        MistGhost.enabled = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                    _stateGhost = StateGhost.Search;
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
    }*/

    #endregion

    #region Vision Cone
    void DrawVisionCone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * VisionRange;
            }

            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit PlayerHitRay, VisionRange, PlayerLayer))
            {
                Vertices[i + 1] = VertForward * PlayerHitRay.distance;
                if(cansee)
                _stateGhost = StateGhost.Hunt;
            }


            Currentangle += angleIcrement;
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
    }
    #endregion
}
