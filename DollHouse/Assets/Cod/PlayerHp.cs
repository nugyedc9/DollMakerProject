using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [Header("Hp Thing")]

    public float MaxHp;
    public float curHp;
    public float ReHp;


    public void Start()
    {
        curHp = MaxHp;
    }

    public void Update()
    {
        if (curHp < 0)
            curHp = 0;
        if (curHp < MaxHp)
            AutoReHp(ReHp);
    }
    public void Takedamage(float damage)
    {
        curHp -= damage;
    }
    public void AutoReHp(float Re)
    {
        curHp += Re * Time.deltaTime;
    }
}
