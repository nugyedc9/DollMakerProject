using UnityEngine;

public class GhsotDetectPlayerSpawn : GhostBaseState
{

    public override void EnterState(GhostStateManager state)
    {
        state.AnimSpawn = true;
        state.PlayerDetectSpawn = true;
        state.GhostBoxCol.enabled = true;
        state.Cansee = true;
        state.HpCross = false;
        state.PlayerHitDelay = state.HuntSpeed;
        state.GhostLight.SetActive(true);
        state.GhostFrom.SetActive(false);
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();
    }
}
