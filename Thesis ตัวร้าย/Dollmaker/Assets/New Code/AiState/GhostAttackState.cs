using UnityEngine;

public class GhostAttackState : GhostBaseState
{
    float TimeHit, TreeSce = 3;
    bool hitplayer;
    public override void EnterState(GhostStateManager state)
    {

    }

    public override void UpdateState(GhostStateManager state)
    {
        if (state.AnimAttack)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("G_atk"))
                state.GhostAni.Play("G_atk", 0, 0);
            state.enemyGhost.speed = 0;
            TimeHit = TreeSce;
            state.AnimAttack = false;
        }

        if (hitplayer)
        {
            TimeHit = TreeSce;
            state.SwitchState(state.IdleState);
            hitplayer = false;
        }

        if (state.enemyGhost.remainingDistance < 1.5)
        {
            if (state.HitPlayer)
            {
                state.HpPlayer.Takedamage(1);
                state.HitPlayer = false;
            }
            state.AnimAlert = true;
            TimeHit -= Time.deltaTime;
            if(TimeHit < 0)
            {
                state.DelayHitPlayer = 2;
                hitplayer = true;
            }
        }
        else
        {
            if(TimeHit < 0)
            {
                state.SwitchState(state.IdleState);
            }
        }

    }
}
