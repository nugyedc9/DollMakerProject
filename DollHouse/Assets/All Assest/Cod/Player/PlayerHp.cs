using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public PlayerAttack PAttack;

    [Header("Hp Thing")]

    public float MaxHp;
    public float curHp;
    public float Delayvideo,DeadDelayvideo;
    public GameObject Hp1, Hp2, DeadCanva, Takeingeyes, blurEye, DeadVideo,THowToHeal;
    private bool PlayGetHit, normaleye, Playdead, tuHeal;


    public void Start()
    {
        curHp = MaxHp;
        normaleye = true;
    }

    public void Update()
    {
        if (curHp < 0)
            curHp = 0;
        if(curHp < 1)
        {
            if(!Playdead)
            {
                DeadVideo.SetActive(true);
                StartCoroutine(DeadPlay());
                Playdead = true;
            }
        }
        if (curHp < 2 && normaleye)
        {
            if(!PlayGetHit)
            {
                Takeingeyes.SetActive(true);
                StartCoroutine(Takeyourballs());
                Hp1.SetActive(true);
                PlayGetHit = true;
                normaleye = false;            
            }
        }

    }
    public void Takedamage(float damage)
    {
        curHp -= damage;
    }

    public void Heal()
    {
        curHp++;
        blurEye.SetActive(false);
        PlayGetHit = false;
        normaleye = true;
    }

    public void openEyes()
    {
        Hp1.SetActive(false);
        blurEye.SetActive(true);
    }

    IEnumerator Takeyourballs()
    {
        yield return new WaitForSeconds(Delayvideo);
        Takeingeyes.SetActive(false);
        if (!tuHeal)
        {
            THowToHeal.SetActive(true);
            tuHeal = true;
            PAttack.DelayTHeal();
        }
    }

    IEnumerator DeadPlay()
    {
        yield return new WaitForSeconds(DeadDelayvideo);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DeadCanva.SetActive(true);
    }
}
