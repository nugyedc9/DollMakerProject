using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMon : MonoBehaviour
{
    public GameObject Mon;
    public int xPos;
    public int YPos;
    public int zPos;   
    public int EnemyCount;
    public int EnemyInScence;
    public List<Target> monGroup;
    public float Timeout;
    public int POutSigth;
    public float TimeCount;
    


    void Start()
    {
        StartCoroutine(EnemySpawn());
    }
    private void Update()
    {
        POutSigth = 0;

        for(int i = 0; i < monGroup.Count; i++)
        {
            if(monGroup[i].playerInSightRange == true)
            {
                POutSigth++;
                Debug.Log("Found");
            }
           
        }
        if (POutSigth == monGroup.Count)
        {
            TimeCount += Time.deltaTime;
            if (TimeCount >= Timeout)
            {
                PlayerNotFound();

            }
        }
        else
            TimeCount = 0;
        
    }
    IEnumerator EnemySpawn()
    {
        while (EnemyCount < EnemyInScence)
        {
            GameObject m = Instantiate(Mon, new Vector3(xPos, YPos, zPos), Quaternion.identity);
            Target t = m.GetComponent<Target>();
            m.name = "enamy"+ EnemyCount;
            t.spawnMon = this;
            monGroup.Add(t);
            yield return new WaitForSeconds(0.1f);
            EnemyCount += 1;

        }
    }

    public void FoundPlayer()
    {
        for (int i = 0; i < monGroup.Count; i++)
        {
            if(monGroup[i].playerInSightRange == false)
            monGroup[i].ChasePlayer();
            monGroup[i].playerInSightRange = true;
            monGroup[i].state = 1;
            
        } 
    }

    public void PlayerNotFound()
    {
        for (int i = 0; i < monGroup.Count; i++)
        {
            monGroup[i].playerInSightRange = false;
            monGroup[i].state = 0;

               
        }
    }
    

}
