using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;


public enum MiniGameAuditionState { Start, ClearSkillCheck, FailSkillCheck, ClearTimeOut, FinishSkillCheck, LeaveDesk, ItemLost };

public class MiniGameAuidition : MonoBehaviour
{
    private MiniGameAuditionState _Currentstate;
    private MiniGameAuidition Instance;
    public GameObject MiniGameAuditionActive;
    public CanPlayMini1 canPlay;
    public ClothColorDrop clothColorDrop;
    public GhostStateManager GhostcomeTocheck;

    [Header("Bar")]
    public MiniG2Bar Bar;
    public float curBar, maxBar, minBar;

    [Header("Score")]
    public float Total;
    public float TotalFinish;
    public int SlotAuditionPass, Randomspawn;
    [SerializeField] private TMP_Text BoxNum;

    [Header("Inventory")]
    public InventoryManager inventoryManager;
    public ClothColorDrop GetfinishDoll;
    public PlayerPickUpItem playpickUp; 
    public Item[] FinishClothID;

    [Header("Prefabs Audition")]
    public GameObject[] AuditionPrefabs;
    public Vector2[] audititionPosition;
    public GameObject[] AuditionPosition;
    public GameObject[] PassAuditionPosition;

    public GameObject[] FrameBox;
    public GameObject AuditionShow;
    public GameObject ButtonCutLineOBJ;

    [Header("---- Audio ----")]
    public AudioSource audioSource;
    public AudioSource machineActive;
    public AudioClip GhostNotice, RightClick, CutLineS;

    [Header("Animator")]
    public Animator Needle;
    public Animator ClothMove, handMove;

    [SerializeField] bool haveitem;
    public bool HaveItem { get { return haveitem; } set { haveitem = value; } }
    [SerializeField] bool CutLine;
    public bool cutLine { get { return CutLine; } set {  CutLine = value; } }

    Queue<float> AuditionPass = new Queue<float>();

    private bool DelaySpawn = true, HoldSpace, printPeek, Fail, NeedToCutLine , NeedleWorking, GetHurt;
    public bool Finish;
    private int CurrectPass, FinishDollHave;
    private float  ClothPlayanim;
    [SerializeField] GameObject[] AuditionOnSceen;
    [SerializeField] GameObject[] FrameClear;
    [SerializeField] GameObject CutHere;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _Currentstate = MiniGameAuditionState.LeaveDesk;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HoldSpace = true;
            if (NeedleWorking)
            {
                Needle.Play("NeedleAnim");
                //ClothMove.Play("ClothAnim");
            }
            if (!GetHurt)
            {
                handMove.Play("Handsewing");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            HoldSpace = false; 
            Needle.enabled = false;
            ClothMove.enabled = false;
            handMove.enabled = false;
        }

        if (HoldSpace && HaveItem)
        {

            Bar.SetMinBar(curBar);
            if(curBar <= 0) curBar = 0;
            if (!NeedleWorking)
            {
                Needle.enabled = false;
                ClothMove.enabled = false;
            }

            if (_Currentstate == MiniGameAuditionState.Start)
            {
                NeedleWorking = true;
                Needle.enabled = true;
                ClothMove.enabled = true;
                handMove.enabled = true;
                machineActive.enabled = true;
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
                    SlotAuditionPass = 0;
                    Fail = false;
                }
            }

