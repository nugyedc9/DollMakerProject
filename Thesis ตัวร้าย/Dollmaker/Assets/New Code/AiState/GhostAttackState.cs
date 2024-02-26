using UnityEngine;

public class GhostAttackState : GhostBaseState
{
    float TimeHit = 2, Sec = 1.5f;
    bool hitplayer, PlayerInRange;
    public override void EnterState(GhostStateManager state)
    {
        Debug.Log("Attack");
        TimeHit = Sec;
        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.clip = state.AttackS;
        state.GhostAudioSoure.Play();
        state.GhostBoxCol.enabled = false;
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
            TimeHit = Sec;
            state.CanseePlayer = false;
            PlayerInRange = false;
            state.SwitchState(state.IdleState);
            hitplayer = false;
        }

        if (!PlayerInRange)
        {
            if (TimeHit < 0.5f)
            {
                if (state.enemyGhost.remainingDistance < 2.5f)
                {

                    if (state.HitPlayer)
                    {
                        state.CanseePlayer = false;
                        state.HpPlayer.Takedamage(1);
                        state.HitPlayer = false;
                        PlayerInRange = true;
                    }

                    state.AnimAlert = true;

                }
                else
                {
                    hitplayer = true;
                    PlayerInRange  = true;
                }
            }
        }

        if (TimeHit < 0)
        {
            hitplayer = true;
            state.CanseePlayer = false;
        }

    }
}
