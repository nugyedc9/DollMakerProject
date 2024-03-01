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
    private CrossCheck CrossUse;

    [Header("Scrissor Thing")]
    public Animator ScrissorAnim;
    public GameObject _5Story;

    [Header("Pick Up")]
    public float Pickrange;
    public Camera FpsCam;
    public Transform pickUPPoint;

    [Header("---- Audio ----")]
    public AudioSource audioSource;
    public AudioClip CrossS, DollS, ScissorS, CutClothS,
        KeyS, DocumentS, PushFiniDollS;

    [Header("Tutorial Pick UP")]
    public GameObject CrossNote;
    public GameObject ScissorNote;
    bool _1Cross, _1Scissor;

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
                        if (!_1Cross)
                        {
                            ShowMouse();
                            CrossNote.SetActive(true);
                            _1Cross = true;
                        }
                        audioSource.clip = CrossS;
                        audioSource.Play();
                        CrossUse = hitInfo.collider.gameObject.GetComponent<CrossCheck>();
                        PAttack.curHpCross = CrossUse.curHp;
                        inventoryManager.TriggerCrossAnim = true;
                        if (CrossUse.curHp == 3)
                            inventoryManager.AddItem(itemPickUp[0]);
                        if (CrossUse.curHp == 2)
                            inventoryManager.AddItem(itemPickUp[4]);
                        if (CrossUse.curHp == 1)
                            inventoryManager.AddItem(itemPickUp[5]);


                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "Doll")
                    {
                        audioSource.clip = DollS;
                        audioSource.Play();
                        inventoryManager.AddItem(itemPickUp[1]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "Scissors")
                    {
                        audioSource.clip = ScissorS;
                        audioSource.Play();
                        inventoryManager.AddItem(itemPickUp[2]);
                        Destroy(hitInfo.collider.gameObject);
                        if (!_1Scissor)
                        {
                            ShowMouse();
                            ScissorNote.SetActive(true);
                            _1Scissor = true;
                        }

                    }
                    if(hitInfo.collider.gameObject.tag == "Cloth")
                    {
                        inventoryManager.AddItem(itemPickUp[3]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if(hitInfo.collider.gameObject.tag == "Key")
                    {
                        audioSource.clip = KeyS;
                        audioSource.Play();
                        pieceClothGet = hitInfo.collider.gameObject.GetComponent<RollClothColor>();
                        KeyId = pieceClothGet.pieceClothID;

                        inventoryManager.AddItem(itemPickUp[10]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                }
                
                if(hitInfo.collider.gameObject.tag == "Document")
                {
                    audioSource.clip = DocumentS;
                    audioSource.Play();
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
                            audioSource.clip = CutClothS;
                            audioSource.Play();
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
                            audioSource.clip = PushFiniDollS;
                            audioSource.Play();
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


    public void ShowMouse()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMouse()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


}
