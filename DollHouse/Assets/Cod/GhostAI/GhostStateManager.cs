using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateManager : MonoBehaviour
{
    public StateGhost currentState;


    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        StateGhost nextState = currentState?.RunCurrentState();

        if( nextState != null )
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(StateGhost nextState)
    {
        currentState = nextState;
    }
}
