using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniMapFlood : MonoBehaviour
{

    public UnityEvent MapActive;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            MapActive.Invoke();
        }
    }

}
