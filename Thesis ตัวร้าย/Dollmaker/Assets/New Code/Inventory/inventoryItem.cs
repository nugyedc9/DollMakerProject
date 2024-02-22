using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    [Header("Image")]
    public Image image;

    [HideInInspector] public Item item;
    [HideInInspector] public int Count = 1;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public InventoryManager inventoryManager;
    private Vector3 orginalPosition;
    private Vector2 lastMousePosition;
    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("Failed to find InventoryManager in the scene.");
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    
    }

    public void OnDrag(PointerEventData eventData)
    {
            Vector2 curremtMousePosition = eventData.position;
            Vector2 diff = curremtMousePosition - lastMousePosition;

            transform.position = Input.mousePosition;

            lastMousePosition = curremtMousePosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            transform.localPosition = orginalPosition;
    }

     public void InitialiseItem(Item newItem)
     {
         item = newItem;
         image.sprite = newItem.image;
     }

    /*
     public void OnBeginDrag(PointerEventData eventData)
     {
         image.raycastTarget = false;
         parentAfterDrag = transform.parent;
         transform.SetParent(transform.root);
         transform.SetAsLastSibling();
     }

     public void OnDrag(PointerEventData eventData)
     {
         transform.position = Input.mousePosition;
     }

     public void OnEndDrag(PointerEventData eventData)
     {
         image.raycastTarget=true;
         transform.SetParent(parentAfterDrag);
     }*/

}
