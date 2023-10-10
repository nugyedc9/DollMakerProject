using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace player
{
    public class Player : MonoBehaviour
    {
        [Header ("CameraThing")]
        [SerializeField] CinemachineVirtualCamera WorkshopView;
        [SerializeField] CinemachineVirtualCamera FirstPerson;

        [Header ("PlayerThing")]
        PlayerMovement pMove;
        public Transform Interact;
        public float InterectRange;
        public PlayerAttack pAttack;
        public GameObject pHand;

        [Header("CanavThing")]
        public GameObject canvaTotelDoll;

        [Header("Audio")]
        public AudioSource StartWork = null;
        public float AudioRange;

        public void Start()
        {
            pMove = GetComponent<PlayerMovement>();
        }

        public void OnEnable()
        {
            ChangePOV.Register(WorkshopView);
            ChangePOV.Register(FirstPerson);
            ChangePOV.SwitchCamera(WorkshopView);
        }
        public void OnDisable()
        {
            ChangePOV.UnRegister(WorkshopView);
            ChangePOV.UnRegister(FirstPerson);  
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
                        if (ChangePOV.IsActiveCamera(WorkshopView))
                        {
                            //print("Switching to FirstPerson");
                            ChangePOV.SwitchCamera(FirstPerson);
                            Cursor.visible = true;
                            Cursor.lockState = CursorLockMode.None;
                            pMove.Stopwalk();
                            pAttack.StopAttack();
                            pHand.SetActive(false);
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
                    Cursor.lockState = CursorLockMode.Locked;
                    pMove.walkAble();
                    pAttack.CanAttack();
                    pHand.SetActive(true);
                }
            }
            #endregion

        }


    }
}
