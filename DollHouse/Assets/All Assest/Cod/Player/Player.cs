using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;

namespace player
{
    public class Player : MonoBehaviour
    {
        [Header ("CameraThing")]
        [SerializeField] CinemachineVirtualCamera FirstPerson;
        [SerializeField] CinemachineVirtualCamera WorkshopView;
        [SerializeField] CinemachineVirtualCamera BedView;
        [SerializeField] CinemachineVirtualCamera ForntDoorView;
        [SerializeField] CinemachineVirtualCamera FinalCutSecne;

        [Header ("PlayerThing")]
        PlayerMovement pMove;
        public Transform Interact;
        public float InterectRange;
        [SerializeField] public PlayerAttack pAttack;
        public GameObject pHand;
        public AudioSource FootStep;

        [Header("CanavThing")]
        public GameObject canvaTutorial;
        public GameObject MiniG2Off;
        public int DialogNow = 0;
        public GameObject CanvaBedView;
        public GameObject CanvaForntDoor;
        public Image ImageDialogue;
        [SerializeField] public string[] Dialog;
        public TextMeshProUGUI TextDialogue;
        public GameObject WorkTutorial;

        [Header("CanvaTutorial")]
        public CanPlayMini1 ItemCheck;
        public GameObject Dollhave;
        public GameObject Clothhave;
        public TextMeshProUGUI DollTotel;
        public TextMeshProUGUI ClothTotel;
        public GameObject HowToUse1GOBJ;
        public GameObject HowToUse2GOBJ;
        public GameObject HowToUse3GOBJ;
        public TextMeshProUGUI HowToUse1;
        public TextMeshProUGUI HowToUse2;
        public TextMeshProUGUI HowToUse3;

        [Header("Audio")]
        public AudioSource StartWork;
        public AudioClip canWorkSound;
        public AudioClip cantWorkSound;
        public float AudioRange;

        [Header("Event")]
        public UnityEvent Dollmake;
        public UnityEvent CutSceneFinal;
        public UnityEvent GoBackDoor;

        private Door DoorInterect;
        public PlayerMovement PMove;
        private bool TutorialWork, workSound;
        private Beat BeatDe;

        public void Start()
        {
            pMove = GetComponent<PlayerMovement>();
            StartWork.Stop();
            StartCoroutine(BedCutscene());
        }

        public void OnEnable()
        {
            ChangePOV.Register(WorkshopView);
            ChangePOV.Register(FirstPerson);
            ChangePOV.Register(BedView);
            ChangePOV.Register(ForntDoorView);
            ChangePOV.SwitchCamera(BedView);
        }
        public void OnDisable()
        {
            ChangePOV.UnRegister(WorkshopView);
            ChangePOV.UnRegister(FirstPerson);  
            ChangePOV.UnRegister(BedView);  
            ChangePOV.UnRegister(ForntDoorView);  
        }


