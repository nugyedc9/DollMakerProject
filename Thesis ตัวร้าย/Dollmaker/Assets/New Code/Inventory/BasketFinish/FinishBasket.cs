using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void Spawndoll()
    {
        GameObject newDoll = Instantiate(dollShow[DollID], Slot[SlotNum].transform);
        SlotNum++;
    }

}
