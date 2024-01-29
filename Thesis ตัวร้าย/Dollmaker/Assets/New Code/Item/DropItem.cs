using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> itemInSlot = new List<GameObject>();
    bool haveItem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
        }
    }

    public void GotItem()
    {
        if (!haveItem)
        {
            foreach (Transform item in transform)
            {
                itemInSlot.Add(item.gameObject);
            }
            haveItem = true;
        }
    }

}