        public void Update()
        {
            #region CameraChange

            Ray r = new Ray(Interact.position, Interact.forward);
            Debug.DrawRay(r.origin, r.direction * InterectRange);
            if (Physics.Raycast(r, out RaycastHit hitinfo, InterectRange))
            {
                if (hitinfo.collider.gameObject.tag == "DeskWorkShop")
                {                  
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //print("Camera switch requested");
                        {
                            if (ChangePOV.IsActiveCamera(FirstPerson))
                            {
                                //print("Switching to FirstPerson");
                                ChangePOV.SwitchCamera(WorkshopView);
                                Cursor.visible = true;
                                Cursor.lockState = CursorLockMode.None;

                                FootStep.enabled = false;
                                pMove.Stopwalk();
                                pAttack.StopAttack();
                                pHand.SetActive(false);

                                Dollhave.SetActive(true);
                                Clothhave.SetActive(true);
                                DollTotel.text = "Doll " + ItemCheck.DollHave + " / 3" ;
                                ClothTotel.text = "Cloth " + ItemCheck.ClothHave + " / 3" ;
                                HowToUse3GOBJ.SetActive(true); 
                                HowToUse2GOBJ.SetActive(true); 
                                HowToUse1GOBJ.SetActive(true); 
                                HowToUse1.text = "Start sewing [Hold space]";
                                HowToUse2.text = "Skill check [W/A/S/D]";
                                HowToUse3.text = "Quit table [Q]";

                                if(!TutorialWork) WorkTutorial.SetActive(true);
                                if (Input.GetKeyDown(KeyCode.Space))
                                {
                                    MiniG2Off.SetActive(true);
                                }
                                if (Input.GetKeyUp(KeyCode.Space))
                                {
                                    MiniG2Off.SetActive(false);
                                    BeatDe = GetComponent<Beat>();
                                    BeatDe.ExitMini1();
                                }
                            }
                        }
                    }
                }
                if (hitinfo.collider.gameObject.tag == "ForntDoor")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (ChangePOV.IsActiveCamera(FirstPerson))
                        {
                            TextDialogue.text = Dialog[DialogNow];
                            DoorInterect = hitinfo.collider.gameObject.GetComponent<Door>();
                            DoorInterect.ForntDoor();
                            CanvaForntDoor.SetActive(true);
                            PMove.Stopwalk();
                            ChangePOV.SwitchCamera(ForntDoorView);
                        }
                    }

                }

            }
            if (ChangePOV.IsActiveCamera(ForntDoorView))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    DialogNow++;
                    if (DialogNow < Dialog.Length) TextDialogue.text = Dialog[DialogNow];

                }
            }
            if (Input.GetButton("Fire1"))
            {
                if (DialogNow >= Dialog.Length)
                {
                    if (ChangePOV.IsActiveCamera(ForntDoorView))
                    {
                        ChangePOV.SwitchCamera(FirstPerson);
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        pMove.walkAble();
                        CanvaForntDoor.SetActive(false);
                    }
                    GoBackDoor.Invoke();
                }
            }


            if (ChangePOV.IsActiveCamera(WorkshopView))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ChangePOV.SwitchCamera(FirstPerson);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    pAttack.CanAttack();
                    pMove.walkAble();
                    pHand.SetActive(true);
                    MiniG2Off.SetActive(false);

                    Dollhave.SetActive(false);
                    Clothhave.SetActive(false);
                    HowToUse1GOBJ.SetActive(false);
                    HowToUse3GOBJ.SetActive(false);
                    HowToUse2GOBJ.SetActive(false);

                    workSound = false;
                }
            }

                if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ChangePOV.IsActiveCamera(BedView))
                {
                    ChangePOV.SwitchCamera(FirstPerson);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    pMove.walkAble();
                    pHand.SetActive(true);
                    MiniG2Off.SetActive(false);
                }
                if (ChangePOV.IsActiveCamera(WorkshopView))
                {
                    MiniG2Off.SetActive(true);                 
                    Dollmake.Invoke();
                    if (workSound)
                    {
                        StartWork.clip = canWorkSound;
                        StartWork.Play();
                    }
                    else
                    {
                        StartWork.clip = cantWorkSound; StartWork.Play();
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (ChangePOV.IsActiveCamera(WorkshopView))
                {
                    MiniG2Off.SetActive(false);
                    StartWork.Stop();
                    TutorialWork = true;
                    if(TutorialWork) WorkTutorial.SetActive(false);
                    Dollmake.Invoke();
                }
            }

            #endregion

        }

        IEnumerator BedCutscene()
        {
            yield return new WaitForSeconds(7);
            if (ChangePOV.IsActiveCamera(BedView))  
            {
                ChangePOV.SwitchCamera(FirstPerson);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pMove.walkAble();
                pHand.SetActive(true);
                MiniG2Off.SetActive(false);
                canvaTutorial.SetActive(true) ; 
            }
            yield return new WaitForSeconds(2);
            CanvaBedView.SetActive(false);

        }

        public void Checkclick()
        {
            print("Click");
        }

        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "FinalCutScene")
            {
                ChangePOV.SwitchCamera(FinalCutSecne);
                StartWork.Stop();
                CutSceneFinal.Invoke();
            }
        }

        public void PlaySoundWork()
        {
            workSound = true;
        }
        public void StopSoundWork()
        {
            workSound = false;
        }
    }


}