using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GhsotIdleState : GhostBaseState
{

    float IdleTime, Delaysee = 0.5f;
    int Dest;
    bool DelayFaterSpawn;

    public override void EnterState(GhostStateManager state)
    {
        state.GhostBoxCol.enabled = true;
        state.GhostFrom.SetActive(true);
        state.AnimAlert = true;
        state.AnimWalk = true;
        state.GhostAmbi.clip = state.GhostIdleAmbiS;
        state.GhostAudioSoure.Stop();
        state.GhostAmbi.Play();
        state.CanseePlayer = false;
        state.Cansee = true;
     //   Debug.Log("Idle");
    }

    public override void UpdateState(GhostStateManager state)
    {
        if (Delaysee <= 0 && !DelayFaterSpawn)
        {
            state.DrawVisionCone();
            DelayFaterSpawn = true;
        }
        
        if(DelayFaterSpawn)
            state.DrawVisionCone();

        if(state.RandomInIdle)
        {
            IdleTime = Random.Range(state.RandomMinIdle, state.RandomMaxIdle);
            Dest = Random.Range(state.DestinationMin, state.DestinationMax);
            state.CurrentDest = state.destination[Dest];
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("demo_idleanima"))
                state.GhostAni.Play("demo_idleanima", 0, 0);
            state.enemyGhost.speed = 0;
            state.RandomInIdle = false;
        }

        if (IdleTime > 0)
        {
            IdleTime -= Time.deltaTime;
        }

        if (Delaysee > 0)
        {
            Delaysee -= Time.deltaTime;
        }

        if (IdleTime <= 0)
        {
            state.SwitchState(state.WalkState);
        }
    }
}
