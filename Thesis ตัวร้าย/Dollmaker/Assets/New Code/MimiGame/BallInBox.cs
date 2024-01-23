using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInBox : MonoBehaviour
{
    public float passBoxCount;
    public MiniGame CountpassBox;

    private void Update()
    {
        if(passBoxCount >= 9) passBoxCount = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BoxSkillCheck")
        {
            passBoxCount++; 
            CountpassBox.PassBoxcheck();
        }
    }
}
