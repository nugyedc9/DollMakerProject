using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlote : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, unSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = unSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            inventoryItem IventoryItem = eventData.pointerDrag.GetComponent<inventoryItem>();
            IventoryItem.parentAfterDrag = transform;
        }
    }


}
