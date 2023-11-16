using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    public Animator doorAni;
    public bool D;
    public bool Lock;
    public GameObject DoorLock;
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
        else
        {
            StartCoroutine(doorLockDelay());
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

    public void UnLockDoor()
    {
        Lock = false;
    }

    public void LockDoor()
    {
        Lock = true;
    }

    IEnumerator doorLockDelay()
    {
        DoorLock.SetActive(true);
        yield return new WaitForSeconds(2);
        DoorLock.SetActive(false);
    }

  
}
