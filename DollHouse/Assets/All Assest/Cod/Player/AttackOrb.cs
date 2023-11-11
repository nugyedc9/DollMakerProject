using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrb : MonoBehaviour
{
    public bool colleded;
    public float OebDamage;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Ghost" && !colleded)
        {
            colleded = true;
            Destroy(gameObject);
        }
        else if (co.gameObject.tag != "Orb")
        {
            colleded = true;
            // Debug.Log("Hitobj");
            Destroy(gameObject);

        }


    }
}
