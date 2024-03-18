using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollClothCut : MonoBehaviour, IDropHandler
{
    public DesignSelect BookCheck;

    public int ClothId;
    public GameObject pieceCloth, HitBoxDrop;

    public SpriteRenderer ClothSpriteRen;
    public Sprite[] SpriteChange;

    bool SciIn;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Scissors"))
        {
            SciIn = true;
            ClothSpriteRen.sprite = SpriteChange[1];
           // print("Enter");
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Scissors"))
        {
            SciIn = false;
            ClothSpriteRen.sprite = SpriteChange[0];
           // print("Exit");
        }
    }



    public void OnDrop(PointerEventData eventData)
    {
        if (SciIn)
        {
            pieceCloth.SetActive(true);
            HitBoxDrop.SetActive(false);
            BookCheck.ClothColorID = ClothId;
            BookCheck.HaveCloth = true;
          //  print("Drop");
        }
    }
}
