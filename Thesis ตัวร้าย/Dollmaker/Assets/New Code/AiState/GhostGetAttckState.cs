using UnityEngine;

public class GhostGetAttckState : GhostBaseState
{


    public override void EnterState(GhostStateManager state)
    {
        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.clip = state.DiedS;
        state.GhostAudioSoure.Play();
       // state.GhostAmbi.Stop();
       // state.PAttack.Attack = false;

        state.enemyGhost.speed = state.HuntSpeed;
       state.FireSound.Play();
       
        if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Damage_ani"))
            state.GhostAni.Play("Damage_ani", 0, 0);
        state.particle.Play();


        //  Debug.Log("GetAttack");
    }

    public override void UpdateState(GhostStateManager state)
    {
       // state.Cansee = false;

        state.DrawVisionCone();
        state.CurrentDest = state.playerPos.transform;
        state.Dest = state.CurrentDest.position;
          state.enemyGhost.destination = state.Dest;

        /*        if (state.ChangePos)
                {
                    if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Damage_ani"))
                        state.GhostAni.Play("Damage_ani", 0, 0);
                    state.particle.Play();
                    state.ChangePos = false;
                }
        */


        if (state.enemyGhost.speed < 1)
        {
            state.enemyGhost.speed = 1;
        }
        else if (state.enemyGhost.speed > 1.5f)
        {
            state.enemyGhost.speed -= 1f * Time.deltaTime;
        }


        if (state.HitDelay > 0)
        {
            state.HitDelay -= Time.deltaTime;
        }
        

        if (state.HpGhost <= 0)
        {
            if (state.HitDelay < 0)
            {
              //  Debug.Log("Died");
                state.PAttack.Attack = true;
                state.SwitchState(state.DiedState);
            }
        }
       else if(state.HpGhost > 0) 
        {
            if(state.HitDelay < 0)
            {
                //   Debug.Log("AfterHit");
                state.HpBeforeHit = state.HpGhost;
                state.PAttack.Attack = true;
                state.SwitchState(state.AlertState);
                state.HitDelay = 0;
            }
        }

        if (Vector3.Distance(state.Dest, state.enemyGhost.gameObject.transform.position) <= 2)
        {
            state.AnimAttack = true;
            state.HitPlayer = true;
            state.SwitchState(state.AttckState);
        }

    }
}
