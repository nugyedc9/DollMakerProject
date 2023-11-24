using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    public Animator doorAni;
    public bool D,key1;
    public bool Lock;
    public AudioSource DoorSound;
    public AudioClip open;
    public AudioClip close;
    public AudioClip knock;
    public AudioClip doorlock;

    public UnityEvent IffopenDoor;
    public UnityEvent IffcloseDoor;

    private void Awake()
    {
        doorAni = GetComponent<Animator>();
    }

    public void Update()
    {
        if (key1)
        {
            UnLockDoor();          
        }
    }

    public void DoorAni()
    {
        if (!Lock)
        {
            if (!D)
            {
                DoorSound.clip = open;
                DoorSound.Play();
                doorAni.Play("Door_open", 0, 0);
                D = true;
                key1 = false;
            }
            else
            {
                DoorSound.clip = close;
                DoorSound.Play();
                doorAni.Play("Door_close", 0, 0);
                D = false;
            }
        }
        else
        {
            DoorSound.clip = doorlock; DoorSound.Play();
        }
    }

    public void ForntDoor()
    {
        DoorSound.Stop();
    }

   public void LockEvent()  
    {
        DoorSound.clip = close; DoorSound.Play();
        Lock = true;
        doorAni.Play("Door_closefast", 0, 0);
        D = false;
    }

    bool jump1shot;

    public void Jump1Atctive()
    {
        if (D) IffopenDoor.Invoke();
        else
        {
            DoorSound.clip = open;
            DoorSound.Play();
            doorAni.Play("Door_open", 0, 0);
            D = true;
            key1 = false;
            IffopenDoor.Invoke();
        }
    }

    public void Key1()
    {
        key1 = true;
    }
    public void UnLockDoor()
    {
        Lock = false;
    }

    public void LockDoor()
    {
        Lock = true;
    }


  
}
