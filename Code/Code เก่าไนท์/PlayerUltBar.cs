using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltBar : MonoBehaviour
{
    public float MaxUlt = 100;
    public float MinUlt = 0;
    public float curUlt;

    public UltBar ULTBar;


    void Start()
    {
        curUlt = MinUlt;
        ULTBar.SetLowUlt(MinUlt);
    }

    void Update()
    {
        if (curUlt > 100)
        {
            curUlt = 100;
        }
    }

    public void UltUse(float manaTake)
    {

        curUlt -= manaTake;
        ULTBar.SetSP(curUlt);

    }

    public void ReUlt(float Manare)
    {
        curUlt += Manare;
        ULTBar.SetSP(curUlt);
    }
}
