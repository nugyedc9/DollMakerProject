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
        state.DelayHitPlayer = 0;
        state.particle.Stop();
        state.FireSound.Stop();
        state.HpCross = false;
        state.GetAttack = false;
        state.PlayerHitDelay = 2;
        state.HpBeforeHit = state.HpGhost;

        state.HitPlayer = false;
        state.CanseePlayer = false;
        state.Cansee = true;

        IdleTime = Random.Range(state.RandomMinIdle, state.RandomMaxIdle);
        Dest = Random.Range(state.DestinationMin, state.DestinationMax);

      //  Debug.Log("Idle");
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

        if (state.RandomInIdle)
        {

            if (state.CurSpawn == 1 || state.CurSpawn == 4)
                state.CurrentDest = state.destination[Dest];
            else if (state.CurSpawn == 0 || state.CurSpawn == 3)
                state.CurrentDest = state.destination2[Dest];
            else if (state.CurSpawn == 2 || state.CurSpawn == 5)
                state.CurrentDest = state.destination3[Dest];

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
