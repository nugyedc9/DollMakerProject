using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CanPlayMini1 : MonoBehaviour
{

   /* public List<Transform> DestinationItemSpawn;
    Transform CurrentDesSpawn;
    int ReadNumSpawn;
    public int DestinationSpawnAmount;*/

    public GameObject canClick;
    public bool Cloth, Doll;
    public GameObject[] Dollobj;
    public GameObject[] Clothobj;
    public int ClothHave, DollHave, TotelDollHave;
    public Player player;
    public PlayerChangeCam minigame;
    public MiniGameAuidition minigamestate;
    public TextMeshProUGUI DollTotel;
    public TextMeshProUGUI ClothTotel;
    public GameObject TotalDollCanva;
    public TextMeshProUGUI TotalDoll;

    [Header("Finish doll")]
    public GameObject[] FinshDoll;
    private int FinishDollHave;

    public AudioSource MakeDollFinish;

    public UnityEvent Dolls6;

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
            // canClick.SetActive(true);
            //  player.PlaySoundWork();
            //  TotalDollCanva.SetActive(true);
            minigame.HaveDollAndCloth();
            minigamestate.ItemHaveCheck();
        }
        else
        {
            //canClick.SetActive(false);
            minigame.dontHaveDollAndCloth();
            minigamestate.LeaveMinigame();
            minigamestate.DontHaveItem();

           // player.StopSoundWork();
        }
        /*DollTotel.text = "Doll " + DollHave + " / 6";
        ClothTotel.text = "Cloth " + ClothHave + " / 6";

        TotalDoll.text = "Finish Doll :  " + TotelDollHave + " / 6";*/

        if(TotelDollHave == 6)
        {
            Dolls6.Invoke();
        } 
        if (DollHave == 0) Doll = false;
        if(ClothHave == 0) Cloth = false;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Cloth")
        {
           Clothobj[ClothHave].SetActive(true);
            Cloth = true;
             ClothHave++;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Doll")
        {
           Dollobj[DollHave].SetActive(true);
            Doll = true;
             DollHave++;
            Destroy(collision.gameObject);
        }
    }

    public void Dolllost()
    {
        DollHave--;
        Dollobj[DollHave].SetActive(false);       
       // MakeDollFinish.Play();      
    }
    public void ClothLost()
    {
        ClothHave--;
        Clothobj[ClothHave].SetActive(false );             
    }

    public void GetFinishDoll()
    {      
        FinshDoll[FinishDollHave].SetActive(true); 
        FinishDollHave++;
    }

    public void FinishDoll()
    {
        Dolllost();
        ClothLost();
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
