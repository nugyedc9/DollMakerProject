using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesignSelect : MonoBehaviour
{
    public GameObject[] BookPage;
    public GameObject[] ClothColor;
    public GameObject BoxColPushCloth;
    public GameObject NextButt,PrevButt,SelectButt;
    public PlayerAttack playerAttack;

    public int PageNum;
    [SerializeField] private bool haveCloth;
    public bool HaveCloth {  get { return haveCloth; } set {  haveCloth = value; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerAttack.DesignNum = PageNum;
        if (HaveCloth)
        {
            BoxColPushCloth.SetActive(false);
            ClothColor[PageNum].SetActive(true);
            BookPage[PageNum].SetActive(true);
            NextButt.SetActive(true);
            PrevButt.SetActive(true);
            SelectButt.SetActive(true);
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
        PageNum++;
        BookPage[PageNum].SetActive(true);
        BookPage[PageNum - 1].SetActive(false);
        if (HaveCloth)
        {
            ClothColor[PageNum].SetActive(true);
            ClothColor[PageNum - 1].SetActive(false);
        }
        if (PageNum >= BookPage.Length - 1) PageNum = BookPage.Length - 1;
    }

    public void PrevPage()
    {
        PageNum--;
        BookPage[PageNum + 1].SetActive(false);
        BookPage[PageNum].SetActive(true);
        if (HaveCloth)
        {
            ClothColor[PageNum + 1].SetActive(false);
            ClothColor[PageNum].SetActive(true);
        }
        if (PageNum <= 0) PageNum = 0;
    }

    public void SelectThisDesign()
    {
            ClothColor[PageNum].SetActive(false);
            playerAttack.GetclothDesign();
            BoxColPushCloth.SetActive(true);
                 HaveCloth = false;
        
    }

}
