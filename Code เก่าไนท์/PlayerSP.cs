using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSP : MonoBehaviour
{

    public float MaxSP = 100;
    public float CurSp;
    public float Manaree;

    public SpBar SPbar;


    void Start()
    {
        CurSp = MaxSP;
        SPbar.SetMaxSP(MaxSP);
    }

    void Update()
    {
        if (CurSp < 0)
        {
            CurSp = 0;
        }
        if (CurSp < MaxSP)
        {
            AutoRe(Manaree);
        }
    }

    public void ManaUse(float manaTake)
    {

        CurSp -= manaTake;
        SPbar.SetSP(CurSp);

    }

    public void ReMana(float Manare)
    {
        CurSp += Manare;
        SPbar.SetSP(CurSp);
    }
    public void AutoRe(float AutoRe)
    {
        CurSp += AutoRe * Time.deltaTime;
        SPbar.SetSP(CurSp);
    }
}


