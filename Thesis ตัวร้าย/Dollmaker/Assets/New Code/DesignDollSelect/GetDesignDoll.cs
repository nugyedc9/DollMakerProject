using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetDesignDoll : MonoBehaviour
{
    public GameObject[] BookPage;
    public GameObject NextButt, PrevButt;
    public Item[] FinishDoll;

    public InventoryManager inventoryManager;
    public DollDropDesignTrigger DollDrop;
    public PlayerPickUpItem playpickUp;

    public int PageNum;
    int dollCount;

    [Header("---- Audio ----")]
    public AudioSource AudioSound;
    public AudioClip ConfirmS, NextS, PrevS;

    [SerializeField] int dollDesingID;
    public int DollColorID { get { return dollDesingID; } set { dollDesingID = value; } }

    public UnityEvent FinishDoll1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        #region Button Show up
        if (PageNum == 0) PrevButt.SetActive(false);
        else if (PageNum != 0) PrevButt.SetActive(true);
        if (PageNum == BookPage.Length - 1) NextButt.SetActive(false);
        if (PageNum < BookPage.Length - 1) NextButt.SetActive(true);
        #endregion

    }

    public void NextPage()
    {
        AudioSound.clip = NextS;
        AudioSound.Play();
        PageNum++;
        BookPage[PageNum].SetActive(true);
        BookPage[PageNum - 1].SetActive(false);
        if (PageNum >= BookPage.Length - 1) PageNum = BookPage.Length - 1;
    }

    public void PrevPage()
    {
        AudioSound.clip = PrevS;
        AudioSound.Play();
        PageNum--;
        BookPage[PageNum + 1].SetActive(false);
        BookPage[PageNum].SetActive(true);
        if (PageNum <= 0) PageNum = 0;
    }
    public void SelectThisDesign()
    {
        if (playpickUp.ItemCount < inventoryManager.inventoryslote.Length)
        {
            dollCount++;
            if (dollCount == 1) FinishDoll1.Invoke();
            AudioSound.clip = ConfirmS;
            AudioSound.Play();
            inventoryManager.AddItem(FinishDoll[DollColorID]);
            DollDrop.GetFinishDoll();
        }
    }

}
