using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum MiniGameAuditionState { Start, ClearSkillCheck, FailSkillCheck, TimeOut, FinishSkillCheck, LeaveDesk, ItemLost };

public class MiniGameAuidition : MonoBehaviour
{
    private MiniGameAuditionState _Currentstate;
    private MiniGameAuidition Instance;
    public GameObject MiniGameAuditionActive;
    public CanPlayMini1 canPlay;
    public Ghost GhostcomeTocheck;

    [Header("Bar")]
    public MiniG2Bar Bar;
    public float curBar, maxBar, minBar;

    [Header("Score")]
    public float Total;
    public float TotalFinish;
    public int SlotAuditionPass, Randomspawn;
    [SerializeField] private TMP_Text BoxNum;

    [Header("Timer")]
    public float Timer;

    [Header("Prefabs Audition")]
    public GameObject[] AuditionPrefabs;
    public Vector2[] audititionPosition;
    public GameObject[] AuditionPosition;
    public GameObject[] PassAuditionPosition;
    public GameObject[] FinishDoll;
    public GameObject[] FrameBox;
    public GameObject AuditionShow;
    public GameObject ButtonCutLineOBJ;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip GhostNotice;

    Queue<float> AuditionPass = new Queue<float>();

    private bool DelaySpawn = true, HoldSpace, printPeek, Fail, cutLine, NeedToCutLine ;
    public bool HaveItem, Finish;
    private int CurrectPass, FinishDollHave;
    private float CurTimer;
    [SerializeField] GameObject[] AuditionOnSceen;
    [SerializeField] GameObject[] FrameClear;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        CurTimer = Timer;
        _Currentstate = MiniGameAuditionState.LeaveDesk;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)) HoldSpace = true;
        else if(Input.GetKeyUp(KeyCode.Space)) HoldSpace= false;

        if (HoldSpace && HaveItem)
        {
            if(!NeedToCutLine)
            curBar += 3 * Time.deltaTime;
            Bar.SetMinBar(curBar);
            if(curBar <= 0) curBar = 0;

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
                else
                {
                    _Currentstate = MiniGameAuditionState.ClearSkillCheck;
                    Timer = CurTimer;
                    SlotAuditionPass = 0;
                }
            }

            if (_Currentstate == MiniGameAuditionState.ClearSkillCheck)
            {
                if(Timer <= CurTimer)
                {
                    Timer -= Time.deltaTime;
                    TellTime(Timer);
                }

                if (Timer <= 0)
                {
                    ButtonCutLineOBJ.SetActive(true);
                    _Currentstate = MiniGameAuditionState.TimeOut;
                }

                if (CurrectPass == 5)
                {
                    _Currentstate = MiniGameAuditionState.FinishSkillCheck;
                }
                #region WASD
                if (!printPeek)
                {
                   // print(AuditionPass.Peek());
                    printPeek = true;
                }
                if (AuditionPass.Peek() == 0)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        AuditionPass.Dequeue();
                        CurrectPass++;
                        CurrectFrame(0);
                        printPeek = false;
                        SpawnAuditionPassPrefabs(0);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                        CurrectFrame(1);
                        SpawnAuditionPassPrefabs(0);
                    }
                }
                if (AuditionPass.Peek() == 1)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        AuditionPass.Dequeue();
                        CurrectPass++;
                        CurrectFrame(0);
                        printPeek = false;
                        SpawnAuditionPassPrefabs(1);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                        CurrectFrame(1);
                        SpawnAuditionPassPrefabs(1);
                    }
                }
                if (AuditionPass.Peek() == 2)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        AuditionPass.Dequeue();
                        CurrectPass++;
                        CurrectFrame(0);
                        printPeek = false;
                        SpawnAuditionPassPrefabs(2);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                        CurrectFrame(1);
                        SpawnAuditionPassPrefabs(2);
                    }
                }
                if (AuditionPass.Peek() == 3)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        AuditionPass.Dequeue();
                        CurrectPass++;
                        CurrectFrame(0);
                        printPeek = false;
                        SpawnAuditionPassPrefabs(3);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        printPeek = false;
                        CurrectFrame(1);
                        SpawnAuditionPassPrefabs(3);
                    }
                }
                #endregion


            }

            if (_Currentstate == MiniGameAuditionState.FailSkillCheck)
            {
                if (!Fail)
                {
                    curBar -= 20;
                    StartCoroutine(FailDelay());
                    GhostcomeTocheck.PlayerFailSkillCheck();
                    audioSource.clip = GhostNotice;
                    audioSource.Play();
                    Fail = true;
                }
                print("Fail");
            }

            if(_Currentstate == MiniGameAuditionState.TimeOut)
            {
                ShowMouse();
                NeedToCutLine = true;
                if (cutLine)
                {                 
                    CurrectFrame(2);
                    StartCoroutine(DelayTimeOut());     
                    ButtonCutLineOBJ.SetActive(false);
                    cutLine = false;
                }
                
            }

            if (_Currentstate == MiniGameAuditionState.FinishSkillCheck)
            {
                AuditionPass.Clear();
                AuditionOnSceen = GameObject.FindGameObjectsWithTag("AuditionPrefabs");
                foreach (GameObject SpawnOnSceen in AuditionOnSceen)
                {
                    Destroy(SpawnOnSceen);

                }
                FrameClear = GameObject.FindGameObjectsWithTag("MiniGameFrame");
                foreach (GameObject FrameOnScene in FrameClear)
                {
                    Destroy(FrameOnScene);
                }
                curBar += 15;
                SlotAuditionPass = 0;
                CurrectPass = 0;
                Finish = true;
                _Currentstate = MiniGameAuditionState.Start;
                print("finish");
            }
        }

        if (_Currentstate == MiniGameAuditionState.LeaveDesk)
        {
            if (HoldSpace)
            {
                AuditionPass.Clear();
                AuditionOnSceen = GameObject.FindGameObjectsWithTag("AuditionPrefabs");
                foreach (GameObject SpawnOnSceen in AuditionOnSceen)
                {
                    Destroy(SpawnOnSceen);
                }
                FrameClear = GameObject.FindGameObjectsWithTag("MiniGameFrame");
                foreach (GameObject FrameOnScene in FrameClear)
                {
                    Destroy(FrameOnScene);
                }
                SlotAuditionPass = 0;
                CurrectPass = 0;
                _Currentstate = MiniGameAuditionState.Start;
            }
        }

        if(_Currentstate == MiniGameAuditionState.ItemLost)
        {
            if (Finish)
            {
                
               // print("LostItem");
                Finish = false;
                canPlay.FinishDoll();
                GetFinishDoll();            
            }
            _Currentstate = MiniGameAuditionState.LeaveDesk;
        }

        if (curBar >= maxBar)
        {   
            _Currentstate = MiniGameAuditionState.ItemLost;     curBar = 0;   
        }
        
    }

    public void SpawnAuditionPassPrefabs(int PrefabsSpawn)
    {
        GameObject SpawnPass = Instantiate(AuditionPrefabs[PrefabsSpawn], new Vector2(0, 0), Quaternion.identity);
        SpawnPass.transform.SetParent(PassAuditionPosition[SlotAuditionPass].transform, false);
        SlotAuditionPass++;
    }

    public void CurrectFrame(int FramePrefabs)
    {
        GameObject FrameCheck = Instantiate(FrameBox[FramePrefabs], new Vector2(0, 0),Quaternion.identity);
        FrameCheck.transform.SetParent(PassAuditionPosition[SlotAuditionPass].transform , false);
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
        FrameClear = GameObject.FindGameObjectsWithTag("MiniGameFrame");
        foreach (GameObject SpawnOnSceen in AuditionOnSceen)
        {
            Destroy(SpawnOnSceen);
        }
        foreach (GameObject FrameOnScene in FrameClear)
        {
            Destroy(FrameOnScene);
        }
        SlotAuditionPass = 0;
        CurrectPass = 0;
        Fail = false;
        _Currentstate = MiniGameAuditionState.Start;
    }

    IEnumerator DelayTimeOut()
    {
        yield return new WaitForSeconds(2);
        _Currentstate = MiniGameAuditionState.FinishSkillCheck;
        NeedToCutLine = false;
        CloseMouse();
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

    public void TellTime(float TimeToDisplay)
    {
        TimeToDisplay += 1;
        float seconds = Mathf.FloorToInt(TimeToDisplay % 60);
        BoxNum.text = string.Format("{0}", seconds);
    }

    public void LeaveMinigame()
    {
        _Currentstate = MiniGameAuditionState.LeaveDesk;
    }
    public void GetFinishDoll()
    {
        FinishDoll[FinishDollHave].SetActive(true);
            FinishDollHave++;
    }
    public void ItemHaveCheck()
    {
        HaveItem = true;
    }
    public void DontHaveItem()
    {
        HaveItem = false;
    }

    public void ButtonCutLine()
    {
        cutLine = true;
    }

    public void ShowMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
