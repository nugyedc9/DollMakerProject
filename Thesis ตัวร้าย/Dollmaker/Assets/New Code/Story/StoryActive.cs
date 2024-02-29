using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoryActive : MonoBehaviour
{

    public UnityEvent EventActive;


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            EventActive.Invoke();
            Destroy(gameObject);
        }
    }

    public void DeleteAnother()
    {
        Destroy(gameObject);
    }

    public void LookActiveevent()
    {
        EventActive.Invoke();
        Destroy(gameObject);    
    }

}
