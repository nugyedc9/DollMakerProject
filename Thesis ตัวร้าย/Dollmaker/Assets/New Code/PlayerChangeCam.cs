using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChangeCam : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] CinemachineVirtualCamera FirstpersonView;
    [SerializeField] CinemachineVirtualCamera WorkShopView;
    [SerializeField] CinemachineVirtualCamera ChangeViewOnDesk;
    [SerializeField] CinemachineVirtualCamera DeskShopView;
    [SerializeField] CinemachineVirtualCamera BedCam;
    [SerializeField] CinemachineVirtualCamera PushClothOnDollView;
    [SerializeField] CinemachineVirtualCamera _1GetScrissorCam;
    [SerializeField] CinemachineVirtualCamera _2MakeDollCutClothCam;
    [SerializeField] CinemachineVirtualCamera _3SewingDollCam;
    [SerializeField] CinemachineVirtualCamera _4DollandClothCam;
    [SerializeField] CinemachineVirtualCamera DollHenshin;
    [SerializeField] CinemachineVirtualCamera SleepCam;

    public PlayerAttack Pattack;
    public Animator TutorialCam2, TutorialCam3, TutorialCam4;

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
    public GameObject Book , Allline;

    [Header("SelectDollDesign")]
    public GameObject DesignDollSelect;
    public GameObject BookDoll;
    public DollDropDesignTrigger DropDoll;


    [Header("Tutorial Arrow")]
    public GameObject clothTutorial;
    public GameObject sewingTutorial, DollTutorial, clothDoll, CutLine;

    [Header("TurnCam")]
    public GameObject TurnOut;
    public GameObject TurnIn;

    public GameObject endgameCanva;
    [SerializeField] bool endGame;
    public bool EndGame { get { return endGame; }set { endGame = value; } }


    [Header("CloseBoxCol")]
    public BoxCollider WorkShopBoxCol;
    public BoxCollider _1Story;
    public CanPlayMini1 CheckCanplayMiniG;
    public PlayerAttack Throwitem;

    [SerializeField] bool canplayMinigame;
    public bool CanplayMinigame { get {  return canplayMinigame; } set { canplayMinigame = value; } }

    public float TutorialTime1, TutorialTime2, TutorialTime3, TutorialTime4, TimerWakeUP, ghosthenshinTime, SleepTimer;

    private bool  CamOnDesk, HaveItem
        , WakeUp, TimeBool = true, Delay,
        _1designCloth, _1sewing, _1doll, _1clothDoll,
        _1cutLine;

    public bool _1DesignCloth { get { return _1designCloth; } set { _1designCloth = value; } }
    public bool _1Sewing { get { return _1sewing; } set { _1sewing = value; } }
    public bool _1Doll { get { return _1doll; } set { _1doll = value; } }
    public bool _1ClothDoll { get { return _1clothDoll; } set { _1clothDoll = value; } }
    public bool T1CutLine { get { return _1cutLine; } set { _1cutLine = value; } }

    float Closecanva, TutorialTimeIncode, CamOnTutorial;

    private bool CamOnPerson = true, onCutScene;
    public bool camOnPerSon { get { return CamOnPerson; } set { CamOnPerson = value; } }
    public bool OnCutScene { get { return onCutScene; } set { onCutScene = value; } }

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
        ChangePOV.Register(_1GetScrissorCam);
        ChangePOV.Register(_2MakeDollCutClothCam);
        ChangePOV.Register(_3SewingDollCam);
        ChangePOV.Register(_4DollandClothCam);
        ChangePOV.Register(DollHenshin);
        ChangePOV.Register(SleepCam);
        ChangePOV.SwitchCamera(BedCam);
    }

    private void OnDisable()
    {
        ChangePOV.UnRegister(FirstpersonView);
        ChangePOV.UnRegister(WorkShopView);
        ChangePOV.UnRegister(ChangeViewOnDesk);
        ChangePOV.UnRegister(DeskShopView);
        ChangePOV.UnRegister(PushClothOnDollView);
        ChangePOV.UnRegister(_1GetScrissorCam);
        ChangePOV.UnRegister(_2MakeDollCutClothCam);
        ChangePOV.UnRegister(_3SewingDollCam);
        ChangePOV.UnRegister(_4DollandClothCam);
        ChangePOV.UnRegister(DollHenshin);
        ChangePOV.UnRegister(SleepCam);
        ChangePOV.UnRegister(BedCam);
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        OnCutScene = true;
        _InputManager.StopWalk();
    }

    private void Update()
    {
        #region Wake UP
        if (TimeBool)
        {
            TimerWakeUP -= Time.deltaTime;
            camOnPerSon = false;
        }

        if (TimerWakeUP <= 0)
        {
            if (!WakeUp)
            {
                if (ChangePOV.IsActiveCamera(BedCam))
                {
                    _1Story.enabled = true;
                    TimeBool = false;
                    camOnPerSon = true;
                    OnCutScene = false;
                    _InputManager.StopWalk();
                    ChangePOV.SwitchCamera(FirstpersonView);
                }
            }
        }


        #region cam tutorial
        if (TutorialTimeIncode > 0)
        {
            TutorialTimeIncode -= Time.deltaTime;
        }
        if(TutorialTimeIncode<= 0)
        {
            if (CamOnTutorial == 0)
            {
                if (ChangePOV.IsActiveCamera(_1GetScrissorCam))
                {
                    TutorialTimeIncode = TutorialTime2;
                    TutorialCam2.Play("gocutcloth");
                    CamOnTutorial = 1;
                    ChangePOV.SwitchCamera(_2MakeDollCutClothCam);
                }
            }
            else if (CamOnTutorial == 1)
            {
                if (ChangePOV.IsActiveCamera(_2MakeDollCutClothCam))
                {
                    TutorialTimeIncode = TutorialTime3;
                    TutorialCam3.Play("sewingdollcloth");
                    CamOnTutorial = 2;
                    ChangePOV.SwitchCamera(_3SewingDollCam);
                }
            }
            else if (CamOnTutorial == 2)
            {
                if (ChangePOV.IsActiveCamera(_3SewingDollCam))
                {
                    TutorialTimeIncode = TutorialTime4;
                    TutorialCam4.Play("dollandcloth");
                    CamOnTutorial = 3;
                    ChangePOV.SwitchCamera(_4DollandClothCam);
                }
            }

            else if (CamOnTutorial == 3)
            {
                if (ChangePOV.IsActiveCamera(_4DollandClothCam))
                {
                    _InputManager.StopWalk();
                    Throwitem.CanAttack();
                    ItemOnPlayer.SetActive(true);
                    TextOnPlayer.SetActive(true);
                    ChangePOV.SwitchCamera(FirstpersonView);
                    CamOnPerson = true;
                    OnCutScene = false;
                }
            }
            
            else if(CamOnTutorial == 4)
            {
                if (ChangePOV.IsActiveCamera(DollHenshin))
                {
                    _InputManager.StopWalk();
                    Throwitem.CanAttack();
                    ItemOnPlayer.SetActive(true);
                    TextOnPlayer.SetActive(true);
                    ChangePOV.SwitchCamera(FirstpersonView);
                    CamOnPerson = true;
                    OnCutScene = false;
                }
            }

            else if (CamOnTutorial == 5)
            {
                OnCutScene = true;
                camOnPerSon = false;
                endgameCanva.SetActive(true);
                EndGame = true;
            }
        }
        #endregion

        #endregion

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(ChangePOV.IsActiveCamera(BedCam))
            {
                _1Story.enabled = true;
                TimeBool = false;
                camOnPerSon = true;
                OnCutScene = false;
                _InputManager.StopWalk();
                ChangePOV.SwitchCamera(FirstpersonView);
            }
            else if (ChangePOV.IsActiveCamera(DollHenshin))
            {
                TimeBool = false;
                camOnPerSon = true;
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
                      //  TextOnPlayer.SetActive(false);
                        CheckCanplayMiniG.OnDesk = true;                  
                        CamOnDesk = true;
                        LookOutGhost = false;
                        TurnOut.SetActive(true);
                        TurnIn.SetActive(false);
                        ChangePOV.SwitchCamera(WorkShopView);
                        StartCoroutine(DelayCamera());

                        if (!_1Sewing)
                        {
                            sewingTutorial.SetActive(true);
                        }
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
                        Allline.SetActive(true);
                        ItemOnPlayer.SetActive(false);
                       // TextOnPlayer.SetActive(false);
                        CamOnDesk = true;
                        ChangePOV.SwitchCamera(DeskShopView);
                        StartCoroutine(DelayCamera());

                        if (!_1DesignCloth)
                        {
                            clothTutorial.SetActive(true);
                        }
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
                      //  TextOnPlayer.SetActive(false);
                        CamOnDesk = true;
                        ChangePOV.SwitchCamera(PushClothOnDollView);
                        StartCoroutine(DelayCamera());

                        if (!_1Doll)
                        {
                            DollTutorial.SetActive(true);
                        }

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
            if (!TabOn.OpenTutor && Pattack.isPause && Time.timeScale == 1 )
                    CloseMouse();
    
            OpenKeyItemInv = false;
            OpenInvBut.SetActive(false);
            CloseInvBut.SetActive(false);
            HandSwing.SetActive(false);
            miniGame.SetActive(false);
        }
        else
        {
            if (!OnCutScene)
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
            else
            {
                if (endGame) ShowMouse();
                OpenInvBut.SetActive(false);
                CloseInvBut.SetActive(false);
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
                      
                        if (!_1Sewing)
                            sewingTutorial.SetActive(false);
                        CloseMouse();
                        ChangePOV.SwitchCamera(FirstpersonView);
                        minigamestate.LeaveMinigame();
                        CamOnPerson = true;
                        CamOnDesk = false;
                    }
                    else if (ChangePOV.IsActiveCamera(DeskShopView))
                    {
                        _InputManager.StopWalk();
                        Throwitem.CanAttack();
                        DesignSelect.SetActive(false);
                        Book.SetActive(false);
                        Allline.SetActive(false);
                        ItemOnPlayer.SetActive(true);
                        TextOnPlayer.SetActive(true);
                        InvOpen.Play("InvClose");

                        if (!_1DesignCloth)
                            clothTutorial.SetActive(false);
                        else if(!T1CutLine && _1DesignCloth)
                            CutLine.SetActive(false);
                        CloseMouse();
                        ChangePOV.SwitchCamera(FirstpersonView);
                        CamOnPerson = true;
                        CamOnDesk = false;
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
                        CamOnDesk = false;
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

                        if (!_1Doll)
                            DollTutorial.SetActive(false);
                        if(!_1clothDoll)
                            clothDoll.SetActive(false);
                        CloseMouse();
                        ChangePOV.SwitchCamera(FirstpersonView);
                        CamOnPerson = true;
                        CamOnDesk = false;
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

    public void ChangeCamToTutorial()
    {
        if (ChangePOV.IsActiveCamera(FirstpersonView))
        {
            OnCutScene = true;
            _InputManager.StopWalk();
            Throwitem.StopAttack();
            ChangePOV.SwitchCamera(_1GetScrissorCam);
            TutorialTimeIncode = TutorialTime1;
        }
    }

    public void ChangCamToGhost()
    {
        if (ChangePOV.IsActiveCamera(FirstpersonView))
        {
            OnCutScene=true;
            CamOnTutorial = 4;
            _InputManager.StopWalk();
            Throwitem.StopAttack();
            ChangePOV.SwitchCamera(DollHenshin);
            TutorialTimeIncode = ghosthenshinTime;
        }
    }

    public void ChangeCamToSleep()
    {
        if (ChangePOV.IsActiveCamera(FirstpersonView))
        {
            OnCutScene = true;
            CamOnTutorial = 5;
            _InputManager.StopWalk();
            Throwitem.StopAttack();
            ItemOnPlayer.SetActive(false);
            ChangePOV.SwitchCamera(SleepCam);
            TutorialTimeIncode = SleepTimer;
        }
    }

}
