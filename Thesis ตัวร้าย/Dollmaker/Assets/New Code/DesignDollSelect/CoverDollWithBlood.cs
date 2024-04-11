using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static InventoryManager;

public class CoverDollWithBlood : MonoBehaviour
{
    public GameObject[] dollShow;
  //  public GameObject[] Slot;

    public Transform SpawnPoint;


/*    [SerializeField] int finishDollneed;
    public int NeedFinishDoll { get { return finishDollneed; } set { finishDollneed = value; } }*/

  

    public int SlotNum;

    float TimerDelay;
    public List<float> dollTimers = new List<float>();
    public List<int > DollId = new List<int>();

    bool SuccDoll;
    public void Spawndoll(int iddoll)
    {

        dollTimers.Add(5);

        DollId.Add(iddoll);
    }


    public void Update()
    {

        for (int i = 0; i < dollTimers.Count; i++)
        {
            if (dollTimers[i] > 0)
            {
                dollTimers[i] -= Time.deltaTime;
            }
            else if (dollTimers[i] < 0)
            {
               // GameObject newDoll = Instantiate(dollShow[DollId[i]], SpawnPoint);
                var DropObj = Instantiate(dollShow[DollId[i]], SpawnPoint.position, Quaternion.identity) as GameObject;

                dollTimers[i] = 0; 
            }
        }
    }



}
