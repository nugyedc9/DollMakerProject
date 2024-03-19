using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Door : MonoBehaviour
{

    public Animator doorAni;
    public int DoorID;
    public bool D;

    [SerializeField] bool _lock;
    public bool Lock { get { return _lock; } set { _lock = value; } }
    public AudioSource DoorSound;
    public AudioClip open;
    public AudioClip close;
    public AudioClip knock;
    public AudioClip doorlock;

    public UnityEvent IffopenDoor;
    public UnityEvent IffcloseDoor;
    public UnityEvent afterGetDoll;


    bool _afterDoll;
    public bool Aftergetdoll { get { return _afterDoll; } set { _afterDoll = value; } }

     [SerializeField] float delayDooropenEvent;
    public float DelayDoorOpenEvent { get { return delayDooropenEvent; } set { delayDooropenEvent = value; } }



    private void Awake()
    {
        doorAni = GetComponent<Animator>();
    }

    public void Update()
    {
        if (DelayDoorOpenEvent > 0) DelayDoorOpenEvent -= Time.deltaTime;
        else if (DelayDoorOpenEvent < 0)
        {
            DoorSound.clip = open;
            DoorSound.Play();
            doorAni.Play("Door_open", 0, 0);
            D = true;
            DelayDoorOpenEvent = 0;
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

                if (Aftergetdoll)
                {
                    afterGetDoll.Invoke();
                    Aftergetdoll = false;
                }

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
            IffopenDoor.Invoke();
        }
    }


    public void OpenDoorDelayEvent()
    {
        if (!Lock)
        {
            DoorSound.clip = open;
            DoorSound.Play();
            doorAni.Play("Door_open", 0, 0);
            D = true;
        }
    }

    public void CloseDoorEvent()
    {
        if (!Lock)
        {
            DoorSound.clip = close;
            DoorSound.Play();
            doorAni.Play("Door_close", 0, 0);
            D = false;
        }
    }
    
}
