using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

namespace player
{
    public class Player : MonoBehaviour
    {
        [Header ("CameraThing")]
        [SerializeField] CinemachineVirtualCamera FirstPerson;
        [SerializeField] CinemachineVirtualCamera WorkshopView;
        [SerializeField] CinemachineVirtualCamera BedView;

        [Header ("PlayerThing")]
        PlayerMovement pMove;
        public Transform Interact;
        public float InterectRange;
        public PlayerAttack pAttack;
        public GameObject pHand;
        public AudioSource FootStep;
        public bool ItemHave;

        [Header("CanavThing")]
        public GameObject canvaTotelDoll;
        public GameObject MiniG2Off;

        [Header("Audio")]
        public AudioSource StartWork = null;
        public float AudioRange;

        public void Start()
        {
            pMove = GetComponent<PlayerMovement>();
            StartCoroutine(BedCutscene());
        }

        public void OnEnable()
        {
            ChangePOV.Register(WorkshopView);
            ChangePOV.Register(FirstPerson);
            ChangePOV.Register(BedView);
            ChangePOV.SwitchCamera(BedView);
        }
        public void OnDisable()
        {
            ChangePOV.UnRegister(WorkshopView);
            ChangePOV.UnRegister(FirstPerson);  
            ChangePOV.UnRegister(BedView);  
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
                    print(hitinfo.collider.gameObject.tag);
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
                            }
                        }
                    }
                  

                }  
                
               
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (ChangePOV.IsActiveCamera(WorkshopView))
                {
                    ChangePOV.SwitchCamera(FirstPerson);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    pMove.walkAble();
                    pHand.SetActive(true);
                    MiniG2Off.SetActive(false);
                }
            }
            if (ItemHave)
            {
                if (!ItemHave)
                {
                    if (ChangePOV.IsActiveCamera(WorkshopView))
                    {
                        ChangePOV.SwitchCamera(FirstPerson);
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        pMove.walkAble();
                        pHand.SetActive(true);
                        MiniG2Off.SetActive(false);
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.Space))
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
            }


            #endregion

        }

        IEnumerator BedCutscene()
        {
            yield return new WaitForSeconds(3);
            if (ChangePOV.IsActiveCamera(BedView))
            {
                ChangePOV.SwitchCamera(FirstPerson);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pMove.walkAble();
                pHand.SetActive(true);
                MiniG2Off.SetActive(false);
            }
        }

        public void HaveItem()
        {
            ItemHave = true;
        }
        public void NoItem()
        {
            ItemHave = false;
        }


    }

}
