using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PCHeckDirection : MonoBehaviour
{
    public UnityEvent PNearGhost;

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PNearGhost.Invoke();   
        }
    }

}
