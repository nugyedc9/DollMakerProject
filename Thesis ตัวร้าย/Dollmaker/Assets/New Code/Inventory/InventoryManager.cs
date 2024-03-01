using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

public PlayerPickUpItem playerPickUpItem;
    public PlayerAttack pAttack;
    public TabTutorial tabTutorial;
    public PlayerChangeCam ChangeCam;
    public Camera Cam;

    public InventorySlote[] inventoryslote;
    public GameObject[] ItemOnHand;
    public GameObject[] ItemPrefab;
    public float DropSpeed;
    public Transform DropPoint;
    public GameObject inventoryItemPrefab;



    [Header("CrossAction")]
    public Animator CorssAni;
    [SerializeField] bool triggerCrossAnim;

    public Animator InvOpenAnim;
    public bool TriggerCrossAnim { get { return triggerCrossAnim; } set { triggerCrossAnim = value; } }

    [SerializeField] int SelectedSlot;
   public int selectedSlot { get { return SelectedSlot; } set { SelectedSlot = value; } }

    [SerializeField] int finishDollID;
    public int FinishDollID { get { return finishDollID; } set { finishDollID = value; } }

    private Vector3 DesDrop;
    bool drop;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        InvOpenAnim.enabled = false;
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


        #region Item Show
        if (selectedSlot >= 0 && selectedSlot <= 2 && tabTutorial.OpenTutor == false && ChangeCam.camOnPerSon == true)
        {
            inventoryItem itemSlot = inventoryslote[selectedSlot].GetComponentInChildren<inventoryItem>();
            if (itemSlot != null && itemSlot.gameObject.CompareTag("Cross"))
            {
                pAttack.Attack = true;
                ItemOnHand[0].SetActive(true);
                if (triggerCrossAnim)
                {
                    if (playerPickUpItem.PAttack.curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (playerPickUpItem.PAttack.curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (playerPickUpItem.PAttack.curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    triggerCrossAnim = false;
                }
                if (drop)
                {
                    if (playerPickUpItem.PAttack.curHpCross == 3) DropitemPrefabs(DropPoint, 0);
                    if (playerPickUpItem.PAttack.curHpCross == 2) DropitemPrefabs(DropPoint, 4);
                    if (playerPickUpItem.PAttack.curHpCross == 1) DropitemPrefabs(DropPoint, 5);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                pAttack.Attack = false;
                ItemOnHand[0].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("Doll"))
            {
                ItemOnHand[1].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPoint, 1);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                ItemOnHand[1].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("Scissors"))
            {
                playerPickUpItem.HaveScissor = true;
                ItemOnHand[2].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPoint, 2);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                playerPickUpItem.HaveScissor = false;
                ItemOnHand[2].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("Cloth"))
            {
                ItemOnHand[3].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPoint, 3);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                ItemOnHand[3].SetActive(false);
            }


            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishDollRedBlue"))
            {
                FinishDollID = 0; playerPickUpItem.FDOnhand = true;
            }else playerPickUpItem.FDOnhand = false;
            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishDollGreen"))
            {
                FinishDollID = 1; playerPickUpItem.FDOnhand1 = true;
            }
            else playerPickUpItem.FDOnhand1 = false;
            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishDollYellow"))
            {
                FinishDollID = 2; playerPickUpItem.FDOnhand2 = true;
            }
            else playerPickUpItem.FDOnhand2 = false;



            if (itemSlot != null && itemSlot.gameObject.CompareTag("Key"))
            {
                ItemOnHand[4].SetActive(true);
                playerPickUpItem.Key = true;
            }
            else
            {
                ItemOnHand[4].SetActive(false);
                playerPickUpItem.Key = false;
            }

            #endregion

            #region Drop Item
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (itemSlot != null)
                    drop = true;
            }
        }
        #endregion
    }

    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventoryslote[selectedSlot].Deselect();
        }

        inventoryslote[newValue].Select();
        selectedSlot = newValue;
        inventoryItem itemSlot = inventoryslote[selectedSlot].GetComponentInChildren<inventoryItem>();
        if (itemSlot != null && itemSlot.gameObject.CompareTag("Cross"))
        {
            triggerCrossAnim = true;
        }
    }

    public bool AddItem(Item item)
    {
        
        for(int i = 0; i < inventoryslote.Length; i++)
        {
            InventorySlote slot = inventoryslote[i];
            inventoryItem itemSlot = slot.GetComponentInChildren<inventoryItem>();
            if(itemSlot == null)
            {
                playerPickUpItem.ItemCount++;
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
                    playerPickUpItem.ItemCount--;
                    Destroy(itemSlot.gameObject);
                }
            }
            return item;
        }

        return null;
    }


    public void DropitemPrefabs(Transform Droppoint , int ItemId)
    {
        Ray R = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(R, out hit)) DesDrop = hit.point;
        else DesDrop = R.GetPoint(1000);


        var DropObj = Instantiate(ItemPrefab[ItemId], DropPoint.position, Quaternion.identity) as GameObject;
        DropObj.GetComponent<Rigidbody>().velocity = (DesDrop - DropPoint.position).normalized * DropSpeed;
    }



}
