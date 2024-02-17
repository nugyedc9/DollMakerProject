using System.Collections;
using UnityEngine;

public class GhsotDetectPlayerSpawn : GhostBaseState
{

    public override void EnterState(GhostStateManager state)
    {
        state.GhostLight.SetActive(false);
        state.GhostFrom.SetActive(false); state.Cansee = true;
        Debug.Log("State detect");
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();       
    }
}
