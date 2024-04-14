using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class FinishBasket : MonoBehaviour
{


    // Start is called before the first frame update
    public GameObject[] dollShow;
    public GameObject[] Slot;


    [SerializeField] int finishDollneed;
    public int NeedFinishDoll {  get { return finishDollneed; } set { finishDollneed = value; } }

    [SerializeField] int dollId;
    public int DollID { get { return dollId; } set { dollId = value; } }

     public int SlotNum;

    public UnityEvent NeedDollSucc, DeedDollSucc2;
    bool SuccDoll, succDoll2;

    public bool SuccDoll2 { get { return succDoll2; } set {  succDoll2 = value; } }
    public void Spawndoll()
    {

        if (DollID >= 0 && DollID < dollShow.Length) 
        {
            GameObject newDoll = Instantiate(dollShow[DollID], Slot[SlotNum].transform);

            SlotNum++;
        }
    }

    public void DestoryDoll()
    {
        foreach (GameObject slot in Slot)
        {
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }

        
        SlotNum = 0;
    }

    public void Update()
    {
        if(finishDollneed <= SlotNum)
        {
            if(!SuccDoll)
            {
                NeedDollSucc.Invoke();
                SuccDoll = true;
            }

            if(SuccDoll2)
            {
                DeedDollSucc2.Invoke();
                SuccDoll2 = false;
            }

        }
    }


}
