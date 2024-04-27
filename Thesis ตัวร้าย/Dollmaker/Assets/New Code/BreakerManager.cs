using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakerManager : MonoBehaviour
{

    [SerializeField ] private List<Event> allEvents = new List<Event>();

    public UnityEvent  cansee;

    SpriteRenderer sp;
    public GameObject[] LightBreaker;
    public Sprite[] spc;

    private void Awake()
    {
         sp = GetComponent<SpriteRenderer>();
        Event[] events = FindObjectsOfType<Event>();
        allEvents.AddRange(events);
    }



   
    public void LightOut()
    {
       

        foreach (Event ev in allEvents)
        { 
          //  Debug.Log("Event: " + ev.ToString() + ", TurnLight: " + ev.TurnLight);
            if (ev.TurnLight)
            {
                ev.TurnOnLight();
                
            }
            ev.ToggleCollider(false);
        }

        LightBreaker[0].SetActive(false);
        LightBreaker[1].SetActive(true);
        sp.sprite = spc[1];
    }

    public void LightOn()
    {
        foreach (Event ev in allEvents)
        {
            if (!ev.TurnLight)
            {
                ev.TurnOnLight();
                ev.ToggleCollider(true);
            }
        }

        LightBreaker[0].SetActive(true);
        LightBreaker[1].SetActive(false);
        sp.sprite = spc[0];
        cansee.Invoke();
    }

}
