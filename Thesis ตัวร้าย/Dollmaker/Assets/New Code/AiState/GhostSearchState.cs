using UnityEngine;

public class GhostSearchState : GhostBaseState
{
    int ranmaxspawn;
    float TimeCount;

    public override void EnterState(GhostStateManager state)
    {
        state.SpawnTimer = 4;
        state.playerOutOfSight = 1;
        TimeCount = 3;
        ranmaxspawn = state.SpawnPoint.Length - 1;
        state.CurSpawn = Random.Range(0, ranmaxspawn);
        if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("BackFrom"))
            state.GhostAni.Play("BackFrom", 0, 0);
    }

    public override void UpdateState(GhostStateManager state)
    {
        if(TimeCount > 0)
        {
            TimeCount -= Time.deltaTime;
        }
        if(TimeCount <= 0)
        {
            state.enemyGhost.Warp(state.SpawnPoint[state.CurSpawn].position);
            state.PlayerDetectSpawn = true;
            state.DrawVisionCone();
            // state.SwitchState(state.SpawnState);
        }
    }
}