            if (_Currentstate == MiniGameAuditionState.ClearSkillCheck)
            {
                Needle.enabled = true;
                ClothMove.enabled = true;
                handMove.enabled= true;
                handMove.Play("Handsewing");

                if (CurrectPass == 5)
                {
                    _Currentstate = MiniGameAuditionState.FinishSkillCheck;
                }

                #region WASD
                if (!printPeek)
                {
                    // print(AuditionPass.Peek());                  
                    ClothPlayanim  -= Time.deltaTime;
                    if(ClothPlayanim <= 0)
                    {
                        printPeek = true;
                    }
                    
                }
                else
                {
                    ClothMove.enabled = false;
                    handMove.enabled = false ;
                }
                if (AuditionPass.Peek() == 0)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        AuditionPass.Dequeue();
                        CurrectPass++;
                        curBar += 5;
                        CurrectFrame(0);
                        printPeek = false;
                        ClothMove.enabled = true;
                        handMove.enabled = true;
                        audioSource.clip = RightClick;
                        audioSource.Play();
                        SpawnAuditionPassPrefabs(0);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        CutHere.SetActive(true);
                        printPeek = false;cutLine = false;
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
                        curBar += 5;
                        printPeek = false;
                        ClothMove.enabled = true;
                        handMove.enabled = true;
                        audioSource.clip = RightClick;
                        audioSource.Play();
                        SpawnAuditionPassPrefabs(1);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        CutHere.SetActive(true);
                        printPeek = false;
                        cutLine = false;
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
                        curBar += 5;
                        printPeek = false;
                        ClothMove.enabled = true;
                        handMove.enabled = true;
                        audioSource.clip = RightClick;
                        audioSource.Play();
                        SpawnAuditionPassPrefabs(2);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        CutHere.SetActive(true);
                        printPeek = false;
                        cutLine = false;
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
                        curBar += 5;
                        printPeek = false;
                        ClothMove.enabled = true;
                        handMove.enabled = true;
                        audioSource.clip = RightClick;
                        audioSource.Play();
                        SpawnAuditionPassPrefabs(3);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        _Currentstate = MiniGameAuditionState.FailSkillCheck;
                        CutHere.SetActive(true);
                        printPeek = false;
                        cutLine = false;
                        CurrectFrame(1);
                        SpawnAuditionPassPrefabs(3);
                    }
                }
                #endregion


            }

            if (_Currentstate == MiniGameAuditionState.FailSkillCheck)
            {
                GetHurt = true;
                handMove.enabled = true;
                handMove.Play("Handhurtsewing");
                NeedToCutLine = true;
                if (!Fail)
                {
                    curBar -= 20;
                  //  GhostcomeTocheck.PlayerFailSkillCheck();
                    audioSource.clip = GhostNotice;
                    audioSource.Play();
                    NeedToCutLine = false;
                    NeedleWorking = false;
                    Needle.enabled = false;
                    ClothMove.enabled = false;
                    Fail = true;
                }
                if (cutLine)
                {
                    audioSource.clip = CutLineS;
                    audioSource.Play();
                    CutHere.SetActive(false);
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
                    GetHurt = false;
                    _Currentstate = MiniGameAuditionState.Start;
                }
                //print("Fail");
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
                CutHere.SetActive(false);
                curBar += 10;
                SlotAuditionPass = 0;
                CurrectPass = 0;
                Finish = true;
                _Currentstate = MiniGameAuditionState.Start;
              //  print("finish");
            }
        }
        if (_Currentstate == MiniGameAuditionState.LeaveDesk)
        {         
            machineActive.enabled = false;
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
            if (playpickUp.ItemCount < inventoryManager.inventoryslote.Length)
            {
                if (Finish)
                {
                    Needle.enabled = false;
                    ClothMove.enabled = false;
                    // print("LostItem");            
                    //canPlay.FinishDoll();
                    canPlay.Cloth = false;
                    inventoryManager.AddItem(FinishClothID[GetfinishDoll.FinishClothID]);
                    Finish = false;
                }
                _Currentstate = MiniGameAuditionState.LeaveDesk;
            }
            
        }

        if (curBar >= maxBar)
        {   
            _Currentstate = MiniGameAuditionState.ItemLost;      curBar = 0;   
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

   /* IEnumerator FailDelay()
    {
        yield return new WaitForSeconds(2f);       
        AuditionPass.Clear();
        AuditionOnSceen = GameObject.FindGameObjectsWithTag("AuditionPrefabs");
        foreach (GameObject SpawnOnSceen in AuditionOnSceen)
        {
            Destroy(SpawnOnSceen);
        }
        CutHere.SetActive(false);
        SlotAuditionPass = 0;
        CurrectPass = 0;
        Fail = false;
        _Currentstate = MiniGameAuditionState.Start;
    }*/


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
        Needle.enabled = false;
        ClothMove.enabled = false;
        handMove.enabled = false;
        if (!NeedToCutLine)
        {
            HoldSpace = false;
            _Currentstate = MiniGameAuditionState.LeaveDesk;
        }
        else { CloseMouse(); }
    
    }
/*    public void GetFinishDoll()
    {
        FinishDoll[FinishDollHave].SetActive(true);
            FinishDollHave++;
    }*/
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
