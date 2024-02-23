using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [SerializeField] Item[] ItemPickUp;
    public Item[] itemPickUp { get { return ItemPickUp; } set { ItemPickUp = value; } }

    [Header("CrossThing")]
    public PlayerAttack PAttack;
    private CrossCheck CrossUse;

    [Header("Pick Up")]
    public float Pickrange;
    public Camera FpsCam;
    public Transform pickUPPoint;

    [SerializeField] bool haveScissor;
    public bool HaveScissor { get {  return haveScissor; } set { haveScissor = value; } }

    public void Update()
    {
        Ray ray = new Ray(pickUPPoint.position, pickUPPoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Pickrange))
        {
           // Debug.Log(hitInfo.collider.gameObject.tag);
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (hitInfo.collider.gameObject.tag == "Cross")
                {
                    CrossUse = hitInfo.collider.gameObject.GetComponent<CrossCheck>();
                    PAttack.curHpCross = CrossUse.curHp;
                    inventoryManager.TriggerCrossAnim = true;
                    if(CrossUse.curHp == 3)
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
                if(hitInfo.collider.gameObject.tag == "Scissors")
                {
                    inventoryManager.AddItem(itemPickUp[2]);
                    Destroy(hitInfo.collider.gameObject);
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (hitInfo.collider.gameObject.tag == "RollCloth")
                {
                    if (HaveScissor)
                    {
                        inventoryManager.AddItem(itemPickUp[3]);
                    }
                }
            }
        }

    }


}
