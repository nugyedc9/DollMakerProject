using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum MiniGameAuditionState { Start, ClearSkillCheck, FailSkillCheck, FinishSkillCheck, LeaveDesk };

public class MiniGameAuidition : MonoBehaviour
{
    private MiniGameAuditionState _Currentstate;
    private MiniGameAuidition Instance;
    public GameObject MiniGameAuditionActive;

    [Header("Score")]
    public float Total;
    public float TotalFinish;
    public int SlotAuditionPass, Randomspawn;
    [SerializeField] private TMP_Text BoxNum;

    [Header("Prefabs Audition")]
    public GameObject[] AuditionPrefabs;
    public Vector2[] audititionPosition;
    public GameObject[] AuditionPosition;
    public GameObject AuditionShow;

    Queue<float> AuditionPass = new Queue<float>();

    private bool DelaySpawn = true, oneRandom = true, printPeek, Fail;
    public int CurrectPass;
    [SerializeField] GameObject[] AuditionOnSceen;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _Currentstate = MiniGameAuditionState.Start;
    }

    // Update is called once per frame
    void Update()
    {


        if (_Currentstate == MiniGameAuditionState.Start)
        {
            if (SlotAuditionPass <= 4)
            {
                if (DelaySpawn)
                {
                    DelaySpawn = false; 
                    Randomspawn = Random.Range(0, AuditionPrefabs.Length);                
                    StartCoroutine(SpawnRandomAudition());                  
                }

            }
            else _Currentstate = MiniGameAuditionState.ClearSkillCheck;
        }

        if(_Currentstate == MiniGameAuditionState.ClearSkillCheck)
        {
            if (CurrectPass == 5)
            {
                _Currentstate = MiniGameAuditionState.FinishSkillCheck;
            }
            #region WASD
            if (!printPeek)
            {
                print(AuditionPass.Peek());
                printPeek = true;
            }
            if (AuditionPass.Peek() == 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    AuditionPass.Dequeue();
                    CurrectPass++;
                    printPeek = false;
                }
                
            }else
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                    }
                }
             if (AuditionPass.Peek() == 1)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    AuditionPass.Dequeue();
                    CurrectPass++;
                    printPeek = false;
                }
                
            }else
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                    }
                }
             if (AuditionPass.Peek() == 2)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    AuditionPass.Dequeue();
                    CurrectPass++;
                    printPeek = false;
                }
               
            } else
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                    }
                }
             if (AuditionPass.Peek() == 3)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    AuditionPass.Dequeue();
                    CurrectPass++;
                    printPeek = false;
                }
                
            }else
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                    }
                }
            #endregion

            
        }

        if (_Currentstate == MiniGameAuditionState.FailSkillCheck)
        {
            if (!Fail)
            {
                StartCoroutine(FailDelay());
                Fail = true;
            }
            print("Fail");
        }
        if(_Currentstate == MiniGameAuditionState.FinishSkillCheck)
        {
            AuditionPass.Clear();
            AuditionOnSceen = GameObject.FindGameObjectsWithTag("AuditionPrefabs");
            foreach (GameObject SpawnOnSceen in AuditionOnSceen)
            {
                Destroy(SpawnOnSceen);
            }
            SlotAuditionPass = 0;
            CurrectPass = 0;
            _Currentstate = MiniGameAuditionState.Start;
            print("finish");
        }
    }


    IEnumerator SpawnRandomAudition()
    {
        yield return new WaitForSeconds(0.1f);      
        GameObject SpawnAuditionPrefabs = Instantiate(AuditionPrefabs[Randomspawn], new Vector2(0, 0), Quaternion.identity);
        SpawnAuditionPrefabs.transform.SetParent(AuditionPosition[SlotAuditionPass].transform, false);
        SlotAuditionPass++;
        DelaySpawn = true;
    }

    IEnumerator FailDelay()
    {
        yield return new WaitForSeconds(2f);
        AuditionPass.Clear();
        AuditionOnSceen = GameObject.FindGameObjectsWithTag("AuditionPrefabs");
        foreach (GameObject SpawnOnSceen in AuditionOnSceen)
        {
            Destroy(SpawnOnSceen);
        }
        SlotAuditionPass = 0;
        CurrectPass = 0;
        Fail = false;
        _Currentstate = MiniGameAuditionState.Start;
    }

    public void AddBoxNumber(float ThisBox)
    {        
            AuditionPass.Enqueue(ThisBox); 
        //foreach(float Num in AuditionPass) BoxNum.text += Num + ", ";
    }

    private void OnDisable()
    {
        foreach(GameObject SpawnOnSceen in AuditionOnSceen)
        {
            Destroy(SpawnOnSceen);
        }
    }
}
