using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOnSelectInv : MonoBehaviour, IPointerEnterHandler
{

    public int Number;
    public PlayerAttack Selectitem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Selectitem.Mouseselect(Number);
    }
}
