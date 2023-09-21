using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPTake : MonoBehaviour
{
    public bool colleded;
    public int HPre;
    Player target;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Player" && !colleded)
        {
            colleded = true;
            target = co.gameObject.GetComponent<Player>();
            if (target != null)
                target.Hpre(HPre);
            Destroy(gameObject);

        }


    }
}
