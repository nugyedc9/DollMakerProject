using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator doorAni;
    public bool D;
    public bool Lock;

    private void Awake()
    {
        doorAni = GetComponent<Animator>();
    }

    public void DoorAni()
    {
        if (!Lock)
        {
            if (!D)
            {
                doorAni.Play("Door", 0, 0);
                D = true;
            }
            else
            {
                doorAni.Play("DoorClose", 0, 0);
                D = false;
            }
        }
    }

   public void LockEvent()  
    {
        Lock = true;
        doorAni.Play("DoorClose", 0, 0);
        D = false;
    }
    public void HaveKey()
    {
        Lock = false;
    }


  
}
