using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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

    [Header("Mini Game")]
    public GameObject minigame;
    public MiniGameAuidition minigamestate;

    public PlayerAttack Throwitem;

    private bool CamOnPerson = true, canplayMinigame;

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

        if (!canplayMinigame) StartCoroutine(DelayCloseMiniGame());
        Ray ray = new Ray(InterectTransform.position, InterectTransform.forward);
        //Debug.DrawRay(InterectTransform.position, InterectTransform.forward);
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
                        StartCoroutine(DelayCamera());
                    }
                }
            }
        }

        if (ChangePOV.IsActiveCamera(WorkShopView))
        {
            if (canplayMinigame) minigame.SetActive(true);
            else minigame.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!CamOnPerson)
            {
                if (ChangePOV.IsActiveCamera(WorkShopView))
                {
                    _InputManager.StopWalk();
                    ChangePOV.SwitchCamera(FirstpersonView);
                    minigame.SetActive(false);
                    minigamestate.LeaveMinigame();
                    CamOnPerson = true;
                }
            }
        }

    }

    public void HaveDollAndCloth()
    {
        canplayMinigame = true;
    }

    public void dontHaveDollAndCloth()
    {
        canplayMinigame = false;
    }

    IEnumerator DelayCloseMiniGame()
    {
        yield return new WaitForSeconds(0.1f);
        minigame.SetActive(false) ;
    }

    IEnumerator DelayCamera()
    {
        yield return new WaitForSeconds(1f);
            CamOnPerson = false;       
    }
    


}
