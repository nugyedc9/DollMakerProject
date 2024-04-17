using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakerManager : MonoBehaviour
{

    [SerializeField ] private List<Event> allEvents = new List<Event>();

    public UnityEvent  cansee;

    void Start()
    {
        Event[] events = FindObjectsOfType<Event>();
        allEvents.AddRange(events);
    }

    void Update()
    {


  
    }

    public void LightOut()
    {
        foreach (Event ev in allEvents)
        {

            if (ev.TurnLight)
            {

                ev.TurnOnLight();
            }
        }
    }

    public void LightOn()
    {
        foreach (Event ev in allEvents)
        {
            if (!ev.TurnLight)
            {
                ev.TurnOnLight();
            }
        }

        cansee.Invoke();
    }
}
