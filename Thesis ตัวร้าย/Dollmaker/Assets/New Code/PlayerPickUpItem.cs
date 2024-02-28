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

    [SerializeField] bool haveScissor;
    public bool HaveScissor { get {  return haveScissor; } set { haveScissor = value; } }

    [SerializeField] int itemCount;
    public int ItemCount { get { return itemCount; } set {  itemCount = value; } }

    private RollClothColor pieceClothGet;
    private DocumentID documentID;

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
                            if(pieceClothGet.pieceClothID == 0)
                            {
                                inventoryManager.AddItem(itemPickUp[6]);
                            }
                            else if(pieceClothGet.pieceClothID == 1)
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
                }

                
            }
        }

    }


}
