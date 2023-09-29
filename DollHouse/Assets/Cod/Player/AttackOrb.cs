using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrb : MonoBehaviour
{
    public bool colleded;
    public float OebDamage;
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
            Debug.Log("hitGhost");
            if (target != null) { }
            target.GetHit();

        }
        else if (co.gameObject.tag != "Orb")
        {
            colleded = true;
            Debug.Log("Hitobj");

        }

        Destroy(gameObject);

    }
}
