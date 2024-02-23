using UnityEngine;

public class GhostAttackState : GhostBaseState
{
    float TimeHit, TreeSce = 2;
    bool hitplayer;
    public override void EnterState(GhostStateManager state)
    {
        TimeHit = TreeSce;
        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.clip = state.AttackS;
        state.GhostAudioSoure.Play();
    }

    public override void UpdateState(GhostStateManager state)
    {
        TimeHit -= Time.deltaTime;
        if (state.AnimAttack)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Attack_ani"))
                state.GhostAni.Play("Attack_ani", 0, 0);
            state.DelayHitPlayer = 2;
            state.enemyGhost.speed = 0f;
            state.AnimAttack = false;
        }
        state.DrawVisionCone();
        if (hitplayer)
        {
            TimeHit = TreeSce;
            state.SwitchState(state.IdleState);
            hitplayer = false;
        }

        if (state.enemyGhost.remainingDistance < 3)
        { 
            if (TimeHit < 1.5)
            {
                if (state.HitPlayer)
                {
                    state.HpPlayer.Takedamage(1);
                    state.HitPlayer = false;
                }
            }

            state.AnimAlert = true;
           
            if (TimeHit < 0)
            {
                hitplayer = true;            
            }
        }
        else
        {
            if (TimeHit < 0)
            {
                state.SwitchState(state.AlertState);
            }
        }

    }
}
