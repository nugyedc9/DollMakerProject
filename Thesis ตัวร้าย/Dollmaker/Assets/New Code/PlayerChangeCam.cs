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
    [SerializeField] CinemachineVirtualCamera MachineCloseUp;
    [SerializeField] CinemachineVirtualCamera DeskShopView;
    [SerializeField] CinemachineVirtualCamera BedCam;

    [Header("Interect")]
    public Transform InterectTransform;
    public float InterectRange;
    public InputManager _InputManager;
    public GameObject Objective;

    [Header("Mini Game")]
    public MiniGameAuidition minigamestate;
    public GameObject miniGame, ItemOnPlayer, TextOnPlayer, pushHere, DropHere;

    [Header("SelectDesign")]
    public GameObject DesignSelect;
    public GameObject Book;
    public Animator PushBookDown;

    [Header("CloseBoxCol")]
    public BoxCollider WorkShopBoxCol;

    public PlayerAttack Throwitem;

    [SerializeField] bool canplayMinigame;
    public bool CanplayMinigame { get {  return canplayMinigame; } set { canplayMinigame = value; } }

    private bool CamOnPerson = true, CamOnDesk, HaveItem
        , WakeUp, TimeBool = true, Delay;
    float TimerWakeUP, Closecanva; 

    private void OnEnable()
    {
        ChangePOV.Register(FirstpersonView);
        ChangePOV.Register(WorkShopView);
        ChangePOV.Register(MachineCloseUp);
        ChangePOV.Register(BedCam);
        ChangePOV.Register(DeskShopView);
        ChangePOV.SwitchCamera(BedCam);
    }

    private void OnDisable()
    {
        ChangePOV.UnRegister(FirstpersonView);
        ChangePOV.UnRegister(WorkShopView);
        ChangePOV.UnRegister(MachineCloseUp);
        ChangePOV.UnRegister(DeskShopView);
        ChangePOV.UnRegister(BedCam);
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        TimerWakeUP = 11;
    }

    private void Update()
    {
        #region Wake UP
        if(TimeBool)
        TimerWakeUP -= Time.deltaTime;
        if (Delay)
        {
            Closecanva -= Time.deltaTime;
        }
        if (TimerWakeUP <= 0)
        {
            if (!WakeUp)
            {
                if (ChangePOV.IsActiveCamera(BedCam))
                {
                    Objective.SetActive(true);
                    Delay = true;
                    Closecanva = 5;
                    ChangePOV.SwitchCamera(FirstpersonView);
                }
            }
        }

        if(Closecanva <= 0)
        {
            Objective.SetActive(false);
            Delay = false;
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(ChangePOV.IsActiveCamera(BedCam))
            {
                ChangePOV.SwitchCamera(FirstpersonView);
            }
        }


        Ray ray = new Ray(InterectTransform.position, InterectTransform.forward);
        //Debug.DrawRay(InterectTransform.position, InterectTransform.forward);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, InterectRange))
        {
           // Debug.Log(hitInfo.collider.gameObject.tag);
            if(hitInfo.collider.gameObject.tag == "WorkShopDesk")
            {
                // if (!HaveItem)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ChangePOV.IsActiveCamera(FirstpersonView))
                    {
                        WorkShopBoxCol.enabled = false;
                        _InputManager.StopWalk();
                        Throwitem.StopAttack();
                        ItemOnPlayer.SetActive(false);
                        TextOnPlayer.SetActive(false);
                            DropHere.SetActive(true);                        
                        CamOnDesk = true;
                        ChangePOV.SwitchCamera(WorkShopView);
                        StartCoroutine(DelayCamera());
                    }
                }
                
            }
            if (hitInfo.collider.gameObject.tag == "DeskWorkShop")
            {
                // if (!HaveItem)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ChangePOV.IsActiveCamera(FirstpersonView))
                    {
                        _InputManager.StopWalk();
                        Throwitem.StopAttack();
                        DesignSelect.SetActive(true);
                        Book.SetActive(true);
                        ItemOnPlayer.SetActive(false);
                        TextOnPlayer.SetActive(false);
                        CamOnDesk = true;
                        ChangePOV.SwitchCamera(DeskShopView);
                        StartCoroutine(DelayCamera());
                    }
                }

            }

            #region Didn't use
            /*            if (Input.GetMouseButtonDown(0))
                        {
                            if (hitInfo.collider.gameObject.tag == "MachineMiniGame")
                            {
                                if (ChangePOV.IsActiveCamera(WorkShopView))
                                {
                                    ChangePOV.SwitchCamera(MachineCloseUp);
                                    if (!CanplayMinigame)
                                    {
                                        ShowMouse();
                                        DropHere.SetActive(true);
                                    }
                                    CamOnDesk = false;
                                    CamOnPerson = false;
                                }
                            }
                        }*/
            #endregion

        }

        if (CamOnPerson)
        {
            CloseMouse();
            miniGame.SetActive(false);
        }
        else
        {
            ShowMouse();
            if (ChangePOV.IsActiveCamera(WorkShopView))
            {
                if (canplayMinigame)
                {
                    miniGame.SetActive(true);
                }
                else
                {
                    miniGame.SetActive(false);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!CamOnPerson)
            {
                if (CamOnDesk)
                {
                    if (ChangePOV.IsActiveCamera(WorkShopView))
                    {
                        WorkShopBoxCol.enabled = true;
                        _InputManager.StopWalk();
                        Throwitem.CanAttack();
                        ItemOnPlayer.SetActive(true);
                        TextOnPlayer.SetActive(true);
                        DropHere.SetActive(false);
                        ChangePOV.SwitchCamera(FirstpersonView);
                        minigamestate.LeaveMinigame();
                        CamOnPerson = true;
                    }
                    else if (ChangePOV.IsActiveCamera(DeskShopView))
                    {
                        _InputManager.StopWalk();
                        Throwitem.CanAttack();
                        DesignSelect.SetActive(false);
                        Book.SetActive(false);
                        ItemOnPlayer.SetActive(true);
                        TextOnPlayer.SetActive(true);
                        ChangePOV.SwitchCamera(FirstpersonView);
                        CamOnPerson = true;
                    }
                }
                #region Didn't use
                if (!CamOnDesk)
                {
                    if (ChangePOV.IsActiveCamera(MachineCloseUp))
                    {
                        miniGame.SetActive(false);
                        ChangePOV.SwitchCamera(WorkShopView);
                        CamOnDesk = true;
                        CamOnPerson = false;
                    }
                }
                #endregion
            }

        }

    }


    IEnumerator DelayCamera()
    {
        yield return new WaitForSeconds(0.1f);
            CamOnPerson = false;       
    }
    
    public void ItemOnHand()
    {
        HaveItem = true;
    }

    public void NoItem()
    {
        StartCoroutine(DelayNoitem());
    }

    IEnumerator DelayNoitem()
    {
        yield return new WaitForSeconds(0.1f);
        HaveItem = false;
    }

    public void ShowMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
