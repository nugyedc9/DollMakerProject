using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseCloseTap : MonoBehaviour, IPointerClickHandler
{

    public UnityEvent CloseTap;

    public void OnPointerClick(PointerEventData eventData)
    {
        CloseTap.Invoke();
    }

}
