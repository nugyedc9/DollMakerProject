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
    public BoxCollider minigameBoxCol;
    public BoxCollider BasketCol;
    public GameObject MiniGame, ItemOnPlayer, TextOnPlayer, pushHere;
    public MiniGameAuidition minigamestate;

    [Header("SelectDesign")]
    public GameObject DesignSelect;

    public PlayerAttack Throwitem;

    private bool CamOnPerson = true, CamOnDesk, canplayMinigame, HaveItem
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


        if (!canplayMinigame) StartCoroutine(DelayCloseMiniGame());
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
                            _InputManager.StopWalk();
                            Throwitem.StopAttack();
                            ItemOnPlayer.SetActive(false);
                            TextOnPlayer.SetActive(false);
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
                        ShowMouse();
                        ItemOnPlayer.SetActive(false);
                        TextOnPlayer.SetActive(false);
                        CamOnDesk = true;
                        ChangePOV.SwitchCamera(DeskShopView);
                        StartCoroutine(DelayCamera());
                    }
                }

            }
            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.collider.gameObject.tag == "MachineMiniGame")
                {
                    if (ChangePOV.IsActiveCamera(WorkShopView))
                    {
                        MiniGame.SetActive(true);
                        ChangePOV.SwitchCamera(MachineCloseUp);
                        CamOnDesk = false;
                        minigameBoxCol.enabled = false;
                        CamOnPerson = false;
                    }
                }
            }
        }

        if (ChangePOV.IsActiveCamera(WorkShopView))
        {
            if (canplayMinigame) minigameBoxCol.enabled = true;
            else minigameBoxCol.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!CamOnPerson)
            {
                if (CamOnDesk)
                {
                    if (ChangePOV.IsActiveCamera(WorkShopView))
                    {
                        _InputManager.StopWalk();
                        Throwitem.CanAttack();
                        ItemOnPlayer.SetActive(true);
                        TextOnPlayer.SetActive(true);
                        ChangePOV.SwitchCamera(FirstpersonView);
                        minigameBoxCol.enabled = false;
                        minigamestate.LeaveMinigame();
                        CamOnPerson = true;
                    }
                    else if (ChangePOV.IsActiveCamera(DeskShopView))
                    {
                        _InputManager.StopWalk();
                        Throwitem.CanAttack();
                        DesignSelect.SetActive(false);
                        CloseMouse();
                        ItemOnPlayer.SetActive(true);
                        TextOnPlayer.SetActive(true);
                        ChangePOV.SwitchCamera(FirstpersonView);
                        minigameBoxCol.enabled = false;
                        minigamestate.LeaveMinigame();
                        CamOnPerson = true;
                    }
                }
                if (!CamOnDesk)
                {
                    if (ChangePOV.IsActiveCamera(MachineCloseUp))
                    {
                        MiniGame.SetActive(false);
                        ChangePOV.SwitchCamera(WorkShopView);
                        CamOnDesk = true;
                        CamOnPerson = false;
                    }
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
        minigameBoxCol.enabled = false ;
    }

    IEnumerator DelayCamera()
    {
        yield return new WaitForSeconds(1f);
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

    public void ChooseThisButton()
    {

    }

}
