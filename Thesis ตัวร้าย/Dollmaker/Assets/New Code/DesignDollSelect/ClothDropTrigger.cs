using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothDropTrigger : MonoBehaviour
{

    public DesignSelect designSelect;
    public InventoryManager inventoryManager;
    public GameObject cloth;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cloth")
        {
            designSelect.HaveCloth = true;
            cloth.SetActive(true);
            inventoryManager.GetSelectedItem(true);
        }
    }
}
