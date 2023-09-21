using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltTake : MonoBehaviour
{
    public bool colleded;
    public int UltRe;
    PlayerUltBar target;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Player" && !colleded)
        {
            colleded = true;
            target = co.gameObject.GetComponent<PlayerUltBar>();
            if (target != null)
                target.ReUlt(UltRe);
            Destroy(gameObject);

        }


    }
}
