using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesignSelect : MonoBehaviour
{
    public GameObject[] ClothDesign;
    public GameObject[] BookPage;
    public GameObject[] ClothColor;
    public GameObject BoxColPushCloth;
    public GameObject SpawnPoint;
    public PlayerAttack playerAttack;

    int PageNum;
    [SerializeField] private bool haveCloth;
    public bool HaveCloth {  get { return haveCloth; } set {  haveCloth = value; } }


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
            ClothColor[PageNum].SetActive(true);
        }
    }

    public void NextPage()
    {
        PageNum++;
        BookPage[PageNum].SetActive(true);
        ClothColor[PageNum].SetActive(true);
        if (PageNum > BookPage.Length) PageNum = BookPage.Length; 
    }

    public void PrevPage()
    {
        PageNum--;
        BookPage[PageNum].SetActive(false);
        ClothColor[PageNum].SetActive(true);
        if (PageNum < BookPage.Length) PageNum = 0;
    }

    public void SelectThisDesign()
    {
        GameObject SpawnClothDesign = Instantiate(ClothDesign[PageNum], new Vector3(0,0,0), Quaternion.identity);
        SpawnClothDesign.transform.SetParent(SpawnPoint.transform, false);
        ClothColor[PageNum].SetActive(false);
        HaveCloth = false;
        BoxColPushCloth.SetActive(true);
    }

}
