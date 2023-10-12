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
    public GameObject Hp1, Hp2, DeadCanva;


    public void Start()
    {
        curHp = MaxHp;
    }

    public void Update()
    {
        if (curHp < 0)
            curHp = 0;
        if(curHp < 1)
        {
            DeadCanva.SetActive(true);
            Time.timeScale = 0f;
        }
        if (curHp < MaxHp)
            AutoReHp(ReHp);
        #region HpCanva
        if (curHp < 2)
            Hp1.SetActive(true);
       /* if(curHp < 3)
            Hp2.SetActive(true);*/
        if(curHp >= 2) Hp1.SetActive(false);
        if(curHp >=3) Hp2.SetActive(false);
        #endregion
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
