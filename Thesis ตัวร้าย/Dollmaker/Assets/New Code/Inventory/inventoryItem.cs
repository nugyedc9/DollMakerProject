using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    [Header("Image")]
    public Image image;

    private RollClothColor RollCloth;

    [HideInInspector] public Item item;
    [HideInInspector] public int Count = 1;
    [HideInInspector] public Transform parentAfterDrag, ChangePos;
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
        //lastMousePosition = eventData.position;
        parentAfterDrag = transform.parent;
        ChangePos = transform.parent;
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    { 
        transform.position = Input.mousePosition;
        /*  Vector2 curremtMousePosition = eventData.position;
          Vector2 diff = curremtMousePosition - lastMousePosition;
          lastMousePosition = curremtMousePosition;*/

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = orginalPosition;
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

    }

    public void InitialiseItem(Item newItem)
     {
         item = newItem;
         image.sprite = newItem.image;
     }

    public void OnDisable()
    {
        transform.localPosition = orginalPosition;
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }

    /*    public void OnBeginDrag(PointerEventData eventData)
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
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
        }*/

}
