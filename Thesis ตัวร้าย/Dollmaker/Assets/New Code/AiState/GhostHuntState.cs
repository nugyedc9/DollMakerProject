using UnityEngine;

public class GhostHuntState : GhostBaseState
{
    public override void EnterState(GhostStateManager state)
    {
        state.GhostAudioSoure.loop = true;
        state.GhostAudioSoure.clip = state.HuntS;
        state.GhostAudioSoure.Play();
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();

        state.Dest = state.playerPos.position;
        state.enemyGhost.destination = state.Dest;
        state.enemyGhost.speed = state.HuntSpeed;
        if (state.AnimHunt)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Walk_ani"))
                state.GhostAni.Play("Walk_ani", 0, 0);
            state.AnimHunt = false;
        }
        if(state.enemyGhost.remainingDistance <= 2)
        {
            state.AnimAttack = true;
            state.HitPlayer = true;
            state.SwitchState(state.AttckState);
        }
    }
}
