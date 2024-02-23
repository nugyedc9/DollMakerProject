using System.Collections;
using UnityEngine;

public class GhostAlertState : GhostBaseState
{
    float DelayTime;

    public override void EnterState(GhostStateManager state)
    {
        // Debug.Log("Alert");
        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.clip = state.FoundS;
        state.GhostAudioSoure.Play();
        state.GhostAmbi.clip = state.GhostHuntAmbi;
        state.GhostAmbi.Play();
    }

    public override void UpdateState(GhostStateManager state)
    {
       // state.DrawVisionCone();

        if (state.AnimAlert)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Found_ani"))
                state.GhostAni.Play("Found_ani", 0, 0);
            DelayTime = 1;
            state.enemyGhost.speed = 0;
            state.AnimAlert = false;
        }
        if(DelayTime > 0)
        {
            DelayTime -= Time.deltaTime;
        }
        if(DelayTime < 0)
        {
            state.AnimHunt = true;
            state.SwitchState(state.HuntState);
        }
    }
}
