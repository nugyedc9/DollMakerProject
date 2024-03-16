using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollClothColor : MonoBehaviour
{
    public int pieceClothID;
    public bool ClothRooll;
    public GameObject ItemPrefab;
    public GameObject SpawnPoint;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprite;
    private bool opencloth;
    public bool OpecCloth { get { return opencloth; } set { opencloth = value; } }
    private float delayCloseCloth;
    public float DelayCloseCloth { get { return delayCloseCloth; } set {  delayCloseCloth = value; } }
    public int ClothCount;


    public void DropitemPrefabs()
    {
        var DropObj = Instantiate(ItemPrefab, SpawnPoint.transform.position, Quaternion.identity) as GameObject;
        DropObj.GetComponent<Rigidbody>().velocity = (SpawnPoint.transform.position).normalized ;
    }

    private void Update()
    {
        if (ClothRooll)
        {
            if (OpecCloth)
            {
                selectThis();
            }
            else noneSelect();

            if (ClothCount == 0)
            {
                spriteRenderer.sprite = sprite[2];
            }
        }


        if(DelayCloseCloth > 0)
        {
            DelayCloseCloth -= Time.deltaTime;  
            
        }
        else if(DelayCloseCloth < 0)
        {
            OpecCloth = false;
        }


    }

    public void selectThis()
    {
        spriteRenderer.sprite = sprite[1];
    }

    public void noneSelect()
    {
        spriteRenderer.sprite = sprite[0];
    }

}
