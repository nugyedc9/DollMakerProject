using UnityEngine;

public class GhostWalkState : GhostBaseState
{
    public override void EnterState(GhostStateManager state)
    {
        Debug.Log("Walk");
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();

        state.Dest = state.CurrentDest.position;
        state.enemyGhost.destination = state.Dest;
        state.enemyGhost.speed = state.WalkSpeed;
        if (state.AnimWalk)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("walk_ani"))
                state.GhostAni.Play("walk_ani", 0, 0);
            state.AnimWalk = false;
        }
        if(state.enemyGhost.remainingDistance <= state.enemyGhost.stoppingDistance )
        {
            state.RandomInIdle = true;
            //Debug.Log("IdleAfterWalk");
            state.SwitchState(state.IdleState);
        }
    }
}
