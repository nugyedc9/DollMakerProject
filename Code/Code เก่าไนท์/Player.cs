using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHp = 100;
    public float CurHp;


    public Hpbar HPbar;


    void Start()
    {
        CurHp = MaxHp;
        HPbar.SetMaxHp(MaxHp);
    }

    void Update()
    {
        if (CurHp < 0)
        {
            CurHp = 0;
        }
    }

    public void TakeDamage(float Damage)
    {
        CurHp -= Damage;
        HPbar.SetHP(CurHp);
    }

    public void Hpre(float Manare)
    {
        CurHp += Manare;
        HPbar.SetHP(CurHp);
    }
}
