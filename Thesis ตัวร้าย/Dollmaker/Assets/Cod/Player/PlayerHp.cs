using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public PlayerAttack PAttack;
    public Animator PlayerGetHit;

    [Header("Hp Thing")]

    public int MaxHp;
    public int curHp;
    public float Delayvideo,DeadDelayvideo;
    public GameObject[] HpPic, DMGPic;
    public GameObject Hp1, Hp2, DeadCanva, Takeingeyes,CutLine, blurEye, DeadVideo,THowToHeal;
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
               // DeadVideo.SetActive(true);
                StartCoroutine(DeadPlay());
                Playdead = true;
            }
        }
      /*  if (curHp < 3 && normaleye)
        {
            if(!PlayGetHit)
            {
               // Takeingeyes.SetActive(true);
                StartCoroutine(Takeyourballs());
              //  Hp1.SetActive(true);
                PlayGetHit = true;
                normaleye = false;            
            }
        }*/
        if(curHp < 2)
        {
            blurEye.SetActive(true);
        }
        if(curHp >= 2)
        {
            blurEye.SetActive(false);
        }
        if (Delayvideo > 0) Delayvideo -= Time.deltaTime;
        if(Delayvideo < 0)
        {
            PlayerGetHit.enabled = false;
            Hp2.SetActive(false);
            Delayvideo = 0;
        }
    }
    public void Takedamage(int damage)
    { 
        int i = curHp - 1;
        Hp2.SetActive(true);
        PlayerGetHit.enabled = true;
        PlayerGetHit.Play("PlayerGetHit");
        Delayvideo = 3;
        HpPic[i].SetActive(false);
        DMGPic[i].SetActive(true);
        curHp -= damage;     
    }

    public void Heal()
    {
        int i = curHp;
        curHp++;
        HpPic[i].SetActive(true);
        DMGPic[i].SetActive(false);
        if (curHp > 3)
        {
            blurEye.SetActive(false);
        }
        PlayGetHit = false;
      
    }

    public void openEyes()
    {
       // Hp1.SetActive(false);
        blurEye.SetActive(true);
      //  CutLine.SetActive(true);
        StartCoroutine(CutLineEye());
    }

    IEnumerator Takeyourballs()
    {
        yield return new WaitForSeconds(2f);
       // Takeingeyes.SetActive(false);
        if (!tuHeal)
        {
            THowToHeal.SetActive(true);
            tuHeal = true;
            PAttack.DelayTHeal();
            PAttack.StopAttack();
        }
    }

    IEnumerator CutLineEye()
    {
        yield return new WaitForSeconds(3);
        CutLine.SetActive(false);
    }

    IEnumerator DeadPlay()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DeadCanva.SetActive(true);
    }
}
