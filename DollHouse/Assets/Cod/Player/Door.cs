using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator doorAni;
    public bool D;
    public bool Lock;
    public AudioSource DoorSound;
    public AudioClip open;
    public AudioClip close;
    public AudioClip knock;
    public AudioClip doorlock;

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
                DoorSound.clip = open;
                DoorSound.Play();
                doorAni.Play("Door_open", 0, 0);
                D = true;
            }
            else
            {
                DoorSound.clip = close;
                DoorSound.Play();
                doorAni.Play("Door_close", 0, 0);
                D = false;
            }
        }
    }

    public void ForntDoor()
    {
        DoorSound.Stop();
    }

   public void LockEvent()  
    {
        DoorSound.clip = doorlock; DoorSound.Play();
        Lock = true;
        doorAni.Play("Door_close", 0, 0);
        D = false;
    }
    public void HaveKey()
    {
        Lock = false;
    }


  
}
