using System.Collections;
using UnityEngine;

public class GhsotSpawnState : GhostBaseState
{

    float Timer;
    
    public override void EnterState(GhostStateManager state)
    {
        Timer = state.SpawnTimer;
        state.GhostBoxCol.enabled = true;
        state.Cansee = false;
        state.HpCross = false;
        state.PlayerHitDelay = state.HuntSpeed;
        state.GhostLight.SetActive(true);
        state.GhostFrom.SetActive(false);
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.SpawnTimer = state.SpawnTimer - (1 * Time.deltaTime);
        if(state.SpawnTimer < 0)
        {
            state.Cansee = true;
            state.GhostLight.SetActive(false);
            state.SpawnTimer = Timer;
            state.RandomInIdle = true;
            state.SwitchState(state.IdleState);
        }
    }

}
