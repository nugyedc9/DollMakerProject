using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MiniGameState { Start, ClearSkillCheck, FailSkillCheck, FinishSkillCheck};

public class MiniGame : MonoBehaviour
{
    private MiniGameState _CurrentState;
    private static MiniGame Instance;
    [Header("Animation skill check")]
    [SerializeField] public Animator BallAnimate;
    public BallInBox CheckBallInBox;

    [Header("Score")]
    public float Total;
    public float TotalFinish;
    public float PassboxExample, PassBox; // For check to make random.
    Queue<float> SkillCheckPass = new Queue<float>();
    public GameObject HoldSpaceText;
    [SerializeField] private TMP_Text BoxNum; // For check number of box
    public TextMeshProUGUI ProcessDollText;

    private bool Example = true, PlayAnimate = true, CurrectBox, FinishCheck;
    public float ClickBoxCount, ProcessDoll, FinishProcessdoll, Dollhave;

    private void Start()
    {
        Instance = this; 
       // _CurrentState = MiniGameState.Start;
    }

    private void Update()
    {
        if(_CurrentState == MiniGameState.Start)
        {
            if (PlayAnimate)
            {
                BallAnimate.Play("StartMove",0 ,0);
                PlayAnimate = false;
            }
            if(PassboxExample >= 9)
            {
                _CurrentState = MiniGameState.ClearSkillCheck;
                PassboxExample = 0;
                ClickBoxCount = 0;
                Example = false;
            }
            if(ProcessDoll == 0 && !FinishCheck)
            {
                FinishProcessdoll = Random.Range(3, 5);
                FinishCheck = true;
            }
            ProcessDollText.text = ProcessDoll + " / " + FinishProcessdoll;
        }
        
        if(_CurrentState == MiniGameState.ClearSkillCheck)
        {
            HoldSpaceText.SetActive(true);
            if (!PlayAnimate && ClickBoxCount == 0)
            {
                StartCoroutine(DelayAnimationBall());
                PlayAnimate = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))  BallAnimate.GetComponent <Animator>().enabled = true;          
            else if (Input.GetKeyUp(KeyCode.Space)) BallAnimate.GetComponent<Animator>().enabled = false;
            if (PassBox >= 9)
            {
                PassBox = 0;
                _CurrentState = MiniGameState.FailSkillCheck;
            }
            if(SkillCheckPass.Peek() < PassBox )
            {
                _CurrentState = MiniGameState.FailSkillCheck;
            }
            if (CurrectBox)
            {
                SkillCheckPass.Dequeue();
                TellNumbox();
                StartCoroutine(Delayclick());
                CurrectBox = false;
            }
            if (SkillCheckPass.Count == 0)
            {
                _CurrentState = MiniGameState.FinishSkillCheck;
            }
        }

        if(_CurrentState == MiniGameState.FailSkillCheck)
        {
            HoldSpaceText.SetActive(false);
            PassBox = 0;
            Example = true; 
            PlayAnimate = true;
            SkillCheckPass.Clear();
            CheckBallInBox.ResetPassBox();
            BoxNum.text = " ";
            _CurrentState = MiniGameState.Start;
        }

        if(_CurrentState == MiniGameState.FinishSkillCheck)
        {
            if(ProcessDoll != FinishProcessdoll)
            {
                _CurrentState = MiniGameState.Start;
                HoldSpaceText.SetActive(false);
                PassBox = 0;
                Example = true;
                PlayAnimate = true;
                SkillCheckPass.Clear();
                CheckBallInBox.ResetPassBox();
                BoxNum.text = " ";
                ProcessDoll++;
                if (ProcessDoll == FinishProcessdoll)
                {
                    Dollhave++;
                    ProcessDoll = 0;
                    FinishCheck = false;
                    HoldSpaceText.SetActive(false);
                    PassBox = 0;
                    Example = true;
                    PlayAnimate = true;
                    SkillCheckPass.Clear();
                    CheckBallInBox.ResetPassBox();
                    BoxNum.text = " ";
                    _CurrentState = MiniGameState.Start;
                }
            }
            
        }
    }

    IEnumerator DelayAnimationBall()
    {
        yield return new WaitForSeconds(0.15f);
        BallAnimate.GetComponent<Animator>().enabled = false;
    }

    #region forCheck
    public void PassBoxcheck()
    {
        if (Example)
        {
            PassboxExample++;
            CheckBallInBox.StartExample();
        }
        else
        {
            PassBox++;
            CheckBallInBox.EndExample();
            CheckBallInBox.ResetColorBox();
        }
    }

    public void CurrectBoxCheck()
    {
        if (ClickBoxCount == 0)
        {
            if (PassBox == SkillCheckPass.Peek())
            {
                CurrectBox = true;
                ClickBoxCount++;
            }
            else if(PassBox !=  SkillCheckPass.Peek())  
            {
                _CurrentState = MiniGameState.FailSkillCheck;
                ClickBoxCount++;
            }
        }
    }

    public void AddBoxNumber(float ThisBox)
    {
        SkillCheckPass.Enqueue(ThisBox);
        TellNumbox();
    }

    void TellNumbox()
    {
        BoxNum.text = string.Empty;
        foreach (float NumberBox in SkillCheckPass)
        {
            BoxNum.text += NumberBox + ", ";
        }
    }

    IEnumerator Delayclick()
    {
        yield return new WaitForSeconds(0.5f);
        ClickBoxCount = 0;
    }

    #endregion

}
