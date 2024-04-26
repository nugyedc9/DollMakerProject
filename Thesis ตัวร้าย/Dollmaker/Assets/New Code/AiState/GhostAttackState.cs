using UnityEngine;

public class GhostAttackState : GhostBaseState
{
    float TimeHit = 2, Sec = 1.5f;
    bool hitplayer, PlayerInRange;
    public override void EnterState(GhostStateManager state)
    {
        //Debug.Log("Attack");
        TimeHit = Sec;
        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.clip = state.AttackS;
        state.GhostAudioSoure.Play();
        state.GhostBoxCol.enabled = false;
        if (state.GhostID != 5 || state.GhostID != 10 && !state.DollOnHand)
            state.AttackBox.enabled = true;
        else if(state.GhostID == 10 )
        {
            state.AttackBox.enabled = false;
        }
        state.MoveSound.Stop();
        PlayerInRange = false;

        if (state.GhostID != 10)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("atkanimation"))
                state.GhostAni.Play("atkanimation", 0, 0);
            state.enemyGhost.speed = 0f;

        }

        if (state.DollOnHand) Debug.Log("DollonHand");
    }

    public override void UpdateState(GhostStateManager state)
    {
        TimeHit -= Time.deltaTime;
        if (state.AnimAttack)
        {
            state.DelayHitPlayer = 2;

          /*  if (state.GhostID != 10)
            {
                if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("atkanimation"))
                    state.GhostAni.Play("atkanimation", 0, 0);
                state.enemyGhost.speed = 0f;

            }*/

             if( state.GhostID == 10)
            {
                if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("atkanimation"))
                    state.GhostAni.Play("atkanimation", 0, 0);
                state.enemyGhost.speed = state.GranmaAttactSpeed;

            }

            state.AnimAttack = false;
        }

            state.DrawVisionCone();

        if (hitplayer)
        {
            TimeHit = Sec;
            state.CanseePlayer = false;
            PlayerInRange = false;
            if(state.GhostID != 5 && state.GhostID != 10) 
            state.AttackBox.enabled = false;
            state.BossAttacked = true;
            state.SwitchState(state.AlertState);
            hitplayer = false;
        }

        if (!PlayerInRange)
        {
            if (TimeHit < 0.5f)
            {
                if (state.GhostID != 10 && state.DollOnHand)
                {
                    if (state.enemyGhost.remainingDistance < 2.5f)
                    {

                        if (state.HitPlayer)
                        {
                            state.CanseePlayer = false;
                            // state.HpPlayer.Takedamage(1);
                            state.invManager.GetSelectedItem(true);
                            state.HitPlayer = false;
                            state.SwitchState(state.SearchState);
                            PlayerInRange = true;
                        }

                        state.AnimAlert = true;

                    }
                    else
                    {
                        if (!state.PCam.hiding)
                            hitplayer = true;

                        PlayerInRange = true;
                    }
                }

            
            } 

            if (state.GhostID == 10)
                {
                    if (Vector3.Distance(state.enemyGhost.transform.position, state.playerPos.position) < state.GranmahitBox)
                    {

                        if (state.HitPlayer)
                        {
                            state.CanseePlayer = false;
                             state.HpPlayer.Takedamage(4);
                            state.HitPlayer = false;
                            PlayerInRange = true;
                        }

                        state.AnimAlert = true;

                    }
                }
        }

        if (TimeHit < 0)
        {
            if (!state.PCam.hiding) 
            hitplayer = true;
            state.CanseePlayer = false;
        }

    }
}
