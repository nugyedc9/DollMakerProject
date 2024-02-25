using UnityEngine;

public class GhostDiedState : GhostBaseState
{
    float Timer = 1.5f;

    public override void EnterState(GhostStateManager state)
    {

        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.clip = state.DiedS;
        state.GhostAudioSoure.Play();
        state.GhostAmbi.Stop();

    }

    public override void UpdateState(GhostStateManager state)
    {

        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer < 0)
        {
            state.GhostLight.SetActive(false);
            state.GhostFrom.SetActive(false);
            state.GhostBoxCol.enabled = false;
            state.GhostAmbi.Stop();
        }
    }
}
