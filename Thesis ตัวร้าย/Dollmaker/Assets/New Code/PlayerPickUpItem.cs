using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public TabTutorial BookGuide;
    public PlayerHp HpPlayer;
    [SerializeField] Item[] ItemPickUp;
    public Item[] itemPickUp { get { return ItemPickUp; } set { ItemPickUp = value; } }

    [Header("Cross Thing")]
    public PlayerAttack PAttack;
    public Slider CrossBar;
    private CrossCheck crossUse;
    public CrossCheck CrossUse {get { return crossUse; } set { crossUse = value; } }

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
        KeyS, DocumentS, PushFiniDollS, UnlockDoorS,
        HealPickS, HealUseS;

    [Header("Tutorial Pick UP")]
    public GameObject CrossNote;
    public GameObject ScissorNote;
    bool _1Cross, _1Scissor;

    [SerializeField] bool haveScissor;
    public bool HaveScissor { get {  return haveScissor; } set { haveScissor = value; } }

    [SerializeField] bool key;
    public bool Key { get { return key; } set { key = value; } }

    [SerializeField] bool healOnhand;
    public bool HealOnhand { get { return healOnhand; } set { healOnhand = value; } }

    [SerializeField] bool finishDollOnHand;
    public bool FDOnhand { get { return finishDollOnHand; } set { finishDollOnHand = value; } }

    [SerializeField] bool finishDollOnHand1;
    public bool FDOnhand1 { get { return finishDollOnHand1; } set { finishDollOnHand1 = value; } }

    [SerializeField] bool finishDollOnHand2;
    public bool FDOnhand2 { get { return finishDollOnHand2; } set { finishDollOnHand2 = value; } }

    [SerializeField] int itemCount;
    public int ItemCount { get { return itemCount; } set {  itemCount = value; } }

    [SerializeField] bool onNote;
    public bool OnNote { get { return onNote; } set { onNote = value; } }

    private RollClothColor pieceClothGet;
    private DocumentID documentID;
    private Door DoorId;
    private FinishBasket dropFinish;
    private StoryActive storyActive;

    private bool GhostComeOut;
    private int keyId;
    public int KeyId { get { return keyId;} set { keyId = value; } }

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
                            OnNote = true;
                            CrossNote.SetActive(true);
                            _1Cross = true;
                        }
                        audioSource.clip = CrossS;
                        audioSource.Play();
                        CrossUse = hitInfo.collider.gameObject.GetComponent<CrossCheck>();
                        PAttack.curHpCross = CrossUse.curHp;
                        CrossBar.maxValue = crossUse.MaxHp;
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
                            OnNote = true;
                            ScissorNote.SetActive(true);
                            _1Scissor = true;
                        }

                    }
                    if (hitInfo.collider.gameObject.tag == "Cloth")
                    {
                        inventoryManager.AddItem(itemPickUp[3]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "Key")
                    {
                        audioSource.clip = KeyS;
                        audioSource.Play();
                        pieceClothGet = hitInfo.collider.gameObject.GetComponent<RollClothColor>();
                        KeyId = pieceClothGet.pieceClothID;

                        inventoryManager.AddItem(itemPickUp[10]);
                        Destroy(hitInfo.collider.gameObject);
                    }

                    if (hitInfo.collider.gameObject.tag == "EyeWash")
                    {
                        audioSource.clip = HealPickS; audioSource.Play();
                        inventoryManager.AddItem(itemPickUp[19]);
                        Destroy(hitInfo.collider.gameObject);

                    }

                    #region ClothColor
                    if (hitInfo.collider.gameObject.tag == "RedCloth")
                    {
                        inventoryManager.AddItem(itemPickUp[6]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "BlueCloth")
                    {
                        inventoryManager.AddItem(itemPickUp[7]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "GreenCloth")
                    {
                        inventoryManager.AddItem(itemPickUp[8]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "YellowCloth")
                    {
                        inventoryManager.AddItem(itemPickUp[9]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    #endregion

                    #region PieceClothColor
                    if (hitInfo.collider.gameObject.tag == "PieceClothRed")
                    {
                        inventoryManager.AddItem(itemPickUp[11]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "PieceClothBlue")
                    {
                        inventoryManager.AddItem(itemPickUp[12]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "PieceClothGreen")
                    {
                        inventoryManager.AddItem(itemPickUp[13]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "PieceClothYellow")
                    {
                        inventoryManager.AddItem(itemPickUp[14]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    #endregion

                    #region FinishClothColor
                    if (hitInfo.collider.gameObject.tag == "FinishClothRed")
                    {
                        inventoryManager.AddItem(itemPickUp[15]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "FinishClothBlue")
                    {
                        inventoryManager.AddItem(itemPickUp[16]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "FinishClothGreen")
                    {
                        inventoryManager.AddItem(itemPickUp[17]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    if (hitInfo.collider.gameObject.tag == "FinishClothYellow")
                    {
                        inventoryManager.AddItem(itemPickUp[18]);
                        Destroy(hitInfo.collider.gameObject);
                    }
                    #endregion
                }

                if (hitInfo.collider.gameObject.tag == "Document")
                {
                    audioSource.clip = DocumentS;
                    audioSource.Play();
                    documentID = hitInfo.collider.gameObject.GetComponent<DocumentID>();
                    if (documentID.DocID == 0)
                    {
                        _5Story.SetActive(true);
                        BookGuide.PageCount++;
                        Destroy(hitInfo.collider.gameObject);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (ItemCount < inventoryManager.inventoryslote.Length)
                {
                    if (hitInfo.collider.gameObject.tag == "RollCloth")
                    {
                        if (HaveScissor && !BookGuide.OpenTutor)
                        {
                            audioSource.clip = CutClothS;
                            audioSource.Play();
                            if (!ScrissorAnim.GetCurrentAnimatorStateInfo(0).IsName("cutanima"))
                                ScrissorAnim.Play("cutanima", 0, 0);

                            pieceClothGet = hitInfo.collider.gameObject.GetComponent<RollClothColor>();
                            pieceClothGet.DropitemPrefabs();
                            /*   if (pieceClothGet.pieceClothID == 0)
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
                               }*/
                        }
                    }

                    if (hitInfo.collider.gameObject.tag == "Door")
                    {
                        if (Key)
                        {
                            DoorId = hitInfo.collider.gameObject.GetComponent<Door>();
                            if (KeyId == DoorId.DoorID)
                            {
                                audioSource.clip = UnlockDoorS; audioSource.Play();
                                DoorId.Lock = false;
                                inventoryManager.GetSelectedItem(true);
                            }
                        }
                    }

                    if (hitInfo.collider.gameObject.tag == "Basket")
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

            if (hitInfo.collider.gameObject.tag == "Event")
            {
                storyActive = hitInfo.collider.gameObject.GetComponent<StoryActive>();
                storyActive.LookActiveevent();
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (HealOnhand && HpPlayer.curHp < HpPlayer.MaxHp)
            {
                audioSource.clip = HealUseS; audioSource.Play();
                HpPlayer.Heal();
                inventoryManager.GetSelectedItem(true);
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
