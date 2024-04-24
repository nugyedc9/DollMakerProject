using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class inventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [Header("Image")]
    public Image image;
    public TextMeshProUGUI CountText;

    private RollClothColor RollCloth;
    public Animator anim;
    public bool NotItemInInv, Scissor;

    [HideInInspector] public Item item;
    [HideInInspector] public int Count = -1;
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
        GenerateGuid();

       

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //lastMousePosition = eventData.position;
        if (Count > 0)
        {
            parentAfterDrag = transform.parent;
            ChangePos = transform.parent;
            image.raycastTarget = false;
        }

        if(Scissor)
        {
            anim.Play("AnimCut");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Count > 0)
        
            transform.position = Input.mousePosition;
        /*  Vector2 curremtMousePosition = eventData.position;
          Vector2 diff = curremtMousePosition - lastMousePosition;
          lastMousePosition = curremtMousePosition;*/

    }

    public void OnEndDrag(PointerEventData eventData)
    {
     
            if (!NotItemInInv)
            {
                transform.localPosition = orginalPosition;
                image.raycastTarget = true;
                transform.SetParent(parentAfterDrag);
            }
            if (NotItemInInv)
            {
                transform.localPosition = orginalPosition;
                image.raycastTarget = true;
            }
        

        if (Scissor)
        {
            anim.Play("ScissorAnim");
        }

    }

    public void InitialiseItem(Item newItem)
     {
         item = newItem;
         image.sprite = newItem.image;
        if(!item.stackable) CountText.gameObject.SetActive(false);
        RefreshCount();

     }

    public void RefreshCount()
    {
        CountText.text = Count.ToString();
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
