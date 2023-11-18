using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [Header("Hp Thing")]

    public float MaxHp;
    public float curHp;
    public float Delayvideo;
    public GameObject Hp1, Hp2, DeadCanva, Takeingeyes;
    private bool Playvideo;


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
        if (curHp < 2)
        {
            Hp1.SetActive(true);
            if(!Playvideo)
            {
                Takeingeyes.SetActive(true);
                StartCoroutine(Takeyourballs());
                Playvideo = true;
            }
        }

    }
    public void Takedamage(float damage)
    {
        curHp -= damage;
    }

    IEnumerator Takeyourballs()
    {
        yield return new WaitForSeconds(Delayvideo);
        Takeingeyes.SetActive(false);
    }

}
