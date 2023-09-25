using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera WorkshopView;
    [SerializeField] CinemachineVirtualCamera FirstPerson;
    
    PlayerMovement pMove;
     public Transform Interact;
    public float InterectRange;

    private void Start()
    {
       pMove =  GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {   
        ChangePOV.Register(WorkshopView);
        ChangePOV.Register(FirstPerson);    
        ChangePOV.SwitchCamera(WorkshopView);
    }
    private void OnDisable()
    {    
        ChangePOV.UnRegister(WorkshopView);
        ChangePOV.UnRegister(FirstPerson);
    }


    private void Update()
    {
        Ray r = new Ray(Interact.position, Interact.forward);
        Debug.DrawRay(r.origin, r.direction * InterectRange);
        if(Physics.Raycast(r, out RaycastHit hitinfo, InterectRange))
        {
            if(hitinfo.collider.gameObject.tag == "DeskWorkShop")
            {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //print("Camera switch requested");
            if (ChangePOV.IsActiveCamera(WorkshopView))
            {
                //print("Switching to FirstPerson");
                ChangePOV.SwitchCamera(FirstPerson);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pMove.Stopwalk();
            }           
        }
            }

        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ChangePOV.IsActiveCamera(FirstPerson))
            {
                ChangePOV.SwitchCamera(WorkshopView);
                Cursor.visible = false;
                Cursor.lockState= CursorLockMode.Locked;
                pMove.walkAble();
            }
        }
    }

}
