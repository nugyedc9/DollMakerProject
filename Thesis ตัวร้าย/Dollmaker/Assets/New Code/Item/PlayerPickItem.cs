using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickItem : MonoBehaviour
{
    #region Didn,t use MAYBE?
    /*[SerializeField] SelectItem ItemSlot;
    public Transform PickUPPoint, Slot1, Slot2, Slot3;
    public float PickUPRate;
    private float ItemCount;

    void Update()
    {
        
        Ray PickUpRay = new Ray(PickUPPoint.position, PickUPPoint.forward);
        Debug.DrawRay(PickUPPoint.position, PickUPPoint.forward);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(PickUpRay, out RaycastHit hitInfo, PickUPRate))
            {
                Debug.Log(hitInfo.collider.gameObject.name);
                if (ItemCount != 3)
                {


                    if (hitInfo.collider.gameObject.tag == "Cross")
                    {
                        ItemCount++;
                        hitInfo.collider.gameObject.GetComponent<Collider>().isTrigger = true;
                        hitInfo.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        if (ItemCount == 1)
                        {
                            ItemSlot.Slot1();
                            hitInfo.collider.gameObject.transform.SetParent(Slot1);
                        }
                        else if (ItemCount == 2)
                        {
                            ItemSlot.Slot2();
                            hitInfo.collider.gameObject.transform.SetParent(Slot2);
                        }
                        else if (ItemCount == 3)
                        {
                            ItemSlot.Slot3();
                            hitInfo.collider.gameObject.transform.SetParent(Slot3);
                        }
                        hitInfo.collider.gameObject.transform.localPosition = Vector3.zero;
                        hitInfo.collider.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);                        
                    }


                    if (hitInfo.collider.gameObject.tag == "Doll")
                    {
                        ItemCount++;
                        hitInfo.collider.gameObject.GetComponent<Collider>().isTrigger = true;
                        hitInfo.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        if (ItemCount == 1)
                        {
                            ItemSlot.Slot1();
                            hitInfo.collider.gameObject.transform.SetParent(Slot1);
                        }
                        else if (ItemCount == 2)
                        {
                            ItemSlot.Slot2();
                            hitInfo.collider.gameObject.transform.SetParent(Slot2);
                        }
                        else if (ItemCount == 3)
                        {
                            ItemSlot.Slot3();
                            hitInfo.collider.gameObject.transform.SetParent(Slot3);
                        }
                        hitInfo.collider.gameObject.transform.localPosition = Vector3.zero;
                        hitInfo.collider.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

                    }


                    if (hitInfo.collider.gameObject.tag == "cloth")
                    {
                        ItemCount++;
                        hitInfo.collider.gameObject.GetComponent<Collider>().isTrigger = true;
                        hitInfo.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        if (ItemCount == 1)
                        {
                            ItemSlot.Slot1();
                            hitInfo.collider.gameObject.transform.SetParent(Slot1);
                        }
                        else if (ItemCount == 2)
                        {
                            ItemSlot.Slot2();
                            hitInfo.collider.gameObject.transform.SetParent(Slot2);
                        }
                        else if (ItemCount == 3)
                        {
                            ItemSlot.Slot3();
                            hitInfo.collider.gameObject.transform.SetParent(Slot3);
                        }
                        hitInfo.collider.gameObject.transform.localPosition = Vector3.zero;
                        hitInfo.collider.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    }
                }
            }
        }
       

    }
*/
 #endregion


}
