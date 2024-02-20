using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothColorDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public CanPlayMini1 canplay;
    public PlayerAttack PAttack;
    public GameObject[] ClothColor;


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
        if (collision.gameObject.tag == "RedCloth")
        {
            ClothColor[0].SetActive(true);
            canplay.Cloth = true;
            PAttack.DropDesignColor();
        }
        if (collision.gameObject.tag == "BlueCloth")
        {
            ClothColor[1].SetActive(true);
            canplay.Cloth = true;
            PAttack.DropDesignColor();
        }
        if (collision.gameObject.tag == "GreenCloth")
        {
            ClothColor[2].SetActive(true);
            canplay.Cloth = true;
            PAttack.DropDesignColor();
        }
        if (collision.gameObject.tag == "YellowCloth")
        {
            ClothColor[3].SetActive(true);
            canplay.Cloth = true;
            PAttack.DropDesignColor();
        }
    }

    public void Finishmakecloth()
    {
        IsRed = false;
        IsBlue = false;
        IsGreen = false;
        IsYellow = false;
    }
}
