using UnityEngine;

public class GhostWalkState : GhostBaseState
{
    public override void EnterState(GhostStateManager state)
    {

    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();

        state.Dest = state.CurrentDest.position;
        state.enemyGhost.destination = state.Dest;
        state.enemyGhost.speed = state.WalkSpeed;
        if(state.enemyGhost.remainingDistance <= state.enemyGhost.stoppingDistance )
        {
            state.RandomInIdle = true;
            Debug.Log("IdleAfterWalk");
            state.SwitchState(state.IdleState);
        }
    }
}
