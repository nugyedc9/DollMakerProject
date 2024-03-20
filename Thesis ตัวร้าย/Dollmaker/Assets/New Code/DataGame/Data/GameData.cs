using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerposition;
    private Vector3 CurPos, firstPos;
    int poscount;

    public GameData()
    {
        playerposition = CurPos;


    }


    public void PlayerPoS(Vector3 PPos)
    {
        CurPos = PPos;
        poscount++;
    }
}

