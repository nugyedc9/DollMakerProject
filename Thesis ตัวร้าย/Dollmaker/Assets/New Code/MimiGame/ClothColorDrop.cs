using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ClothColorDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public CanPlayMini1 canplay;
    public GameObject[] ClothColor;
    public GameObject hand;
    public InventoryManager inventoryManager;
    public PlayerPickUpItem playerPickup;
    public PlayerChangeCam PCam;

    [Header("Tutorial Arrow")]
    public GameObject ClothTutorail;

    [SerializeField] int finishClothID;
    public int FinishClothID { get { return finishClothID; } set { finishClothID = value; } }

    [SerializeField] bool isRed;
    public bool IsRed { get { return isRed; } set { isRed = value; } }

    [SerializeField] bool isBlue;
    public bool IsBlue { get { return isBlue; } set { isBlue = value; } }

    [SerializeField] bool isGreen;
    public bool IsGreen { get { return isGreen; } set { isGreen = value; } }

    [SerializeField] bool isYellow;
    public bool IsYellow { get { return isYellow; } set { isYellow = value; } }




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsRed) { ClothColor[0].SetActive(false); }
        if (!IsBlue) { ClothColor[1].SetActive(false); }
        if (!IsGreen) { ClothColor[2].SetActive(false); }
        if (!IsYellow) { ClothColor[3].SetActive(false); }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PieceClothRed")
        {
            PCam._1Sewing = true;
            ClothTutorail.SetActive(false);
            ClothColor[0].SetActive(true);
            finishClothID = 0;
            hand.SetActive(true);
            canplay.Cloth = true;
            inventoryManager.GetSelectedItem(true);
        }
        if (collision.gameObject.tag == "PieceClothBlue")
        {
            PCam._1Sewing = true;
            ClothTutorail.SetActive(false);
            ClothColor[1].SetActive(true);
            finishClothID = 1;
            hand.SetActive(true);
            canplay.Cloth = true;
            inventoryManager.GetSelectedItem(true);
        }
        if (collision.gameObject.tag == "PieceClothGreen")
        {
            PCam._1Sewing = true;
            ClothTutorail.SetActive(false);
            ClothColor[2].SetActive(true);
            finishClothID = 2;
            hand.SetActive(true);
            canplay.Cloth = true;
            inventoryManager.GetSelectedItem(true);
        }
        if (collision.gameObject.tag == "PieceClothYellow")
        {
            PCam._1Sewing = true;
            ClothTutorail.SetActive(false);
            ClothColor[3].SetActive(true);
            finishClothID = 3;
            hand.SetActive(true);
            canplay.Cloth = true;
            inventoryManager.GetSelectedItem(true);
        }
    }

    public void Finishmakecloth()
    {
        IsRed = false;
        IsBlue = false;
        IsGreen = false;
        IsYellow = false;
        hand.SetActive(false);
    }
}
