using UnityEngine;

public class GhostGetAttckState : GhostBaseState
{
    float CurDelay;

    public override void EnterState(GhostStateManager state)
    {
        
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.Cansee = false;
        state.DrawVisionCone();
        if (state.ChangePos)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("G_dead"))
                state.GhostAni.Play("G_dead", 0, 0);
            CurDelay = state.ChangePosDelay;
            state.ChangePos = false;
        }

        CurDelay -= Time.deltaTime;

        if(CurDelay < 0)
        {
            state.SwitchState(state.SpawnState);
        }
    }
}
