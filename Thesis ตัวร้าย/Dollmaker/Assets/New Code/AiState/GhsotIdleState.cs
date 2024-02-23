using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GhsotIdleState : GhostBaseState
{

    float IdleTime;
    int Dest;

    public override void EnterState(GhostStateManager state)
    {
        state.GhostFrom.SetActive(true);
        state.AnimAlert = true;
        state.AnimWalk = true;
        state.GhostAmbi.clip = state.GhostIdleAmbiS;
        state.GhostAmbi.Play();
        state.Cansee = true;
     //   Debug.Log("Idle");
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();

        if(state.RandomInIdle)
        {
            IdleTime = Random.Range(state.RandomMinIdle, state.RandomMaxIdle);
            Dest = Random.Range(state.DestinationMin, state.DestinationMax);
            state.CurrentDest = state.destination[Dest];
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Dead_ani"))
                state.GhostAni.Play("Dead_ani", 0, 0);
            state.enemyGhost.speed = 0;
            state.RandomInIdle = false;
        }

        if (IdleTime > 0)
        {
            IdleTime -= Time.deltaTime;
        }
        
        if(IdleTime <= 0)
        {
            state.SwitchState(state.WalkState);
        }
    }
}
