using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CutThisLine : MonoBehaviour
{

    public UnityEvent EventActive, EventAnomaly;
    [SerializeField] bool anomalyActive;
    public bool AnomalyAtcive { get { return anomalyActive; } set {  anomalyActive = value; } }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!AnomalyAtcive)
        {
            if (other.gameObject.tag == "Scissors")
            {
                EventActive.Invoke();
            }
        }
         if (AnomalyAtcive)
        {
            if (other.gameObject.tag == "Scissors")
            {
                EventAnomaly.Invoke();
                AnomalyAtcive = false;
            }
        }
    }
}
