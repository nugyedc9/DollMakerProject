using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;

public enum DollCreatingState { Start, TrySkillCheckButton, ClearSkillCheck, FinishMiniG2 }; 

public class MiniG2 : MonoBehaviour
{
    public float maxRan, minRan, curRan, maxBar, curBar, minBar;
    public float WorkThis;


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
    public GameObject barSlider, Stick, canvaMiniG2;
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
    [SerializeField] GameObject SpawnAction;
    [SerializeField] CanPlayMini1 Canplay;

    public UnityEvent Cutscene;
    public UnityEvent PickItem2;

    private DollCreatingState CurrentDollCreatingState;
    public static MiniG2 Instance;
    [SerializeField] public Player PCut;

    // Start is called before the first frame update
    void Start()
    {
        CurrentDollCreatingState = DollCreatingState.Start;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
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
          
            if(CountAction == 0)
            {   
                StartCoroutine(spawnRan()); 
            }
            if(CountAction >= ActionInGame)
            {
                StopAllCoroutines();
                CurrentDollCreatingState = DollCreatingState.ClearSkillCheck;
            }
        }
        else if(CurrentDollCreatingState == DollCreatingState.ClearSkillCheck)
        {
            if(Point >= MaxPoint)
            {
                CurrentDollCreatingState = DollCreatingState.Start;
                Point = 0;
                CountAction = 0;
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
            if(TotelDoll != DollHave)
                DollHave = TotelDoll;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvaMiniG2.SetActive(false);
        }


        if (curBar >= maxBar)
        {
            CurrentDollCreatingState = DollCreatingState.FinishMiniG2;
        }

        TotelD.text = "Total Doll :  " + TotelDoll;

        if(TotelDoll == 2)
        {
            Cutscene.Invoke();
        }
        if(TotelDoll == 1)
        {
            PickItem2.Invoke();
        }


    }

    IEnumerator RandomShowBar()
    {
        WaitForSeconds wait = new WaitForSeconds(curRan);

        yield return wait;
           // barSlider.SetActive(true); 
        CurrentDollCreatingState = DollCreatingState.TrySkillCheckButton;
    }

    public void Workingnow()
    {
        curBar += 2.5f;
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

    public void CheckStart()
    {
        if(TotelDoll != DollHave)
        {
            if(CountAction !=0)
            CountAction = 0;
            CurrentDollCreatingState = DollCreatingState.Start;
        }
        else
        {
            CurrentDollCreatingState = DollCreatingState.Start;
        }
    }

}
