using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static InventoryManager;

public class CoverDollWithBlood : MonoBehaviour,IDataGame
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

    bool SuccDoll, Covered2, Covered3;
    int dollCovered;

    public UnityEvent Covered2Event, Covered3Event;

    public void Spawndoll(int iddoll)
    {

        dollTimers.Add(5);

        DollId.Add(iddoll);

        dollCovered++;
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

        if(!Covered2 && dollCovered >= 4)
        {
            Covered2Event.Invoke();
            Covered2 = true;
        }
      /*  if (!Covered3 && dollCovered >= 3)
        {
            Covered3Event.Invoke();
            Covered3 = true;
        }
*/
    }

    public void LoadData(GameData data)
    {
        dollCovered = data.DollsCoveredCount;
        Covered2 = data.GhostSpawnAfterCovered2;
        Covered3 = data.GhostSpawnAfterCovered3;
    }

    public void SaveData(GameData data)
    {
        data.GhostSpawnAfterCovered2 = Covered2;
        data.GhostSpawnAfterCovered3 = Covered3;
        data.DollsCoveredCount = dollCovered;
    }

    public void deleteData(GameData data)
    {
       
    }
}
