using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlote : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    public Image image;
    public Color selectedColor, unSelectedColor;
    public int slotNumber;
    [HideInInspector] public InventoryManager inventoryManager;
    bool MouseDown;
    private void Awake()
    {
        Deselect();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Update()
    {
         if(Input.GetMouseButtonUp(0))
        {
            MouseDown = false; 
        }
         if(Input.GetMouseButtonDown(0))
        {
            MouseDown = true;
        }
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


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!MouseDown)
        {
            if(slotNumber >= 0 && slotNumber <= 2)
            inventoryManager.ChangeSelectedSlot(slotNumber);
            else if(slotNumber >= 3 && slotNumber <= 5)
                inventoryManager.ChangeSelectedKeySlot(slotNumber);
        }
    }
}
