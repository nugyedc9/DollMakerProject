using UnityEngine;

public class GhostWalkState : GhostBaseState
{
    public override void EnterState(GhostStateManager state)
    {
      //  Debug.Log("Walk");
       state.GhostFrom.SetActive(true);
       state.GhostAudioSoure.loop = true;
        state.GhostAudioSoure.Stop();
        state.MoveSound.loop = true;
        state.MoveSound.clip = state.WalkS;
        state.MoveSound.Play();
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.DrawVisionCone();

        state.Dest = state.CurrentDest.position;
        state.enemyGhost.destination = state.Dest;
        state.enemyGhost.speed = state.WalkSpeed;
        if (state.AnimWalk)
        {
            if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("walkanimation"))
                state.GhostAni.Play("walkanimation", 0, 0);
            state.AnimWalk = false;
        }
        if(Vector3.Distance(state.Dest, state.enemyGhost.gameObject.transform.position) <= 0)
        {
            state.RandomInIdle = true;
            //Debug.Log("IdleAfterWalk");
            state.SwitchState(state.IdleState);
        }
    }
}
