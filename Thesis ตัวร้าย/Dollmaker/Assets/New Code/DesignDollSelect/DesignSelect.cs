using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesignSelect : MonoBehaviour
{
    public GameObject[] BookPage;
    public GameObject[] ClothColor;
    public GameObject[] ClothCutline, FirstLineCut;
  //  public GameObject BoxColPushCloth;
    public GameObject NextButt,PrevButt,SelectButt,FinishButt;

    public PlayerChangeCam PCam;
    public PlayerPickUpItem playPickup;
    public InventoryManager inventoryManager;
    public CanPlayMini1 CanPlayminigame;
    public MiniGameAuidition minigame;
    public ClothColorDrop clothSwingID;
    public Item[] PieceCloth;


    [Header("ClothOnSwing")]
    public GameObject[] ClothOnSwing;


    [Header("Tutorial Arrow")]
    public GameObject clothTutorial;
    public GameObject CutLineTutorial, CutLineNote;
    [SerializeField] bool _1ctline;
    public bool _1CutLine {  get { return _1ctline; } set {  _1ctline = value; } }

    [Header("---- Audio ----")]
    public AudioSource AudioSound;
    public AudioClip ConfirmS, NextS, PrevS;

    public int PageNum;
    [SerializeField] private bool haveCloth;
    public bool HaveCloth {  get { return haveCloth; } set {  haveCloth = value; } }

    public int NextClothID;

    bool ConfirmThis;

    [SerializeField] private LineShow lineShow;

    [SerializeField] int clothColorID;
    public int ClothColorID {  get { return clothColorID; } set {  clothColorID = value; } }

    public bool SelecRed, SelecBlue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HaveCloth)
        {
            // BoxColPushCloth.SetActive(false);
            ClothColor[PageNum].SetActive(true);
            lineShow = ClothCutline[PageNum].GetComponent<LineShow>();
            BookPage[PageNum].SetActive(true);
            NextButt.SetActive(true);
            PrevButt.SetActive(true);


            /* if (!ConfirmThis)
             {
                 if (PageNum == clothColorID) SelectButt.SetActive(true);
                 else SelectButt.SetActive(false);
             }*/

            PCam._1DesignCloth = true;
            clothTutorial.SetActive(false);
        }
        else
        {
            SelectButt.SetActive(true);
        }

        

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

        if (PageNum == 0) SelecRed = true;
        if (PageNum == 1) SelecBlue = true;
        HaveCloth = true;
            AudioSound.clip = ConfirmS;
            AudioSound.Play();
            ClothCutline[PageNum].SetActive(true);
            ConfirmThis = true;
        if (!_1CutLine)
        {
            CutLineTutorial.SetActive(true);
            CutLineNote.SetActive(true);
        }

        SelectButt.SetActive(false);
            /* ClothColor[PageNum].SetActive(false);
            inventoryManager.AddItem(PieceCloth[PageNum]);
            BoxColPushCloth.SetActive(true);
            HaveCloth = false;*/
       
    }

    public void finishThiDesign()
    {
       

        if (SelecRed)
        {
            ClothColor[0].SetActive(false);
            ClothColor[1].SetActive(true);
            lineShow.CloseAllLine();
            ClothCutline[0].SetActive(false);
            FirstLineCut[1].SetActive(true);
            BookPage[PageNum].SetActive(false);
            PageNum = 1;
            AudioSound.clip = ConfirmS;
            AudioSound.Play();
            ClothCutline[1].SetActive(true);
            ConfirmThis = true;
            minigame.Make2Cloth = true;
            NextClothID = 0;

            FinishButt.SetActive(false);
            StartCoroutine(Delayseletfalse());
        }
        if (SelecBlue)
        {

            ClothColor[1].SetActive(false);
            ClothColor[0].SetActive(true);
            lineShow.CloseAllLine();
            ClothCutline[1].SetActive(false);
            FirstLineCut[0].SetActive(true);
            BookPage[PageNum].SetActive(false);
            PageNum = 0;
            AudioSound.clip = ConfirmS;
            AudioSound.Play();
            ClothCutline[0].SetActive(true);
            ConfirmThis = true;
            minigame.Make2Cloth = true;
            NextClothID = 1;

            FinishButt.SetActive(false);
            StartCoroutine(Delayseletfalse());
        }

        if (!SelecRed && !SelecBlue)
        {
            AudioSound.clip = ConfirmS;
            AudioSound.Play();
                ClothColor[PageNum].SetActive(false);
                lineShow.CloseAllLine();
                ClothCutline[PageNum].SetActive(false);

                // inventoryManager.AddItem(PieceCloth[LockDesign]);



                //  BoxColPushCloth.SetActive(true);
                FirstLineCut[PageNum].SetActive(true);          

            FinishButt.SetActive(false);
            PCam.ChangeToSwing();
            ConfirmThis = false;
            HaveCloth = false;
            SelectButt.SetActive(true);

            ClothOnSwing[PageNum].SetActive(true);
            clothColorID = PageNum;
            CanPlayminigame.Cloth = true;

        }
      //  Debug.Log("click");
    }


    public void CloseClothSwing()
    {
        
        ClothOnSwing[clothColorID].SetActive(false);
    }

    IEnumerator Delayseletfalse()
    {
        yield return new WaitForSeconds(1);
        SelecBlue = false;
        SelecRed = false;
        yield break;
    }
}
