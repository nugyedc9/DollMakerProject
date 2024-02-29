using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public TabTutorial BookGuide;
    [SerializeField] Item[] ItemPickUp;
    public Item[] itemPickUp { get { return ItemPickUp; } set { ItemPickUp = value; } }

    [Header("Cross Thing")]
    public PlayerAttack PAttack;
    public GameObject GhostActive;
    private CrossCheck CrossUse;

    [Header("Scrissor Thing")]
    public Animator ScrissorAnim;
    public GameObject _5Story;

    [Header("Pick Up")]
    public float Pickrange;
    public Camera FpsCam;
    public Transform pickUPPoint;

    [SerializeField] bool haveScissor;
    public bool HaveScissor { get {  return haveScissor; } set { haveScissor = value; } }

    [SerializeField] bool key;
    public bool Key { get { return key; } set { key = value; } }

    [SerializeField] bool finishDollOnHand;
    public bool FDOnhand { get { return finishDollOnHand; } set { finishDollOnHand = value; } }

    [SerializeField] bool finishDollOnHand1;
    public bool FDOnhand1 { get { return finishDollOnHand1; } set { finishDollOnHand1 = value; } }

    [SerializeField] bool finishDollOnHand2;
    public bool FDOnhand2 { get { return finishDollOnHand2; } set { finishDollOnHand2 = value; } }

    [SerializeField] int itemCount;
    public int ItemCount { get { return itemCount; } set {  itemCount = value; } }

    private RollClothColor pieceClothGet;
    private DocumentID documentID;
    private Door DoorId;
    private FinishBasket dropFinish;
    private StoryActive storyActive;

    private bool GhostComeOut;
    private int KeyId;

    public void Update()
    {
        Ray ray = new Ray(pickUPPoint.position, pickUPPoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Pickrange))
        {
           // Debug.Log(hitInfo.collider.gameObject.tag);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ItemCount < inventoryManager.inventoryslote.Length)
                {
                    if (hitInfo.collider.gameObject.tag == "Cross")
                    {
                        CrossUse = hitInfo.collider.gameObject.GetComponent<CrossCheck>();
                        PAttack.curHpCross = CrossUse.curHp;
                        inventoryManager.TriggerCrossAnim = true;
                        if (CrossUse.curHp == 3)
                            inventoryManager.AddItem(itemPickUp[0]);
                        if (CrossUse.curHp == 2)
                            inventoryManager.AddItem(itemPickUp[4]);
                        if (CrossUse.curHp == 1)
                            inventoryManager.AddItem(itemPickUp[5]);

                        if (!GhostComeOut)
                        {
                            GhostActive.SetActive(true);
                            GhostComeOut = true;
                        }

                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "Doll")
                    {
                        inventoryManager.AddItem(itemPickUp[1]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "Scissors")
                    {
                        inventoryManager.AddItem(itemPickUp[2]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if(hitInfo.collider.gameObject.tag == "Cloth")
                    {
                        inventoryManager.AddItem(itemPickUp[3]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if(hitInfo.collider.gameObject.tag == "Key")
                    {
                        pieceClothGet = hitInfo.collider.gameObject.GetComponent<RollClothColor>();
                        KeyId = pieceClothGet.pieceClothID;

                        if (!GhostComeOut)
                        {
                            GhostActive.SetActive(true);
                            GhostComeOut = true;
                        }

                        inventoryManager.AddItem(itemPickUp[10]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                }
                
                if(hitInfo.collider.gameObject.tag == "Document")
                {
                    documentID = hitInfo.collider.gameObject.GetComponent<DocumentID>();
                    if(documentID.DocID == 0)
                    {
                        _5Story.SetActive(true);
                        BookGuide.PageCount++;
                        Destroy(hitInfo.collider.gameObject);
                    }
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (ItemCount < inventoryManager.inventoryslote.Length)
                {
                    if (hitInfo.collider.gameObject.tag == "RollCloth")
                    {
                        if (HaveScissor)
                        {
                            if (!ScrissorAnim.GetCurrentAnimatorStateInfo(0).IsName("cutanima"))
                                ScrissorAnim.Play("cutanima", 0, 0);

                            pieceClothGet = hitInfo.collider.gameObject.GetComponent<RollClothColor>();
                            if (pieceClothGet.pieceClothID == 0)
                            {
                                inventoryManager.AddItem(itemPickUp[6]);
                            }
                            else if (pieceClothGet.pieceClothID == 1)
                            {
                                inventoryManager.AddItem(itemPickUp[7]);
                            }
                            else if (pieceClothGet.pieceClothID == 2)
                            {
                                inventoryManager.AddItem(itemPickUp[8]);
                            }
                            else if (pieceClothGet.pieceClothID == 3)
                            {
                                inventoryManager.AddItem(itemPickUp[9]);
                            }
                        }
                    }

                    if (hitInfo.collider.gameObject.tag == "Door")
                    {
                        if (Key)
                        {
                            DoorId = hitInfo.collider.gameObject.GetComponent<Door>();
                           if(KeyId == DoorId.DoorID)
                            {
                                DoorId.Lock = false;
                                inventoryManager.GetSelectedItem(true);
                            }
                        }                     
                    }

                    if(hitInfo.collider.gameObject.tag == "Basket")
                    {
                        if (FDOnhand || FDOnhand1 || FDOnhand2)
                        {
                            dropFinish = hitInfo.collider.gameObject.GetComponent<FinishBasket>();
                            dropFinish.DollID = inventoryManager.FinishDollID;
                            dropFinish.Spawndoll();
                            inventoryManager.GetSelectedItem(true);
                        }
                        
                    }

                }
                
            }

            if(hitInfo.collider.gameObject.tag == "Event")
            {
                storyActive = hitInfo.collider.gameObject.GetComponent<StoryActive>();
                storyActive.LookActiveevent();
            }
        }

    }


}
