using System.Collections;
using UnityEngine;

public class GhsotSpawnState : GhostBaseState
{

    float Timer;
    bool AnimChange;
    
    public override void EnterState(GhostStateManager state)
    {
        AnimChange = true;
        Timer = state.SpawnTimer;
        state.PlayerDetectSpawn = true;
        state.GhostBoxCol.enabled = true;
        state.Cansee = false;
        state.HpCross = false;
        state.PlayerHitDelay = state.HuntSpeed;
        state.GhostLight.SetActive(true);
        state.GhostFrom.SetActive(true);
        if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Shake_ani"))
            state.GhostAni.Play("Shake_ani", 0, 0);

    }

    public override void UpdateState(GhostStateManager state)
    {
        state.SpawnTimer = state.SpawnTimer - (1 * Time.deltaTime);

        state.AnimSpawn = false;
        if(state.SpawnTimer < 0)
        {
            state.PlayerDetectSpawn = false;
            state.Cansee = true;
            state.GhostLight.SetActive(false);
            state.SpawnTimer = Timer;
            state.RandomInIdle = true;
            state.SwitchState(state.IdleState);
        }

        if(state.SpawnTimer < Timer / 2)
        {
            if (AnimChange)
            {
                state.GhostAudioSoure.clip = state.SpawnS;
                state.GhostAudioSoure.Play();
                if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Transform_ani"))
                    state.GhostAni.Play("Transform_ani", 0, 0);
                AnimChange = false;
            }
        }
    }

}
