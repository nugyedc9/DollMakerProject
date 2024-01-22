using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChangeCam : MonoBehaviour
{
    [Header ("Camera")]
    [SerializeField] CinemachineVirtualCamera FirstpersonView;
    [SerializeField] CinemachineVirtualCamera WorkShopView;

    [Header("Interect")]
    public Transform InterectTransform;
    public float InterectRange;
    public InputManager _InputManager;
    private void OnEnable()
    {
        ChangePOV.Register(FirstpersonView);
        ChangePOV.Register(WorkShopView);
        ChangePOV.SwitchCamera(FirstpersonView);
    }

    private void OnDisable()
    {
        ChangePOV.UnRegister(FirstpersonView);
        ChangePOV.UnRegister(WorkShopView);
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update()
    {
        Ray ray = new Ray(InterectTransform.position, InterectTransform.forward);
        Debug.DrawRay(InterectTransform.position, InterectTransform.forward);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, InterectRange))
        {
           // Debug.Log(hitInfo.collider.gameObject.tag);
            if(hitInfo.collider.gameObject.tag == "WorkShopDesk")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (ChangePOV.IsActiveCamera(FirstpersonView))
                    {
                        _InputManager.StopWalk();
                        ChangePOV.SwitchCamera(WorkShopView);
                    }
                    else if(ChangePOV.IsActiveCamera(WorkShopView))
                    {
                        _InputManager.StopWalk();
                        ChangePOV.SwitchCamera(FirstpersonView);
                    }
                }
            }
        }
    }
}
