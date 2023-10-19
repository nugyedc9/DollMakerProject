using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPlayMini1 : MonoBehaviour
{

   /* public List<Transform> DestinationItemSpawn;
    Transform CurrentDesSpawn;
    int ReadNumSpawn;
    public int DestinationSpawnAmount;*/

    public GameObject canClick;
    public GameObject MiniG;
    public bool Cloth, Doll;
    public GameObject Dollobj1, Dollobj2, Dollobj3;
    public GameObject Clothobj1, Clothobj2, Clothobj3;  
    public float ClothHave, DollHave;
    

    // Start is called before the first frame update
    void Start()
    {
        Dollobj1.SetActive(false);
        Dollobj2.SetActive(false);
        Dollobj3.SetActive(false);
        Clothobj1.SetActive(false);
        Clothobj2.SetActive(false);
        Clothobj3.SetActive(false);
       /* ReadNumSpawn = Random.Range(0, DestinationSpawnAmount);
        CurrentDesSpawn = DestinationItemSpawn[ReadNumSpawn];*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Cloth && Doll)
        {
            canClick.SetActive(true);
        }
        else
        {
            canClick.SetActive(false);
            MiniG.SetActive(false);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cloth")
        {
            ClothHave++;
            Cloth = true;
            if (ClothHave == 1)
            {
                Clothobj1.SetActive(true);
                Destroy(collision.gameObject);
            }
            if(ClothHave == 2)
            {
                Clothobj2.SetActive(true);
                Destroy(collision.gameObject);
            }
            if (ClothHave == 3)
            {
                Clothobj3.SetActive(true);
                Destroy(collision.gameObject);
            }
        }
        if(collision.gameObject.tag == "Doll")
        {
            DollHave++;
            Doll = true;
            if (DollHave == 1)
            {
                Dollobj1.SetActive(true);
                Destroy(collision.gameObject);
            }
            if(DollHave == 2)
            {
                Dollobj2.SetActive(true);
                Destroy(collision.gameObject);
            }
            if(DollHave == 3)
            {
                Dollobj3.SetActive(true);
                Destroy(collision.gameObject);
            }
        }
    }

    public void Dolllost()
    {
        DollHave--;
        if (DollHave == 0)
        {
            Dollobj1.SetActive(false);
            Doll = false;
        }
        if (DollHave == 1) Dollobj2.SetActive(false);
        if (DollHave == 2) Dollobj3.SetActive(false);

    }
    public void ClothLost()
    {
        ClothHave--;
        if (ClothHave == 0)
        {
            Cloth = false;
            Clothobj1.SetActive(false);
        }
        if (ClothHave == 1) Clothobj2.SetActive(false);
        if (ClothHave == 2) Clothobj3.SetActive(false);
    }

}
