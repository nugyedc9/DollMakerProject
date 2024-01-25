using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallInBox : MonoBehaviour
{
    public int passBoxCount, RandomPassBox;
    public MiniGame CountpassBox;
    public GameObject[] HitBox;
    private bool pass, Example = true, Reset, ResetColor;

    private void Update()
    {
        if(passBoxCount >= 9) passBoxCount = 0;
        if (Reset)
        {
            passBoxCount = 0;
            HitBox[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[3].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[4].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[5].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[6].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[7].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[8].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            Reset = false;
        }
        if (!pass)
        {
            RandomPassBox = Random.Range(1, 3);
            pass = true;
        }
        if (ResetColor)
        {
            HitBox[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[3].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[4].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[5].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[6].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[7].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            HitBox[8].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            ResetColor = false;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BoxSkillCheck")
        {
            passBoxCount++; 
            CountpassBox.PassBoxcheck();
            pass = false;
            if (Example)
            {
                if (RandomPassBox == 2 && passBoxCount != 9 )
                {
                    CountpassBox.AddBoxNumber(passBoxCount);
                    HitBox[passBoxCount].GetComponent<Image>().color = new Color32(255,0,0,255);
                }
            }
            
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BoxSkillCheck")
        {
            if(!Example) 
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    CountpassBox.CurrectBoxCheck();
                    HitBox[passBoxCount].GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                }
            }
        }
    }

    public void EndExample()
    {
        Example = false;
    }

    public void StartExample()
    {
        Example = true; 
    }

    public void ResetPassBox()
    {
        Reset = true;
    }

    public void ResetColorBox()
    {
        ResetColor = true;
    }
}
