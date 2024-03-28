using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DollDropDesignTrigger : MonoBehaviour, IDataGame
{
    public BoxCollider2D Box;
    public GetDesignDoll designSelect;
    public InventoryManager inventoryManager;
    public PlayerPickUpItem playpickUp;
    public GameObject SelectButton;
    public GameObject Doll;
    public GameObject[] DollDesignVisual;
    public PlayerChangeCam PCam;


    [Header("Tutorial Arrow")]
    public GameObject DollTutorial;
    public GameObject ClothTutorial;

    public UnityEvent TakeDoll;


    bool  NeedRed, NeedBlue, _1Doll;
    float dollCount;

    [SerializeField] bool closeboxDropDoll;
    public bool CloseboxDropDoll { get { return closeboxDropDoll; } set {  closeboxDropDoll = value; } }
    [SerializeField] bool dollHave;
    public bool DollHave { get { return dollHave; } set { dollHave = value;} }


    public void Update()
    {
        if(dollCount == 3)
        {
            TakeDoll.Invoke();
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Doll")
        {
            if (!DollHave)
            {
                inventoryManager.GetSelectedItem(true);
                Doll.gameObject.SetActive(true);
                DollDesignVisual[0].SetActive(true);
                dollCount++;
                DollHave = true;
                DollTutorial.SetActive(false);

                if (!_1Doll)
                {
                    ClothTutorial.SetActive(true);
                    PCam._1Doll = true;
                    _1Doll = true;
                }
            }
        }
        if (DollHave)
        {

            if (collision.gameObject.tag == "FinishClothRed")
            {
                ClothTutorial.SetActive(false);
                inventoryManager.GetSelectedItem(true);
                DollDesignVisual[0].SetActive(false);
                if (!NeedRed)
                    DollDesignVisual[1].SetActive(true);
                else
                {
                    DollDesignVisual[5].SetActive(true);
                    DollDesignVisual[1].SetActive(false);
                    designSelect.DollColorID = 0;
                    SelectButton.SetActive(true);
                    CloseboxDropDoll = true;
                    Box.enabled = false;
                }
                NeedBlue = true;
            }
            if (collision.gameObject.tag == "FinishClothBlue")
            {
                ClothTutorial.SetActive(false);
                inventoryManager.GetSelectedItem(true);
                DollDesignVisual[0].SetActive(false);
                if (!NeedBlue)
                    DollDesignVisual[2].SetActive(true);
                else
                {
                    DollDesignVisual[5].SetActive(true);
                    DollDesignVisual[2].SetActive(false);
                    designSelect.DollColorID = 0;
                    SelectButton.SetActive(true);
                    CloseboxDropDoll = true;
                    Box.enabled = false;
                }
                NeedRed = true;
            }
            if (collision.gameObject.tag == "FinishClothGreen")
            {
                ClothTutorial.SetActive(false);
                if (!NeedBlue && !NeedRed)
                {
                    DollDesignVisual[0].SetActive(false);
                    inventoryManager.GetSelectedItem(true);
                    DollDesignVisual[3].SetActive(true);
                    designSelect.DollColorID = 1;
                    SelectButton.SetActive(true);
                    CloseboxDropDoll = true;
                    Box.enabled = false;
                }
            }
            if (collision.gameObject.tag == "FinishClothYellow")
            {
                ClothTutorial.SetActive(false);
                if (!NeedBlue && !NeedRed)
                {
                    DollDesignVisual[0].SetActive(false);
                    inventoryManager.GetSelectedItem(true);
                    DollDesignVisual[4].SetActive(true);
                    designSelect.DollColorID = 2;
                    SelectButton.SetActive(true);
                    CloseboxDropDoll = true;
                    Box.enabled = false;
                }
            }
        }
    }

    public void GetFinishDoll()
    {
        NeedBlue = false;
        NeedRed = false;    
        DollHave = false;
        CloseboxDropDoll = false; SelectButton.SetActive(false);
        DollDesignVisual[0].SetActive(false);
        DollDesignVisual[1].SetActive(false);
        DollDesignVisual[2].SetActive(false);
        DollDesignVisual[3].SetActive(false);
        DollDesignVisual[4].SetActive(false);
        DollDesignVisual[5].SetActive(false);
        Box.enabled = true;
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        
    }

    public void deleteData(GameData data)
    {
       
    }
}
