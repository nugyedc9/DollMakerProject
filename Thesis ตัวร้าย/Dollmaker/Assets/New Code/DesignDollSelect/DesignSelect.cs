using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesignSelect : MonoBehaviour
{
    public GameObject[] BookPage;
    public GameObject[] ClothColor;
    public GameObject[] ClothCutline, FirstLineCut;
    public GameObject BoxColPushCloth;
    public GameObject NextButt,PrevButt,SelectButt,FinishButt;

    public PlayerChangeCam PCam;
    public PlayerPickUpItem playPickup;
    public InventoryManager inventoryManager;
    public Item[] PieceCloth;

    [Header("Tutorial Arrow")]
    public GameObject clothTutorial;

    [Header("---- Audio ----")]
    public AudioSource AudioSound;
    public AudioClip ConfirmS, NextS, PrevS;

    public int PageNum;
    [SerializeField] private bool haveCloth;
    public bool HaveCloth {  get { return haveCloth; } set {  haveCloth = value; } }

    bool ConfirmThis;

    [SerializeField] private LineShow lineShow;

    [SerializeField] int clothColorID;
    public int ClothColorID {  get { return clothColorID; } set {  clothColorID = value; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HaveCloth)
        {
            BoxColPushCloth.SetActive(false);
            ClothColor[ClothColorID].SetActive(true);
            lineShow = ClothCutline[clothColorID].GetComponent<LineShow>();
            BookPage[PageNum].SetActive(true);
            NextButt.SetActive(true);
            PrevButt.SetActive(true);
            if (!ConfirmThis)
            {
                if (PageNum == clothColorID) SelectButt.SetActive(true);
                else SelectButt.SetActive(false);
            }

            PCam._1DesignCloth = true;
            clothTutorial.SetActive(false);
        }
        else SelectButt.SetActive(false);


        #region Button Show up
        if (PageNum == 0) PrevButt.SetActive(false);
        else if (PageNum != 0) PrevButt.SetActive(true);
        if(PageNum == BookPage.Length - 1) NextButt.SetActive(false);
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
            AudioSound.clip = ConfirmS;
            AudioSound.Play();
            ClothCutline[PageNum].SetActive(true);
            ConfirmThis = true;
        SelectButt.SetActive(false);
            /* ClothColor[PageNum].SetActive(false);
            inventoryManager.AddItem(PieceCloth[PageNum]);
            BoxColPushCloth.SetActive(true);
            HaveCloth = false;*/
       
    }

    public void finishThiDesign()
    {
        if (playPickup.ItemCount < inventoryManager.inventoryslote.Length)
        {
            AudioSound.clip = ConfirmS;
            AudioSound.Play();
            ClothColor[PageNum].SetActive(false);           
            lineShow.CloseAllLine();
            ClothCutline[clothColorID].SetActive(false);
            inventoryManager.AddItem(PieceCloth[PageNum]);
            BoxColPushCloth.SetActive(true);
            FirstLineCut[clothColorID].SetActive(true);
            FinishButt.SetActive(false);
            ConfirmThis = false;
            HaveCloth = false;
        }
        Debug.Log("click");
    }

}
