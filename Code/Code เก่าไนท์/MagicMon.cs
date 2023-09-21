using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMon : MonoBehaviour
{

    public bool colleded;
    public int DamageMon;
    Player P;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {

        if (co.gameObject.tag == "Player" && !colleded)
        {
            colleded = true;
            P = co.gameObject.GetComponent<Player>();
            if (P != null)
                P.TakeDamage(DamageMon);

        }

        else if (co.gameObject.tag != "Magic")
        {
            colleded = true;
            Debug.Log("Hitobj");

        }

        Destroy(gameObject);

    }
}
