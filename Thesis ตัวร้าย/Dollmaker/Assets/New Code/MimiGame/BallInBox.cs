using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInBox : MonoBehaviour
{
    public float passBoxCount, RandomPassBox;
    public MiniGame CountpassBox;
    public GameObject[] HitBox;
    private bool pass, Example = true, Reset;

    private void Update()
    {
        if(passBoxCount >= 9) passBoxCount = 0;
        if (Reset)
        {
            passBoxCount = 0;
            Reset = false;
        }
        if (!pass)
        {
            RandomPassBox = Random.Range(1, 4);
            pass = true;
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
                if (RandomPassBox == 2 && passBoxCount != 9)
                {
                    CountpassBox.AddBoxNumber(passBoxCount);
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
}
