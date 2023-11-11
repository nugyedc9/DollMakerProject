using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum StateGhost { Walk, Idle, Search, Hunt, HitP, ChangePosition, Dead };
public class Ghost : MonoBehaviour, HearPlayer
{

    public NavMeshAgent enemyGhost;
    public List<Transform> destination;
    [SerializeField] public Animator GhostAni;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, IdleTime,
        catchDistance, chaseTime, DistanceAmount, HpGhost, Stun, curStun, lowSpeed;
    private bool   ChaseCheck,
        chasing, searching, stopSearch, Attacked, getHit,
        ded, HpLow, getAttack, cansee;
    public Transform player;
    public Vector3 LastSound;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int FirstDest, destinationAmount;
    public Vector3 rayCastOffset;

    [Header("Player")]
    [SerializeField] PlayerHp HpPlayer;
    [SerializeField] PlayerAttack PAttack;
    public float DamageGhost;

    [Header("Ghost")]
    public GameObject BlackSphere;
    public GameObject GhostFrom;
    public BoxCollider GhostCloseDistance;

    [Header("Ghost spawn")]
    public Transform GhostTransFrom;
    public Transform Spawn1, Spawn2, Spawn3;
    private float ToSpawn;


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

    private StateGhost _stateGhost;
    bool Stay, Box, Phit, StopCount;


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
        ToSpawn = 0;

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

     /*   if (HpGhost < 1)
        {
            StopAllCoroutines();
            _stateGhost = StateGhost.Dead;
        }*/

        if (curStun == 0) curStun = Stun;

        #region BoxcolliderActive
        if (enemyGhost.remainingDistance <= 0.3f)
        {
            GhostCloseDistance.enabled = false;
            Box = false;
        }
        {
            if (!Phit)
            {
                GhostCloseDistance.enabled = true;
                Box = true;
            }
        }
        #endregion


        if (_stateGhost == StateGhost.Walk)
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

        if (_stateGhost == StateGhost.Idle)
        {
            stopSearch = false;
            cansee = true;
            getHit = false;
            MistGhost.enabled = true;
            ChaseGhost.enabled = false;
            FoundPlayer.enabled = false;
            DiedGhost.enabled = false;
            StartCoroutine(stayIdle());
        }

        if (_stateGhost == StateGhost.Search)
        {
            //  Debug.Log("SearchState");
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
            ChaseCheck = true;
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            ChaseGhost.enabled = true;
            FoundPlayer.enabled = true;
            dest = player.position;
            enemyGhost.destination = dest;
            enemyGhost.speed = chaseSpeed;
            getAttack = false;
            if (!chasing)
            {
                if (!GhostAni.GetCurrentAnimatorStateInfo(0).IsName("G_Run"))
                    GhostAni.Play("G_Run", 0, 0);
                chasing = true;
            }
            // GhostAni.SetTrigger("Run");
            if (enemyGhost.remainingDistance <= catchDistance && enemyGhost.remainingDistance != 0 && Box)
            {
                cansee = false;
                if (!Attacked)
                {
                    GhostAni.Play("G_atk", 0, 0);
                    Attacked = true;
                }
                //  GhostAni.SetTrigger("HitPlayer");
                _stateGhost = StateGhost.HitP;
            }
        }
        else ChaseCheck = false;

        if (_stateGhost == StateGhost.HitP)
        {
            stopSearch = true;
            enemyGhost.speed = 0;
            if (Attacked)
            {
                HpPlayer.Takedamage(DamageGhost);
                Attacked = false;
            }
            StartCoroutine(Attack());
        }

        if (lowSpeed < 1)
        {
            Phit = true;
            getHit = true;
            stopSearch = true;
            GhostCloseDistance.enabled = false;
            StopAllCoroutines();
            if (!HpLow)
            {
                HpGhost--;
                ToSpawn++;
                PAttack.CrossRuin();
                HpLow = true;
            }
            cansee = false;
            enemyGhost.speed = 0;
            lowSpeed = chaseSpeed;
            if (ToSpawn != 3)
                _stateGhost = StateGhost.ChangePosition;
            else _stateGhost = StateGhost.Dead;
        }

        if (_stateGhost == StateGhost.ChangePosition)
        {
            enemyGhost.speed = 0;
            HpLow = false;
            getAttack = false;
            chasing = false;
            BlackSphere.SetActive(true);
            GhostFrom.SetActive(false);
            DiedGhost.enabled = true;
            StartCoroutine(DelayChagePos());
            // Debug.Log("IdleState after changepos");
        }

        if (_stateGhost == StateGhost.Dead)
        {
            cansee = false;
            enemyGhost.speed = 0;
            HpLow = false;
            getAttack = false;
            chasing = false;
            if (!ded)
            {
                GhostFrom.SetActive(true);
                if (!GhostAni.GetCurrentAnimatorStateInfo(0).IsName("G_dead"))
                    GhostAni.Play("G_dead", 0, 0);
                DiedGhost.enabled = true;
                ded = true;
            }
            if(!StopCount) 
            StunTime();
            if (curStun < 0)
            {
                FoundPlayer.enabled = false;
                DiedGhost.enabled = false;
                MistGhost.enabled = false;
                ChaseGhost.enabled = false;

                BlackSphere.SetActive (false);
                GhostFrom.SetActive (false);
                StartCoroutine(AfterDead());
                curStun = 0;
                StopCount = true;
            }
            // GhostAni.SetTrigger("Dead");
        }


        #region Animation play
        if (_stateGhost != StateGhost.Dead) {ded = false; StopCount = false;}
        if (_stateGhost != StateGhost.Hunt) chasing = false;
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
            // Debug.Log("stay");
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



    #region Attack
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(2);
        _stateGhost = StateGhost.Idle;

    }
    #endregion

    IEnumerator DelayChagePos()
    {
        yield return new WaitForSeconds(2);
        GhostCloseDistance.enabled = true;
        getHit = false;
        cansee = true;
        Phit = false;
        if (ToSpawn == 1)
        {
            playerNearSpawn2();
            _stateGhost = StateGhost.Idle;
        }
        if (ToSpawn == 2)
        {
            playerNearSpawn3();
            _stateGhost = StateGhost.Idle;
        }
    }

    IEnumerator AfterDead()
    {
        yield return new WaitForSeconds(20);
        playerNearSpawn1();
        Phit = false;
        ToSpawn = 0;
        _stateGhost = StateGhost.Idle;
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
    public void playerNearSpawn3()
    {
        GhostTransFrom.position = Spawn3.position;
        FirstDest = 7;
        destinationAmount = 9;
    }

    #endregion

    
    public void PlayerHitGhost()
    {
        if (!getHit)
        {
            lowSpeed -= 8f * Time.deltaTime;
            enemyGhost.speed = lowSpeed;
            if (!getAttack)
            {
                if (!GhostAni.GetCurrentAnimatorStateInfo(0).IsName("G_getatk"))
                    GhostAni.Play("G_getatk", 0, 0);
                getAttack = true;
            }
            //GhostAni.SetTrigger("Phit");
        }
    }


    public void StunTime()
    {
        curStun -= 1 * Time.deltaTime;
    }


    #region Vision Cone
   private bool PlayerInsight;

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
                if (cansee)
                {
                    if (curStun > 1)
                        PlayerInsight = true;
                    StopAllCoroutines();
                    _stateGhost = StateGhost.Hunt;
                }
            }
            else
            {
                if (PlayerInsight)
                {
                    StunTime();
                    if (curStun < 1)
                    {
                        PlayerInsight = false;
                    }
                }

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
