using System.Collections;
using UnityEngine;

public class GhsotSpawnState : GhostBaseState
{

    float Timer;
    bool AnimChange;
    int ghostNum , ranmaxspawn;
    
    public override void EnterState(GhostStateManager state)
    {
        AnimChange = true;
        /* if (ghostNum == 0) Timer = state.SpawnTimer;
         else if(ghostNum >= 1) Timer = state.SpawnTimer;*/

       // state.PlayerDetectSpawn = true;
        //state.GhostBoxCol.enabled = true;
        state.Cansee = false;
        state.HpCross = false;
        state.PlayerHitDelay = state.HuntSpeed;
        state.GhostLight.SetActive(true);
        state.GhostFrom.SetActive(false);
     /*   if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Shake_ani"))
            state.GhostAni.Play("Shake_ani", 0, 0);*/
      //  ghostNum++;
    }

    public override void UpdateState(GhostStateManager state)
    {
        state.SpawnTimer = state.SpawnTimer - (1 * Time.deltaTime);

        state.AnimSpawn = false;
        if (state.SpawnTimer < 0)
        {
            state.PlayerDetectSpawn = false;
            state.Cansee = true;
            state.GhostLight.SetActive(false);
            state.RandomInIdle = true;

            if(state.WherePlayer.PosId < 17)
            state.CurSpawn = Random.Range(0, 3);
            else if(state.WherePlayer.PosId >= 17) 
                state.CurSpawn = Random.Range(4, 7);

            state.enemyGhost.Warp(state.SpawnPoint[state.CurSpawn].position);

            state.SwitchState(state.IdleState);
            state.SpawnTimer = 0;
        }

        /*if(state.SpawnTimer < Timer / 12)
        {
            if (AnimChange)
            {
                state.GhostAudioSoure.clip = state.SpawnS;
                state.GhostAudioSoure.Play();
                *//*if (!state.GhostAni.GetCurrentAnimatorStateInfo(0).IsName("Transform_ani"))
                    state.GhostAni.Play("Transform_ani", 0, 0);*//*
                AnimChange = false;
            }
        }*/
    }

}
