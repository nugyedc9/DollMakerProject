using UnityEngine;

public class GhostHuntState : GhostBaseState
{

    public override void EnterState(GhostStateManager state)
    {
      //  Debug.Log("Hunt");
        state.GhostAudioSoure.loop = true;
        state.GhostAudioSoure.clip = state.HuntS;
        state.GhostAudioSoure.Play();
        state.OnPlayerAudio.enabled = true;
        state.OnPlayerAudio.clip = state.PlayerHeartBeat;
        state.OnPlayerAudio.Play();
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();


        state.enemyGhost.speed = state.HuntSpeed;
        state.CurrentDest = state.playerPos.transform;
        state.Dest = state.CurrentDest.position;
            state.enemyGhost.destination = state.Dest;


        if (state.AnimHunt)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Walk_ani"))
                state.GhostAni.Play("Walk_ani", 0, 0);
            state.AnimHunt = false;
        }

        if(Vector3.Distance(state.Dest, state.enemyGhost.gameObject.transform.position) <= 2)
        {        
            state.AnimAttack = true;
            state.HitPlayer = true;
            state.SwitchState(state.AttckState);
        }
    }
}
