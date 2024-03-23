using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class GhostStateManager : MonoBehaviour
{

    GhostBaseState CurrentState;
    public GhsotIdleState IdleState = new GhsotIdleState();
    public GhostWalkState WalkState = new GhostWalkState();
    public GhostAlertState AlertState = new GhostAlertState();
    public GhostHuntState HuntState = new GhostHuntState();
    public GhostAttackState AttckState = new GhostAttackState();
    public GhostSearchState SearchState = new GhostSearchState();
    public GhostGetAttckState GetAtKState = new GhostGetAttckState();
    public GhostDiedState DiedState = new GhostDiedState();
    public GhsotSpawnState SpawnState = new GhsotSpawnState();
    public GhsotDetectPlayerSpawn DetectPlayerState = new GhsotDetectPlayerSpawn();

    public bool ForTest;

    
    [Header("Player")]
    public PlayerHp HpPlayer;
    public PlayerAttack PAttack;
    public GameObject GhostHuntEffect;
    public bool HpCross;

    [Header("Ghost")]
    public NavMeshAgent enemyGhost;
    public BoxCollider GhostBoxCol;
    public ParticleSystem particle;
    public GameObject GhostFrom, GhostLight;
    public float DistanceAmount ,WalkSpeed, HuntSpeed;
    public bool RandomInIdle, PlayerInSight, CanseePlayer, HitPlayer,
        GetHit, GetAttack, ChangePos, PlayerDetectSpawn;

    [Header("GhostHP")]
    public float HpGhost;
    public float HpBeforeHit, HitDelay, GhostSpeedDown, GhostSpeedMin;
    public bool PlayerHit;

  [Header("Timer Thing")]
    public float SpawnTimer;
    public float RandomMinIdle, RandomMaxIdle, playerOutOfSight,
        DelayHitPlayer, PlayerHitDelay, ChangePosDelay;
    
    [Header("GhostAnimCheck")]
    public Animator GhostAni;
    public bool AnimWalk, AnimAlert, AnimHunt , AnimSpawn,AnimAttack
       , AlertSPlay;



    [Header("Ghost vision cone")]
    public GameObject HeadVistion;
    public Material VisionConeMaterial;
    public float VisionRange, HearRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;
    public LayerMask PlayerLayer;
    public int VisionConeResolution = 120;
    public bool Cansee;
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;

    [Header("Destinations")]
    public int CurSpawn;
    public List<Transform> destination;
    public List<Transform> destination2;
    public List<Transform> destination3;
    public Transform[] SpawnPoint;
    public Vector3 Dest;
    public Transform playerPos,CurrentDest;
    public int DestinationMin, DestinationMax;

  
    [Header("---- Audio Sound ----")]
    public AudioSource GhostAudioSoure; 
    public AudioSource GhostAmbi, OnPlayerAudio, FireSound, MoveSound, EffectHunt;
    public AudioClip  DetectS, SpawnS, WalkS, RunS, HuntS, AttackS, DiedS ,
        GetAttackS, FoundS, GhostIdleAmbiS, GhostHuntAmbi, PlayerHeartBeat;

    [Header("Event")]
    public UnityEvent EventGhostAfterDied;
    public UnityEvent GhostHuntLightOff, GhostOutSightLightOn;



    // Start is called before the first frame update
    void Start()
    {
        enemyGhost = GetComponent<NavMeshAgent>();
        CurrentState = DetectPlayerState;
        CurrentState.EnterState(this);
        curplayerOutSight = playerOutOfSight;
            particle.Stop();
        HpBeforeHit = HpGhost;
        

        #region Vision Cone
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        DistanceAmount = enemyGhost.remainingDistance;
        CurrentState.UpdateState(this);
        if (PlayerInSight)
        {
            playerOutOfSight -= Time.deltaTime; 
            DelayHitPlayer -= Time.deltaTime;

        }

        /*        if(PlayerHitDelay <= 0)
                {
                    if (!HpCross)
                    {
                        PAttack.CrossRuin();
                        GhostBoxCol.enabled = false;
                        SwitchState(GetAtKState);
                        HpCross = true;
                    }
                }  */

        if (ForTest)
        {
            if (Input.GetKeyDown(KeyCode.L)) SwitchState(SpawnState);
        }


        if (GetAttack)
        {
            SwitchState(GetAtKState);
            GetAttack = false;
        }


        if (HpGhost <= 0)
        {
                //  Debug.Log("Died");
                //PAttack.Attack = true;
                GhostHuntEffect .SetActive(false);  
                SwitchState(DiedState);           
        }

    }

    public void SwitchState(GhostBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }

    public void Playerhit()
    {

        /*PlayerHitDelay -= 8 * Time.deltaTime;
        enemyGhost.speed = HuntSpeed - Time.deltaTime;
        if (!GetAttack)
        {
            if (!GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Attack_ani"))
                GhostAni.Play("Attack_ani", 0, 0);
            HpGhost--;
            ChangePos = true;
            GetAttack = true;
        }*/

        if (HpBeforeHit == HpGhost)
        {
            GetAttack = true;
        }
        HitDelay = 0.5f;
            HpGhost -= Time.deltaTime;
        


        /* if (!GetAttack)
         {
           //  PAttack.CrossRuin();
             //GhostBoxCol.enabled = false;

             ChangePos = true;
             GetAttack = true;
         }*/
    }

    float curplayerOutSight;

   

    #region Vision
    public void DrawVisionCone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        if (Cansee)
        {
            for (int i = 0; i < VisionConeResolution; i++)
            {
                Sine = Mathf.Sin(Currentangle);
                Cosine = Mathf.Cos(Currentangle);
                Vector3 RaycastDirection = (HeadVistion.transform.forward * Cosine) + (HeadVistion.transform.right * Sine);
                Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
                if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
                {
                    Vertices[i + 1] = VertForward * hit.distance;          
                }
                else if(!Physics.Raycast(transform.position, RaycastDirection, out hit, VisionRange, VisionObstructingLayer))
                {

                   Vertices[i + 1] = VertForward * VisionRange; 
                    if (Physics.Raycast(transform.position, RaycastDirection, out hit, VisionRange, PlayerLayer))
                    {
                        Vertices[i + 1] = VertForward * hit.distance;
                      if (PlayerDetectSpawn)
                        {                        
                            SwitchState(SpawnState);                           
                        }
                        if (DelayHitPlayer <= 0 && !PlayerDetectSpawn) 
                        {
                            playerOutOfSight = curplayerOutSight;
                            PlayerInSight = true;
                            if (!CanseePlayer)
                            {              
                                SwitchState(AlertState);   
                                CanseePlayer = true;
                            }

                        }
                    }

                    else if (!Physics.Raycast(transform.position, RaycastDirection, out hit, VisionRange, PlayerLayer))
                    {
                        Vertices[i + 1] = VertForward * VisionRange;
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
        /*   VisionConeMesh.Clear();
            VisionConeMesh.vertices = Vertices;
            VisionConeMesh.triangles = triangles;
            MeshFilter_.mesh = VisionConeMesh;*/
        }

        if (playerOutOfSight < 0)
        {
            if (PlayerInSight)
            {
                AlertSPlay = false;
                RandomInIdle = true;
                CanseePlayer = false;
                OnPlayerAudio.Stop();
                GhostOutSightLightOn.Invoke();
                // Debug.Log("IdleAfterPlayer");
                SwitchState(IdleState);
                PlayerInSight = false;
            }

        }
    }
    #endregion




    public void DeleteGhost()
    {
        Destroy(gameObject);
    }

    public void Spawn1ghost()
    {
        HpCross = false;
        GetAttack = false;
        PlayerHitDelay = 2;
        GhostBoxCol.enabled = true;
       AnimAlert = true;
       AnimWalk = true;
        PlayerDetectSpawn = false;
        CurrentDest = destination[0];
        SwitchState(WalkState);
    }

}
