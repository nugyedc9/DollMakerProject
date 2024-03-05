using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CutThisLine : MonoBehaviour
{

    public UnityEvent EventActive;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Scissors")
        {
            EventActive.Invoke();
        }
    }
}
