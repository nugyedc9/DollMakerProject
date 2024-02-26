using UnityEngine;

public class GhostGetAttckState : GhostBaseState
{
    float CurDelay;

    public override void EnterState(GhostStateManager state)
    {
        state.GhostAudioSoure.loop = false;
        state.GhostAudioSoure.clip = state.DiedS;
        state.GhostAudioSoure.Play();
        state.GhostAmbi.Stop();
        state.PAttack.Attack = false;
        CurDelay = 2;
      //  Debug.Log("GetAttack");
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.Cansee = false;
        state.DrawVisionCone();
        if (state.ChangePos)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Damage_ani"))
                state.GhostAni.Play("Damage_ani", 0, 0);
            state.ChangePos = false;
        }

        if(CurDelay > 0)
        {
            CurDelay -= Time.deltaTime;
        }
        

        if (state.HpGhost <= 0)
        {
            if (CurDelay < 0)
            {
              //  Debug.Log("Died");
                state.PAttack.Attack = true;
                state.SwitchState(state.DiedState);
            }
        }
       else if(state.HpGhost > 0) 
        {
            if(CurDelay < 0)
            {
             //   Debug.Log("AfterHit");
                state.PAttack.Attack = true;
                state.SwitchState(state.IdleState);
                CurDelay = 0;
            }
        }


    }
}
