using JetBrains.Annotations;
using UnityEngine;

public class GhsotDetectPlayerSpawn : GhostBaseState
{

    public override void EnterState(GhostStateManager state)
    {
        state.AnimSpawn = true;
        state.PlayerDetectSpawn = true;
        state.GhostBoxCol.enabled = false;
        state.Cansee = true;
        state.HpCross = false;
        state.PlayerHitDelay = state.HuntSpeed;
        state.GhostLight.SetActive(true);
        state.GhostFrom.SetActive(false);
      /*  state.GhostAudioSoure.clip = state.DetectS;
        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.Play();*/
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();
    }
}
