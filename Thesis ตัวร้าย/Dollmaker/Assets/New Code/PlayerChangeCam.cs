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
    [SerializeField] CinemachineVirtualCamera ChangeViewOnDesk;
    [SerializeField] CinemachineVirtualCamera DeskShopView;
    [SerializeField] CinemachineVirtualCamera BedCam;
    [SerializeField] CinemachineVirtualCamera PushClothOnDollView;

    [Header("Key Item Inventory")]
    public TabTutorial TabOn;
    public GameObject OpenInvBut, CloseInvBut;
    public Animator InvOpen;
    [SerializeField] bool openkeyItemInv;
    public bool OpenKeyItemInv {  get { return openkeyItemInv; } set { openkeyItemInv = value; } }

    [Header("Interect")]
    public Transform InterectTransform;
    public float InterectRange;
    public InputManager _InputManager;
    public GameObject Objective;

    [Header("Mini Game")]
    public MiniGameAuidition minigamestate;
    public GameObject HandSwing;
    public GameObject miniGame, ItemOnPlayer, TextOnPlayer, pushHere, DropHere;

    [Header("SelectDesign")]
    public Animator InvAnim;
    public GameObject DesignSelect;
    public GameObject Book;

    [Header("SelectDollDesign")]
    public GameObject DesignDollSelect;
    public GameObject BookDoll;
    public DollDropDesignTrigger DropDoll;

    [Header("TurnCam")]
    public GameObject TurnOut;
    public GameObject TurnIn;

    [Header("CloseBoxCol")]
    public BoxCollider WorkShopBoxCol;
    public CanPlayMini1 CheckCanplayMiniG;
    public PlayerAttack Throwitem;

    [SerializeField] bool canplayMinigame;
    public bool CanplayMinigame { get {  return canplayMinigame; } set { canplayMinigame = value; } }

    private bool  CamOnDesk, HaveItem
        , WakeUp, TimeBool = true, Delay;
    float TimerWakeUP, Closecanva;

    private bool CamOnPerson = true;
    public bool camOnPerSon { get { return CamOnPerson; } set { CamOnPerson = value; } }

    [SerializeField] bool closeInterectShow;
    public bool CloseInterectShow { get { return closeInterectShow; } set { closeInterectShow = value; } }

    private void OnEnable()
    {
        ChangePOV.Register(FirstpersonView);
        ChangePOV.Register(WorkShopView);
        ChangePOV.Register(ChangeViewOnDesk);
        ChangePOV.Register(BedCam);
        ChangePOV.Register(DeskShopView);
        ChangePOV.Register(PushClothOnDollView);
        ChangePOV.SwitchCamera(BedCam);
    }

    private void OnDisable()
    {
        ChangePOV.UnRegister(FirstpersonView);
        ChangePOV.UnRegister(WorkShopView);
        ChangePOV.UnRegister(ChangeViewOnDesk);
        ChangePOV.UnRegister(DeskShopView);
        ChangePOV.UnRegister(PushClothOnDollView);
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
                        CheckCanplayMiniG.OnDesk = true;                  
                        CamOnDesk = true;
                        LookOutGhost = false;
                        TurnOut.SetActive(true);
                        TurnIn.SetActive(false);
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

            if (hitInfo.collider.gameObject.tag == "DeskPushClothToDoll")
            {
                // if (!HaveItem)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ChangePOV.IsActiveCamera(FirstpersonView))
                    {                   
                        _InputManager.StopWalk();
                        Throwitem.StopAttack();
                        ItemOnPlayer.SetActive(false);
                        if (!DropDoll.CloseboxDropDoll)
                            DesignDollSelect.SetActive(true);
                        BookDoll.SetActive(true);
                        TextOnPlayer.SetActive(false);
                        CamOnDesk = true;
                        ChangePOV.SwitchCamera(PushClothOnDollView);
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
            CloseInterectShow = false;
            if(TabOn.OpenTutor == false)
            CloseMouse();          
            OpenKeyItemInv = false;
            OpenInvBut.SetActive(false);
            CloseInvBut.SetActive(false);
            HandSwing.SetActive(false);
            miniGame.SetActive(false);
        }
        else
        {          
            ShowMouse();
            if (ChangePOV.IsActiveCamera(WorkShopView))
            {
                if (canplayMinigame)
                {
                    HandSwing.SetActive(true);
                    miniGame.SetActive(true);
                }
                else
                {
                    HandSwing.SetActive(false);
                    miniGame.SetActive(false);
                }
            }
            if (ChangePOV.IsActiveCamera(ChangeViewOnDesk))
            {
                if (canplayMinigame)
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
                        TurnOut.SetActive(false);
                        TurnIn.SetActive(false);
                        CheckCanplayMiniG.OnDesk = false;
                        InvOpen.Play("InvClose");
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
                        InvOpen.Play("InvClose");
                        ChangePOV.SwitchCamera(FirstpersonView);
                        CamOnPerson = true;
                    }
                    else if (ChangePOV.IsActiveCamera(ChangeViewOnDesk))
                    {
                        WorkShopBoxCol.enabled = true;
                        _InputManager.StopWalk();
                        Throwitem.CanAttack();
                        ItemOnPlayer.SetActive(true);
                        TextOnPlayer.SetActive(true);
                        DropHere.SetActive(false);
                        InvOpen.Play("InvClose");
                        ChangePOV.SwitchCamera(FirstpersonView);
                        minigamestate.LeaveMinigame();
                        CamOnPerson = true;
                    }
                    else if (ChangePOV.IsActiveCamera(PushClothOnDollView))
                    {
                        _InputManager.StopWalk();
                        Throwitem.CanAttack();
                        DesignDollSelect.SetActive(false);
                        BookDoll.SetActive(false);
                        ItemOnPlayer.SetActive(true);
                        TextOnPlayer.SetActive(true);
                        InvOpen.Play("InvClose");
                        ChangePOV.SwitchCamera(FirstpersonView);
                        CamOnPerson = true;
                    }
                }

            }

        }

    }


    IEnumerator DelayCamera()
    {
        yield return new WaitForSeconds(0.1f);
            CamOnPerson = false;
        openkeyItemInv = true;
        InvAnim.enabled = true;
        CloseInterectShow = true;
        Throwitem.Attack = false;
        OpenInvBut.SetActive(true);
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


    bool LookOutGhost;
    public void TrunView()
    {
        if (!LookOutGhost)
        {
            if (ChangePOV.IsActiveCamera(WorkShopView))
            {
                ChangePOV.SwitchCamera(ChangeViewOnDesk);
                LookOutGhost = true;
            }
        }
        else
        {
            if (ChangePOV.IsActiveCamera(ChangeViewOnDesk))
            {
                if (canplayMinigame)
                {
                    miniGame.SetActive(true);
                }
                ChangePOV.SwitchCamera(WorkShopView);
                LookOutGhost=false;
            } 
        }
    }

}
