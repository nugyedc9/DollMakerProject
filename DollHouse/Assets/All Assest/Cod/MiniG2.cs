using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

public enum DollCreatingState { Start, TrySkillCheckButton, ClearSkillCheck, FinishMiniG2 };

public class MiniG2 : MonoBehaviour
{
    public float maxRan, minRan, curRan, maxBar, curBar, minBar;
    public float WorkThis;

    float SkipNextStep = 4;
    float CurSkipNextStep;

    [Header("Score")]
    public float Score;
    public float MaxScore;
    public float Miss;
    public float TotelDoll = 0;
    public float DollHave;
    public GameObject TotelDollCanva;
    public TextMeshProUGUI TotelD;

    [Header("GameObj")]
    public bool StartMiniG2;
    public bool StopMiniG2;
    public bool working;
    public GameObject canvaMiniG2;

    [Header("Finish doll")]
    public GameObject[] FinshDoll;
    private int FinishDollHave;

    [Header("Audio")]
    public AudioSource SoundSoure;
    public AudioClip Alertsound;
    public AudioClip FinishSound;
    public AudioClip FailSound;
    public AudioClip SuccessSound;


    public MiniG2Bar G2bar;

    [Header("SpawnAction")]
    public float minTranX;
    public float minTranY;
    public float maxTranX;
    public float maxTranY;
    public float Spawnsecond;
    public float CountAction;
    public float ActionInGame;
    public float Point;
    public float MaxPoint;

    [Header("Dialog Pick Item")]

    [SerializeField] GameObject[] Action;
    [SerializeField] GameObject[] DestroyBeat;
    [SerializeField] CanPlayMini1 Canplay;

    public UnityEvent Cutscene;
    public UnityEvent PickItem2;
    public UnityEvent Make4Doll;
    public UnityEvent Make6Doll;

