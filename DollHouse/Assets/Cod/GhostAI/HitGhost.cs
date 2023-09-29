using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGhost : MonoBehaviour
{
    public bool colleded;
    public float orbAttack;
    Ghost target;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Ghost" && !colleded)
        {
            colleded = true;
            target = co.gameObject.GetComponent<Ghost>();
            Debug.Log("hit");
            if (target != null) { }
            target.GetHit();

        }
        else if (co.gameObject.tag != "Orb")
        {
            colleded = true;
            print("Hit");
        }

        Destroy(gameObject);

    }
}
