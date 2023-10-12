using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniG2Action : MonoBehaviour
{

    [SerializeField] GameObject[] Action;
    [SerializeField] GameObject SpawnAction1;
    public bool israndomized;

    [SerializeField] float minTranX;
    [SerializeField] float maxTranX;
    [SerializeField] float minTranY;
    [SerializeField] float maxTranY;
    [SerializeField] float Spawnsecond;
    [SerializeField] float CountAction;

    // Start is called before the first frame update
    void Start()
    {
        /*int index = israndomized ? Random.Range(0, Action.Count) : 0;
        if (Action.Count > 0)
        {
            GameObject ActionObj = Instantiate(Action[index], transform.position, transform.rotation);
            ActionObj.transform.parent = transform;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (CountAction == 0)
        {
            StartCoroutine(spawnRan());
        }
        else if(CountAction == 4)
        {
            StopAllCoroutines();
        }

    }

    IEnumerator spawnRan()
    {
        while (true)
        {
            var wantedX = Random.Range(minTranX, maxTranX);
            var wantedY = Random.Range(minTranY, maxTranY);
            var position = new Vector3(wantedX, wantedY);
            GameObject ActionObj = Instantiate(Action[Random.Range(0, Action.Length)], position, Quaternion.identity); 
                ActionObj.transform.parent = transform;
                CountAction++;
                yield return new WaitForSeconds(Spawnsecond);
               
            
        }
    }

}
