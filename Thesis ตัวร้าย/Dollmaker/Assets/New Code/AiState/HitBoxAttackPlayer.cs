using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAttackPlayer : MonoBehaviour
{
    public PlayerHp playerHp;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerHp.Takedamage(1);
        }
    }

}
