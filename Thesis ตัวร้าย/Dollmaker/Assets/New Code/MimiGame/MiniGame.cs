using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniGameState { Start, ClearSkillCheck, FailSkillCheck, FinishSkillCheck};

public class MiniGame : MonoBehaviour
{
    private MiniGameState _CurrentState;
    private static MiniGame Instance;
    [Header("Animation skill check")]
    [SerializeField] public Animator BallAnimate;

    [Header("Score")]
    public float Total;
    public float TotalFinish;
    public float PassboxExample, PassBox; // For check to make random.

    private bool Example = true;
    private bool PlayAnimate = true;

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
                Example = false;
            }
        }
        
        if(_CurrentState == MiniGameState.ClearSkillCheck)
        {
            
        }

        if(_CurrentState == MiniGameState.FailSkillCheck)
        {

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
        }
        else
        {
            PassBox++;
        }
    }
    #endregion

}
