using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicToHitBig : MonoBehaviour
{
    public bool colleded;
    public float DamageMagic;
    MonCha moncha;
    public Enum.Type typePlayer;

    void Update()
    {

    }

    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Enemy" && !colleded)
        {
  colleded = true;
            moncha = co.gameObject.GetComponent<MonCha>();
            if (moncha != null) { }
            moncha.TakeDamageMon(DamageMagic, typePlayer);

        }
        else if (co.gameObject.tag != "Magic")
        {
            colleded = true;
            Debug.Log("Hitobj");
        }

        Destroy(gameObject);

    }
}
