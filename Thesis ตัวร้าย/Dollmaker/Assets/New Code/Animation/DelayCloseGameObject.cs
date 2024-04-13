using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayCloseGameObject : MonoBehaviour
{
    public float DelayTimer;
    public UnityEvent eventTimer;

    private void Update()
    {
        if(DelayTimer > 0)
        {
            DelayTimer -= Time.deltaTime;
        }
        else if(DelayTimer < 0)
        {
            eventTimer.Invoke();
        }
    }
}
