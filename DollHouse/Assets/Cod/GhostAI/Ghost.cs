using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum StateGhost { Mist, Search, Hunt, Dead};
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
    public BoxCollider GhostCloseDistance;

    [Header("GhostView")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject PlayerPos;
    public LayerMask layerPLayer;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    [Header("audio")]
    public AudioSource AudioGhost;
    public AudioSource FoundPlayer;
    public AudioClip MistGhost;
    public AudioSource ChaseGhost;
    public AudioSource DiedGhost;

    private StateGhost _stateGhost;


    // Start is called before the first frame update
    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destination[randNum];
        curStun = Stun;
        PlayerPos = GameObject.FindGameObjectWithTag("Player");
        AudioGhost.enabled = true;
        FoundPlayer.enabled = false;
        DiedGhost.enabled = false;
       // _stateGhost = StateGhost.Mist;
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
            getHit = false;
            stopSearch = true;
            chasing = false;
            BlackSphere.SetActive(false);
            GhostFrom.SetActive(true);
            StopAllCoroutines();
            GhostAni.SetTrigger("Dead");
            StunTime(1);
            DiedGhost.enabled = true;
            if (curStun <= 0)
            {
                Destroy(gameObject);
            }

        }

        #region BoxcolliderActive
        if (enemyGhost.remainingDistance <= 0.3f)
            GhostCloseDistance.enabled = false;
        else
        GhostCloseDistance.enabled=true;
        #endregion

        #region Chase
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
                /*GhostAni.ResetTrigger("Sprint");
                GhostAni.SetTrigger("Jumpscare");
                StartCoroutine(deathRoutine());*/
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
                /* GhostAni.ResetTrigger("Walk");
                    GhostAni.SetTrigger("Idle");*/
                enemyGhost.speed = 0;
                StopAllCoroutines();
                StartCoroutine(stayIdle());
                walking = false;
            }
        }
        else AudioGhost.enabled = false;
       

        /*if(_stateGhost == StateGhost.Mist)
        {

        }*/
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

    IEnumerator WaitFoundSound()
    {
        yield return new WaitForSeconds(1);
        FoundPlayer.enabled = false;
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Orb")
        {
            getHit = true;
            if(getHit) 
            print("GetHit");
            HpGhost -= 1;
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
           /* else
            {
                StopAllCoroutines();
                StartCoroutine("stayIdle");
                canSeePlayer = false;
            }*/
        }
        /*else if (canSeePlayer)
        {
            StopAllCoroutines();
            StartCoroutine("stayIdle");
            canSeePlayer = false;
        }*/
    }


}
