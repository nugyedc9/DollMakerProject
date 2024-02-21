using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemPickUp;

    [Header("Pick Up")]
    public float Pickrange;
    public float DropSpeed;
    public Camera FpsCam;
    public Transform pickUPPoint;

    public void Update()
    {
        Ray ray = new Ray(pickUPPoint.position, pickUPPoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Pickrange))
        {
            Debug.Log(hitInfo.collider.gameObject.tag);
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (hitInfo.collider.gameObject.tag == "Doll")
                {
                    inventoryManager.AddItem(itemPickUp[0]);
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }

    }

    public void GetSelectItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if(receivedItem != null)
        {

        }
        else
        {

        }
    }
    
    public void UseSelectItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if(receivedItem != null)
        {

        }
        else
        {

        }
    }

}
