using UnityEngine;

public class GhostDiedState : GhostBaseState
{
    public override void EnterState(GhostStateManager state)
    {
        state.GhostLight.SetActive(false);
        state.GhostFrom.SetActive(false);
        state.GhostBoxCol.enabled = false;
   
    }

    public override void UpdateState(GhostStateManager state)
    {

    }
}
