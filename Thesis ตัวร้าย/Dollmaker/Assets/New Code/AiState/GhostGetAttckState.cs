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
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Damage_ani"))
                state.GhostAni.Play("Damage_ani", 0, 0);
            CurDelay = 2;
            state.ChangePos = false;
        }

        CurDelay -= Time.deltaTime;

        if(CurDelay < 0)
        {
            state.SwitchState(state.DiedState);
        }
    }
}
