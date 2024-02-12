using UnityEngine;

public abstract class GhostBaseState 
{
    public abstract void EnterState(GhostStateManager stateGhost);
    public abstract void UpdateState(GhostStateManager stateGhost);
}
