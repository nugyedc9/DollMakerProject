using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StoryActive : MonoBehaviour
{

    public float DelayBoxShow;

    public UnityEvent EventActive;
    private BoxCollider Box;


    public void Awake()
    {
        Box = GetComponent<BoxCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == "Player")
        {
            EventActive.Invoke();
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(DelayBoxShow > 0)
        {
            DelayBoxShow -= Time.deltaTime;
        }
        else if (DelayBoxShow < 0)
        {
            Box.enabled = true;
            DelayBoxShow = 0;
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
