using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothDropTrigger : MonoBehaviour
{

    public DesignSelect designSelect;
    public InventoryManager inventoryManager;
    public PlayerPickUpItem playpickUp;
    public GameObject cloth;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "RedCloth")
        {
            designSelect.ClothColorID = 0;
            designSelect.HaveCloth = true;
            cloth.SetActive(true);
            inventoryManager.GetSelectedItem(true);
        }
        if (collision.gameObject.tag == "BlueCloth")
        {
            designSelect.ClothColorID = 1;
            designSelect.HaveCloth = true;
            cloth.SetActive(true);
            inventoryManager.GetSelectedItem(true);
        }
        if (collision.gameObject.tag == "GreenCloth")
        {
            designSelect.ClothColorID = 2;
            designSelect.HaveCloth = true;
            cloth.SetActive(true);
            inventoryManager.GetSelectedItem(true);
        }
        if (collision.gameObject.tag == "YellowCloth")
        {
            designSelect.ClothColorID = 3;
            designSelect.HaveCloth = true;
            cloth.SetActive(true);
            inventoryManager.GetSelectedItem(true);
        }
    }
}