    private DollCreatingState CurrentDollCreatingState;
    public static MiniG2 Instance;
    [SerializeField] public Player PCut;
    private bool pass4doll;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {             

        DestroyBeat = GameObject.FindGameObjectsWithTag("HeadBeat");

        if (CurrentDollCreatingState == DollCreatingState.Start)
        {
            //print("StartState");
            curBar += 3 * Time.deltaTime;
            G2bar.SetMinBar(curBar);
            DollHave += 0.01f * Time.deltaTime;
            working = true;
            if (curBar >= minBar && curBar < maxBar && working)
            {
                StartCoroutine(RandomShowBar());
            }
        }
        else if (CurrentDollCreatingState == DollCreatingState.TrySkillCheckButton)
        {
            // print("SkillState");
            working = false;

            if (CountAction == 0)
            {
                StartCoroutine(spawnRan());
            }
            if (CountAction >= ActionInGame)
            {
                StopAllCoroutines();
                CurrentDollCreatingState = DollCreatingState.ClearSkillCheck;
            }
        }
        else if (CurrentDollCreatingState == DollCreatingState.ClearSkillCheck)
        {
            curBar += 1 * Time.deltaTime;
            G2bar.SetMinBar(curBar);

            if (Point >= MaxPoint)
            {
                CurrentDollCreatingState = DollCreatingState.Start;
                Point = 0;
                CountAction = 0;
            }
                CurSkipNextStep += 1 * Time.deltaTime;
            if (CurSkipNextStep >= SkipNextStep)
            {
                CurSkipNextStep = 0;
                Point = 0;
                CountAction = 0;
                CurrentDollCreatingState = DollCreatingState.Start;
            }           
        }
        else if (CurrentDollCreatingState == DollCreatingState.FinishMiniG2)
        {
            // print("finsh");
            StartMiniG2 = false;
            StopMiniG2 = true;
            canvaMiniG2.SetActive(false);
            StopAllCoroutines();
            TotelDollCanva.SetActive(true);
            TotelDoll++;
            curBar = 0;
            Canplay.Dolllost();
            Canplay.ClothLost();
            G2bar.SetMinBar(curBar);
            if (TotelDoll != DollHave)
            {
                print("finsh");
                DollHave = TotelDoll;
                FinishDollHave++;
                ShowFinishDoll();
                CurrentDollCreatingState = DollCreatingState.Start;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvaMiniG2.SetActive(false);
        }


        if (curBar >= maxBar)
        {
            SoundSoure.clip = FinishSound;
            SoundSoure.Play();
            CurrentDollCreatingState = DollCreatingState.FinishMiniG2;
        }

        TotelD.text = "Finish Doll :  " + TotelDoll + " / 6";

        if (TotelDoll == 4)
        {
            if (!pass4doll)
            {
                TotelDoll--;
                FinshDoll[FinishDollHave].SetActive(false);
                FinishDollHave--;
                Make4Doll.Invoke();
                pass4doll = true;
            }
        }
        if (TotelDoll == 1)
        {
            PickItem2.Invoke();
        }
        if(TotelDoll == 6)
        {
            Make6Doll.Invoke();
        }

        #region Screen check
        if (Screen.width < 4000 && Screen.height < 2200)
        {
            minTranX = Screen.width - 3500;
            maxTranX = Screen.width - 500;
            minTranY = Screen.height - 1700;
            maxTranY = Screen.height - 500;
        }
        if (Screen.width < 2000 && Screen.height < 1100)
        {
            minTranX = Screen.width - 1700;
            maxTranX = Screen.width - 300;
            minTranY = Screen.height - 900;
            maxTranY = Screen.height - 500;
        }
        if (Screen.width < 1400 && Screen.height < 900)
        {
            minTranX = Screen.width - 1200;
            maxTranX = Screen.width - 200;
            minTranY = Screen.height - 700;
            maxTranY = Screen.height - 200;
        }
        #endregion

    }

    IEnumerator RandomShowBar()
    {
        SoundSoure.clip = Alertsound;
        SoundSoure.Play();
        //WaitForSeconds wait = new WaitForSeconds(curRan);
        yield return new WaitForSeconds(curRan);
        // barSlider.SetActive(true);    
        CurrentDollCreatingState = DollCreatingState.TrySkillCheckButton;
    }
    public void Workingnow()
    {
        SoundSoure.clip = SuccessSound; SoundSoure.Play();
        curBar += 2.5f;
        G2bar.SetMinBar(curBar);
        Point++;
        curRan = Random.Range(minRan, maxRan);
    }
    public void failCheck()
    {
        SoundSoure.clip = FailSound; SoundSoure.Play(); 
        curBar -= 2.5f;
        G2bar.SetMinBar(curBar);
        Point++;
        curRan = Random.Range(minRan, maxRan);
    }

    IEnumerator spawnRan()
    {
        while (true)
        {
            var wantedX = Random.Range(minTranX, maxTranX);
            var wantedY = Random.Range(minTranY, maxTranY);
            var position = new Vector3(wantedX, wantedY);
            GameObject ActionObj = Instantiate(Action[Random.Range(0, Action.Length)], position, Quaternion.identity);
            ActionObj.transform.parent = transform;
            CountAction++;         
            yield return new WaitForSeconds(Spawnsecond);
        }
    }

    public void ShowFinishDoll()
    {
        FinshDoll[FinishDollHave].SetActive(true);
    }

 /*   public void CheckStart()
    {
        if (TotelDoll != DollHave)
        {
            if (CountAction != 0)
                CountAction = 0;
            CurrentDollCreatingState = DollCreatingState.Start;
        }
        else
        {
            CurrentDollCreatingState = DollCreatingState.Start;
        }
    }*/

    private void OnDisable()
    {
        foreach (GameObject headBeat in DestroyBeat)
        {
            Destroy(headBeat);
        }
    }

    public void GetFinishDoll()
    {
        FinishDollHave++;
        FinshDoll[FinishDollHave].SetActive(true);
    }

}