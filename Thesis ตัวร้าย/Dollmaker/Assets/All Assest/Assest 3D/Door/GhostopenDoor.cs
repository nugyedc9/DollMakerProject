using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostopenDoor : MonoBehaviour
{


    public Door door;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            door.CloseDoorEvent();
            timer = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ghost")
        {
            door.OpenDoorDelayEvent();
        } 
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            timer = 2;
        }
    }

}
