using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaginMonManaDrop : MonoBehaviour
{
    public bool colleded;
    public int DamageMon;
    public GameObject Mana;
    Player P;
    public float HpMagic = 5;


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
            DestoryMagic();
        }

        else if (co.gameObject.tag != "Magic" )
        {
            colleded = true;
            Debug.Log("Hitobj"); 
            DestroyEnemy();
            DropMana();

        }
      
       

    }

    public void HitMagic(float manaTake)
    {

        HpMagic -= manaTake;

    }

    void DestoryMagic()
    {
        Destroy(gameObject);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    public void DropMana()
    {
        Vector3 position = transform.position;
        GameObject mana = Instantiate(Mana, position + new Vector3(0f, 1f, 0f), Quaternion.identity);
    }
}
