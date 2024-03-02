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

    public GameObject DropClothHere, minigame;
    public bool  Doll;
    [SerializeField] bool cloth;
    public bool Cloth { get {  return cloth; }  set { cloth = value; } }
    [SerializeField] bool onDesk;
    public bool OnDesk { get { return onDesk; } set { onDesk = value; } }
    public GameObject[] Dollobj;
    public GameObject[] Clothobj;
    public int ClothHave, DollHave;
    [SerializeField] int totalDollHave;
    public int TotalDollHave { get { return totalDollHave; } set { totalDollHave = value; } }
    public PlayerChangeCam DeskView;
    public MiniGameAuidition minigamestate;
    public ClothColorDrop clothColorDrop;
    public TextMeshProUGUI DollTotel;
    public TextMeshProUGUI ClothTotel;
    public GameObject TotalDollCanva;
    public TextMeshProUGUI TotalDoll;
    public Animator animator;
    public Animator ClothMove, HAndMove;

    [Header("Finish doll")]
    public GameObject[] FinshDoll;
    private int FinishDollHave;
    private float DelayFinish1doll;
    public GameObject Finishgame;
    public AudioSource MakeDollFinish;

    public UnityEvent Dolls6;


    // Start is called before the first frame update
    void Start()
    {
       /* ReadNumSpawn = Random.Range(0, DestinationSpawnAmount);
        CurrentDesSpawn = DestinationItemSpawn[ReadNumSpawn];*/
       animator.enabled = false;
       ClothMove.enabled = false;
        HAndMove.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cloth)
        {
            // canClick.SetActive(true);
            //  player.PlaySoundWork();
            //  TotalDollCanva.SetActive(true);
            if(!DeskView.camOnPerSon)
            CloseMouse();
            DropClothHere.SetActive(false);
            DeskView.CanplayMinigame = true;
            minigamestate.HaveItem = true;
        }
        else
        {
            if (onDesk)
            {
                DropClothHere.SetActive(true);
            }
            else DropClothHere.SetActive(false);

            DeskView.CanplayMinigame = false;
            minigame.SetActive(false);
            minigamestate.LeaveMinigame();
            clothColorDrop.Finishmakecloth();
            minigamestate.HaveItem = false;

            // player.StopSoundWork();
        }
        /*DollTotel.text = "Doll " + DollHave + " / 6";
        ClothTotel.text = "Cloth " + ClothHave + " / 6";

        TotalDoll.text = "Finish Doll :  " + TotelDollHave + " / 6";*/

        if(TotalDollHave == 6)
        {
            Dolls6.Invoke();
        } 

        if(DelayFinish1doll > 0) DelayFinish1doll -= Time.deltaTime;
        if(DelayFinish1doll < 0)
        {
            Time.timeScale = 0f;
            Finishgame.SetActive(true);
        }
    }

/*
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
    }*/

    public void AddDollOnDesk(int i)
    {
        Dollobj[DollHave].SetActive(true);
        Doll = true;
        DollHave += i;
    }

    public void AddClothOndesk(int i)
    {
        Clothobj[ClothHave].SetActive(true);
        Cloth = true;
        ClothHave += i;
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
        DelayFinish1doll = 5f;
        Dolllost();
        ClothLost();
    }

    public void ShowMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


}
