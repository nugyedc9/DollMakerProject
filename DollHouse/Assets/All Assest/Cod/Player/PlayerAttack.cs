using player;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    public float ShootSpeed = 30;
    public float DropSpeed;
    public Camera FpsCam;
    public GameObject projectile;
    public Transform RH;
    public Transform pickUPPoint;
    public float InterectRange;
    public bool Attack, LightOn, CanDropItem, CrossOnHand, DollOnHand, ClothOnHand, LightOnHand;
    public float Pickrange;
    private Vector3 destination;

    [Header("Audio")]
    public AudioClip HitGhostSound;
    public AudioClip HitWindSound;
    public AudioSource HitAudio;
    public AudioClip LightOutLetsGooo;


    [Header("PLayerLight")]
    public GameObject Light;
    public GameObject pointLight;
    public Animator LanternAni;

    [Header("Item On Hand")]
    public GameObject CorssR;
    public Animator CorssAni;
    public GameObject DollR;
    public Animator DollAni;
    public GameObject ClothR;
    public Animator ClothAni;

    [Header("Item Drop")]
    public GameObject CrossD;
    public GameObject CrossD2;
    public GameObject CrossD1;
    public GameObject DollD;
    public GameObject ClothD;

    [Header("CrossAction")]
    public float curHpCross;
    private bool crossruin;

    [Header("CanvaDialogue")]
    public GameObject CanvaDialog;
    public Image CanvaImage;
    [SerializeField] public string[] Dialogue;
    public TextMeshProUGUI Textdialogue;

    [Header("CanvaInterect")]
    public Image InterectAble;
    public Sprite InterectSprite;
    public Sprite pointSprite; 
    public GameObject ItemText;
    public GameObject E;
    public TextMeshProUGUI ItemName;
    private bool dialogCheck, InterectItem;

    [Header("TextShow")]
    public TextMeshProUGUI TextYouhere;
    public TextMeshProUGUI NeedToDo;

    [Header("PauseGame")]
    public GameObject PauseMenu;
    private bool isPause;

    private Door DoorInterect;
    private Ghost GhostHit;
    private CrossCheck CrossUse;
    private bool Holddown;

    [Header("AllEvent")]
    public UnityEvent LightOutEvent;
    public UnityEvent GetKey;
    public UnityEvent GhostEvent;
    public UnityEvent EventCloseDoor;
    public UnityEvent Radio;

    private void Start()
    {       
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        Ray r = new Ray(RH.position, RH.forward);

        #region Attack
        if (Attack)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (curHpCross == 3) CorssAni.SetTrigger("AttackCorss");
                if (curHpCross == 2) CorssAni.SetTrigger("AttackCorss2");
                if (curHpCross == 1) CorssAni.SetTrigger("AttackCorss3");

                HitAudio.clip = HitWindSound;
                HitAudio.Play();
                Holddown = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (curHpCross == 3) CorssAni.SetTrigger("NotAttack");
                if (curHpCross == 2) CorssAni.SetTrigger("NotAttack2");
                if (curHpCross == 1) CorssAni.SetTrigger("NotAttack3");
                Holddown = false;
            }
        } else Holddown = false;
        if (Holddown)
        {
            if (Physics.Raycast(r, out RaycastHit hitinfo, 5))
            {
                if (hitinfo.collider.gameObject.tag == "Ghost")
                {
                    if (curHpCross != 1)
                    {
                        crossruin = true;
                        HitAudio.clip = HitGhostSound;
                        GhostHit = hitinfo.collider.gameObject.GetComponent<Ghost>();
                        GhostHit.PlayerHitGhost();
                    }
                    //StartCoroutine(AttackReset());
                }
            }
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause) PauseGame();
            else ResumeGame();
        }

        #region Interect evnet
        Ray Interect = new Ray(pickUPPoint.position, pickUPPoint.forward);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(Interect, out RaycastHit hitInterect, Pickrange))
            {
                //  print(hitInterect.collider.gameObject);
                if (hitInterect.collider.gameObject.tag == "Lantern")
                {
                    Light.SetActive(true);
                    LanternAni.SetTrigger("LightUp");
                    LightOn = true;
                    LightOnHand = true;
                    pointLight.SetActive(true);
                    Destroy(hitInterect.collider.gameObject);
                }
                if (hitInterect.collider.gameObject.tag == "Door")
                {
                    DoorInterect = hitInterect.collider.gameObject.GetComponent<Door>();
                    DoorInterect.DoorAni();

                }
                if (hitInterect.collider.gameObject.tag == "Key")
                {
                    
                    GetKey.Invoke();
                    StartCoroutine(GhostSpawnDelay());
                    Destroy(hitInterect.collider.gameObject);
                }
                if (hitInterect.collider.gameObject.tag == "Radio")
                {
                    Radio.Invoke();
                }

            }
        }
        #endregion

        #region Show what can interect
        if (Physics.Raycast(Interect, out RaycastHit hitevent, Pickrange))
        {
            if (hitevent.collider.tag == "GhostEvent")
            {
                GhostEvent.Invoke();
                Destroy(hitevent.collider.gameObject);
            }
            if (hitevent.collider.gameObject.tag == "Key")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "Key";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Radio")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "Radio";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Door")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "Door";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Lantern")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "Lantern";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Cross")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "Cross";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Doll")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "Doll";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Cloth")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "Cloth";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "DeskWorkShop")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "DeskWorkShop";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Basket")
            {
                ItemText.SetActive(true);
                ItemName.text = "Basket";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "FrontDoor")
            {
                ItemText.SetActive(true);
                E.SetActive(true);
                ItemName.text = "FrontDoor";
                InterectItem = true;
            }
            else
            {
                InterectItem = false;
                E.SetActive(false);
                ItemText.SetActive(false);
            }
        }
        else
        {
            InterectItem = false;
        }

        if (InterectItem)
        {
            InterectAble.sprite = InterectSprite;
        }
        else
        {
            ItemText.SetActive(false);
            InterectAble.sprite = pointSprite;
        }
        #endregion


        #region Pick drop item
        if (CanDropItem)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (CrossOnHand)
                {
                    if (curHpCross == 3)
                    {
                        CorssR.SetActive(false);
                        Attack = false;
                        DropCross();
                    }
                    if(curHpCross == 2)
                    {
                        CorssR.SetActive(false);
                        Attack = false;
                        DropCross2();
                    }
                    if(curHpCross == 1)
                    {
                        CorssR.SetActive(false);
                        Attack = false;
                        DropCross1();
                    }
                }
                if (DollOnHand)
                {
                    DollR.SetActive(false);
                    DropDoll();
                }
                if (ClothOnHand)
                {
                    ClothR.SetActive(false);
                    DropCloth();
                }

                CanDropItem = false;
                CrossOnHand = false;
                DollOnHand = false;
                ClothOnHand = false;
            }


        }
        else if (!CanDropItem) 
        {
            Ray RPick = new Ray(pickUPPoint.position, pickUPPoint.forward);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(RPick, out RaycastHit hitInfo, Pickrange))
                {
                    if (hitInfo.collider.gameObject.tag == "Cross")
                    {
                        CrossUse = hitInfo.collider.gameObject.GetComponent<CrossCheck>();
                        curHpCross = CrossUse.curHp;
                        CrossOnHand = true;
                        Attack = true;
                        CorssR.SetActive(true);
                        CanDropItem = true;
                        if(curHpCross == 3) CorssAni.SetTrigger("OnHand");
                        if(curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                        if(curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                        Destroy(hitInfo.collider.gameObject);
                        // print("Cross");
                    }
                    if (hitInfo.collider.gameObject.tag == "Doll")
                    {
                        // DollAni.SetTrigger("OnHand");
                        DollOnHand = true;
                        DollR.SetActive(true);
                        CanDropItem = true;
                        Destroy(hitInfo.collider.gameObject);
                        //print("doll");
                    }
                    if (hitInfo.collider.gameObject.tag == "Cloth")
                    {
                        ClothOnHand = true;
                        ClothR.SetActive(true);
                        CanDropItem = true;
                        Destroy(hitInfo.collider.gameObject);

                    }
                
                }
            }
        }
        #endregion

        #region LightUP

        Ray LPick = new Ray(pickUPPoint.position, pickUPPoint.forward);
        if (Input.GetKeyDown(KeyCode.E))
            if (Physics.Raycast(LPick, out RaycastHit hitInfo, Pickrange))
            {
                if (hitInfo.collider.gameObject.tag == "Lantern")
                {
                    Light.SetActive(true);
                    LanternAni.SetTrigger("LightUp");
                    LightOn = true;
                    LightOnHand = true;
                    pointLight.SetActive(true);
                    Destroy(hitInfo.collider.gameObject);
                }
            }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (LightOnHand)
            {
                if (!LightOn)
                {
                    Light.gameObject.SetActive(true);
                    LanternAni.SetTrigger("LightUp");
                    LightOn = true;
                    pointLight.SetActive(true);
                }
                else
                {
                    Light.gameObject.SetActive(false);
                    LightOn = false;
                    pointLight.SetActive(false);
                }
            }
        }
        #endregion


        if (dialogCheck)
        {
            StopAllCoroutines();
            StartCoroutine(DelayDialog());
            dialogCheck = false;
        }

    }


    #region Drop prefeb item
    void Shoot()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        Attacking(RH);

    }
    void DropCross()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingCross(RH);

    }
    void DropCross2()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingCross2(RH);

    }
    void DropCross1()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingCross1(RH);

    }
    void DropDoll()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingDoll(RH);

    }
    void DropCloth()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingCloth(RH);

    }

    void Attacking(Transform FirePoint)
    {
        var projectileOBj = Instantiate(projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * ShootSpeed;
    }

    void DropingCross(Transform FirePoint)
    {
        var projectileOBj = Instantiate(CrossD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    void DropingCross2(Transform FirePoint)
    {
        var projectileOBj = Instantiate(CrossD2, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    void DropingCross1(Transform FirePoint)
    {
        var projectileOBj = Instantiate(CrossD1, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }

    void DropingDoll(Transform FirePoint)
    {
        var projectileOBj = Instantiate(DollD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    void DropingCloth(Transform FirePoint)
    {
        var projectileOBj = Instantiate(ClothD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }

#endregion

    public void StopAttack()
    {
        Attack = false;
    }
    public void CanAttack()
    {
        if(CrossOnHand) 
        Attack = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            //print("GhostHit");
            if (CrossOnHand)
            {
                CorssR.SetActive(false);
                Attack = false;
                CanDropItem = false;
                CrossOnHand = false;
                DropCross();
            }
            if (DollOnHand)
            {
                DollR.SetActive(false);
                CanDropItem = false;
                DollOnHand = false;
                DropDoll();
            }
            if (ClothOnHand)
            {
                ClothR.SetActive(false);
                CanDropItem = false;
                ClothOnHand = false;
                DropCloth();
            }
        }
    }
    

    #region Trigger enter
    public void OnTriggerEnter(Collider other)
    {
        #region Dialogue event
        if (other.gameObject.tag == "LightOutEvent")
        {
            LightOutEvent.Invoke();
            HitAudio.clip = LightOutLetsGooo;
            HitAudio.Play();    
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "CloseDoorEvent")
        {
            EventCloseDoor.Invoke();
            Destroy(other.gameObject);
        }
        if ((other.gameObject.tag == "Tutorial"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "PickItem1")
        {
            Textdialogue.text = Dialogue[0];
            dialogCheck = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "GoRadioPlay")
        {
            Textdialogue.text = Dialogue[1];
            dialogCheck = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "LightOutEvent")
        {
            Textdialogue.text = Dialogue[3];               
            StartCoroutine(LightOutDelay());
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "CloseDoorEvent")
        {
            Textdialogue.text = Dialogue[4];
            dialogCheck = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "BeQuiet")
        {
            Textdialogue.text = Dialogue[5];
            dialogCheck = true;
            Destroy(other.gameObject);
        }

        #endregion


        #region Enter room

        if (other.gameObject.tag == "BedRoom1")
        {
            TextYouhere.text = "Bed room 1";
        }
        if (other.gameObject.tag == "BedRoom2")
        {
            TextYouhere.text = "Bed room 2";
        }
        if (other.gameObject.tag == "BedRoom3")
        {
            TextYouhere.text = "Bed room 3";
        }
        if (other.gameObject.tag == "WalkWayF2")
        {
            TextYouhere.text = "Walk way F2";
        }
        if (other.gameObject.tag == "WalkWayF1")
        {
            TextYouhere.text = "Walk way F1";
        }
        if (other.gameObject.tag == "ToiletF2")
        {
            TextYouhere.text = "Toilet F2";
        }
        if (other.gameObject.tag == "FrontDoor")
        {
            TextYouhere.text = "Front door";
        }
        if (other.gameObject.tag == "Library")
        {
            TextYouhere.text = "Library";
        }
        if (other.gameObject.tag == "DollMakerRoom")
        {
            TextYouhere.text = "Doll maker room";
        }
        if (other.gameObject.tag == "DinnerRoom")
        {
            TextYouhere.text = "Dinner room";
        }
        if (other.gameObject.tag == "Kitchen")
        {
            TextYouhere.text = "Kitchen";
        }
        #endregion

    }

    public void PickItem2Event()
    {
        Textdialogue.text = Dialogue[1];
        StartCoroutine(PickItem2Delay());
    }

    IEnumerator DelayDialog()
    {
        CanvaDialog.SetActive(true);
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
        dialogCheck = false;
    }

    IEnumerator PickItem2Delay()
    {
        CanvaDialog.SetActive(true) ;
        yield return new WaitForSeconds(5);
        Textdialogue.text = Dialogue[2];
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
    }
    IEnumerator LightOutDelay()
    {
        CanvaDialog.SetActive(true);
        yield return new WaitForSeconds(5);
        Textdialogue.text = Dialogue[1];
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
    }
    IEnumerator GhostSpawnDelay()
    {
        CanvaDialog.SetActive(true);
        yield return new WaitForSeconds(5);
        Textdialogue.text = Dialogue[1];
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
    }
    #endregion



    public void CrossRuin()
    {
        if (crossruin)
        {
            curHpCross--;
            CrossUse.curHp = curHpCross;
            if(curHpCross == 0) curHpCross = 1;
            crossruin = false;
        }
    }


    #region Pause game
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        isPause = true;
        Attack = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        isPause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (CrossOnHand) Attack = true;
    }
    #endregion
}
