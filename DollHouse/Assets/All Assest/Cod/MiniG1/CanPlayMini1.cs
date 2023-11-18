using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanPlayMini1 : MonoBehaviour
{

   /* public List<Transform> DestinationItemSpawn;
    Transform CurrentDesSpawn;
    int ReadNumSpawn;
    public int DestinationSpawnAmount;*/

    public GameObject canClick;
    public GameObject MiniG;
    public bool Cloth, Doll;
    public GameObject[] Dollobj;
    public GameObject[] Clothobj;
    public int ClothHave, DollHave, TotelDollHave;
    public Player player;
    public TextMeshProUGUI DollTotel;
    public TextMeshProUGUI ClothTotel;
    public GameObject TotalDollCanva;
    public TextMeshProUGUI TotalDoll;
    

    // Start is called before the first frame update
    void Start()
    {
       /* ReadNumSpawn = Random.Range(0, DestinationSpawnAmount);
        CurrentDesSpawn = DestinationItemSpawn[ReadNumSpawn];*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Cloth && Doll)
        {
            canClick.SetActive(true);
            player.PlaySoundWork();
            TotalDollCanva.SetActive(true);
        }
        else
        {
            canClick.SetActive(false);
            MiniG.SetActive(false);
            player.StopSoundWork();
        }
        DollTotel.text = "Doll " + DollHave + " / 6";
        ClothTotel.text = "Cloth " + ClothHave + " / 6";

        TotalDoll.text = "Finish Doll :  " + TotelDollHave + " / 6";
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Cloth")
        {
            ClothHave++;
            Cloth = true;
            Clothobj[ClothHave].SetActive(true);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Doll")
        {
            DollHave++;
            Doll = true;
            Dollobj[DollHave].SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    public void Dolllost()
    {
        Dollobj[DollHave].SetActive(false);
        DollHave--;
        if (DollHave == 0) Doll = false;
    }
    public void ClothLost()
    {
        Clothobj[ClothHave].SetActive(false );
        ClothHave--;
        if(ClothHave == 0) Cloth = false;
    }

    public void AddTotalDoll()
    {
        TotelDollHave++;
    }
    public void LosttotalDoll()
    {
        TotelDollHave--;
    }

}
