using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemPickUp;

    [Header("CrossThing")]
    private CrossCheck CrossUse;
    [SerializeField] float CurHpCross;
    public float curHpCross { get { return CurHpCross;  } set { CurHpCross = value; } }

    [Header("Pick Up")]
    public float Pickrange;
    public float DropSpeed;
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
                    curHpCross = CrossUse.curHp;
                    inventoryManager.TriggerCrossAnim = true;
                    inventoryManager.AddItem(itemPickUp[0]);
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
