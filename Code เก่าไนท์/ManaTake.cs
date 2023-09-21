using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaTake : MonoBehaviour
{
    public bool colleded;
    public int ManaRe;
    PlayerSP target;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Player" && !colleded)
        {
            colleded = true;
            target = co.gameObject.GetComponent<PlayerSP>();
            if (target != null)
                target.ReMana(ManaRe);
            Destroy(gameObject);  

        }   
        
          
    }
}
