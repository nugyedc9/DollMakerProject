using UnityEngine;

public class GhostHuntState : GhostBaseState
{
    public override void EnterState(GhostStateManager state)
    {

    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();

        state.Dest = state.playerPos.position;
        state.enemyGhost.destination = state.Dest;
        state.enemyGhost.speed = state.HuntSpeed;
        if (state.AnimHunt)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("walk_ani"))
                state.GhostAni.Play("walk_ani", 0, 0);
            state.AnimHunt = false;
        }
        if(state.enemyGhost.remainingDistance <= 1)
        {
            state.AnimAttack = true;
            state.HitPlayer = true;
            state.SwitchState(state.AttckState);
        }
    }
}
