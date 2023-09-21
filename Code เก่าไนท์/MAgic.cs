using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAgic : MonoBehaviour
{
    public bool colleded;
    public float DamageMagic;
    Target target;
    public Enum.Type typePlayer;

    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision co)
    {  
        if(co.gameObject.tag == "Enemy" && !colleded)
        {
            colleded = true;
            target = co.gameObject.GetComponent<Target>();
            Debug.Log("hit");
            if (target != null) { }  
            target.TakeDamageMon(DamageMagic, typePlayer);
            
        }
        else if(co.gameObject.tag != "Magic")
        {
            colleded = true;
            Debug.Log("Hitobj");
            
        }

            Destroy(gameObject);
        
    }
}
