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
    [SerializeField] private TMP_Text BoxNum; // For check number of box

    private bool Example = true, PlayAnimate = true, CurrectBox;
    private float ClickBoxCount;

    private void Start()
    {
        Instance = this;
        _CurrentState = MiniGameState.Start;
    }

    private void Update()
    {
        if(_CurrentState == MiniGameState.Start)
        {
            if (PlayAnimate)
            {
                BallAnimate.Play("StartMove", 0, 0);
                PlayAnimate = false;
            }
            if(PassboxExample >= 9)
            {
                _CurrentState = MiniGameState.ClearSkillCheck;
                PassboxExample = 0;
                ClickBoxCount = 0;
                Example = false;
            }
        }
        
        if(_CurrentState == MiniGameState.ClearSkillCheck)
        {
            if(PassBox >= 9)
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
        }

        if(_CurrentState == MiniGameState.FailSkillCheck)
        {
            PassBox = 0;
            Example = true; 
            PlayAnimate = true;
            SkillCheckPass.Clear();
            CheckBallInBox.ResetPassBox();
            _CurrentState = MiniGameState.Start;
        }

        if(_CurrentState == MiniGameState.FinishSkillCheck)
        {

        }
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
