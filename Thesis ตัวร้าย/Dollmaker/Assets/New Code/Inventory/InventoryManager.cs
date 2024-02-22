using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventorySlote[] inventoryslote;
    public GameObject inventoryItemPrefab;
    public PlayerPickUpItem playerPickUpItem;

    [SerializeField] int SelectedSlot;
   public int selectedSlot { get { return SelectedSlot; } set { SelectedSlot = value; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
    }
    private void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 4)
            {
                ChangeSelectedSlot(number - 1);
            }
        }

        inventoryItem itemSlot = inventoryslote[selectedSlot].GetComponentInChildren<inventoryItem>();
        if (itemSlot != null && itemSlot.gameObject.CompareTag("Scissors"))
        {
            playerPickUpItem.HaveScissor = true;
        }
        else
        {
            playerPickUpItem.HaveScissor = false;
        }
    }

    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventoryslote[selectedSlot].Deselect();
        }

        inventoryslote[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        
        for(int i = 0; i < inventoryslote.Length; i++)
        {
            InventorySlote slot = inventoryslote[i];
            inventoryItem itemSlot = slot.GetComponentInChildren<inventoryItem>();
            if(itemSlot == null)
            {
                SpawnnewItem(item, slot);
                return true;
            }       
        }
        return false;
    }

    void SpawnnewItem(Item item, InventorySlote slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        inventoryItem InventoryItem = newItemGo.GetComponent<inventoryItem>();
        InventoryItem.InitialiseItem(item);
        newItemGo.tag = item.type.ToString();
    }

    public Item GetSelectedItem(bool use)
    {

        InventorySlote slot = inventoryslote[selectedSlot];
        inventoryItem itemSlot = slot.GetComponentInChildren<inventoryItem>();
        if (itemSlot != null)
        {
            Item item = itemSlot.item;
            if (use)
            {
                itemSlot.Count--;
                if(itemSlot.Count <= 0)
                {
                    
                    Destroy(itemSlot.gameObject);
                }
            }
            return item;
        }

        return null;
    }
}
