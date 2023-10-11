using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum DollCreatingState { Start, TrySkillCheckButton, FinishMiniG2 }; 

public class MiniG2 : MonoBehaviour
{
    public float maxRan, minRan, curRan, maxBar, curBar, minBar;
    public float WorkThis;


    [Header("Score")]
    public float Score;
    public float MaxScore;
    public float Miss;
    public float TotelDoll = 0;
    public TextMeshProUGUI TotelD;

    [Header("GameObj")]
    public bool StartMiniG2, StopMiniG2, working;
    public GameObject barSlider, Stick, canvaMiniG2;
    public MiniG2Bar G2bar;

    private DollCreatingState CurrentDollCreatingState;


    // Start is called before the first frame update
    void Start()
    {
        CurrentDollCreatingState = DollCreatingState.Start;
    }

    // Update is called once per frame
    void Update()
    {


        if (CurrentDollCreatingState == DollCreatingState.Start)
        {
            print("StartState");
            curBar += 1 * Time.deltaTime;
            G2bar.SetMinBar(curBar);
            working = true;
            if (curBar >= minBar && curBar < maxBar && working)
            {            
                StartCoroutine(RandomShowBar());
            }
        }
        else if (CurrentDollCreatingState == DollCreatingState.TrySkillCheckButton)
        {
            print("SkillState");
            working = false;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopAllCoroutines();
                print("GeykeySpace");
                CurrentDollCreatingState = DollCreatingState.Start;
                barSlider.SetActive(false);
                curBar += 10;
                G2bar.SetMinBar(curBar);
                curRan = Random.Range(minRan, maxRan);
            }
        }
        else if (CurrentDollCreatingState == DollCreatingState.FinishMiniG2)
        {
            print("finsh");
            StartMiniG2 = false;
            StopMiniG2 = true;
            barSlider.SetActive(false);
            StopAllCoroutines();
            TotelDoll++;
            curBar = 0;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvaMiniG2.SetActive(false);
        }


        if (curBar >= maxBar)
        {
            CurrentDollCreatingState = DollCreatingState.FinishMiniG2;
        }

      /*  if (!StopMiniG2)
        {
            if (curBar >= maxBar)
            {

                StartMiniG2 = false;
                StopMiniG2 = true;
                barSlider.SetActive(false);
                StopAllCoroutines();
                TotelDoll++;
                curBar = 0;
            }
            if (curBar >= minBar && curBar < maxBar && working) 
            {
                StartCoroutine("RandomShowBar");
            }
            if (StartMiniG2)
            {
                curBar += 1 * Time.deltaTime;
                G2bar.SetMinBar(curBar);
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            StartMiniG2 = true;
            StopMiniG2 = false;
            WorkThis++;
        }
        if(WorkThis >=1)
        {
            WorkThis -= 0.1f * Time.deltaTime;
            if(WorkThis <= 0)
            { WorkThis = 0; }   
        }*/
    }

    IEnumerator RandomShowBar()
    {
        WaitForSeconds wait = new WaitForSeconds(curRan);

        yield return wait;
            barSlider.SetActive(true); 
        CurrentDollCreatingState = DollCreatingState.TrySkillCheckButton;
        yield break;
    }

    public void Workingnow()
    {
        curBar += 10;
        curRan = Random.Range(minRan, maxRan);
        working = true;
    }

}
