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
    public GameObject Inventory, BlackCross, BlackDoll, BlackCloth;
    public TextMeshProUGUI DollTotel,ClothTotel;
    public CanPlayMini1 takeFinishDoll;

    [Header("Item Change")]
    public int ItemSelect = 0;
    private int Itemhave,Dollhave,Clothhave;
    private bool showCross,showDoll,showCloth,setTriggerCross;


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
    public TextMeshProUGUI ItemName;
    public int StoryNow;
    private bool dialogCheck, InterectItem;

    [Header("TextShow")]
    public TextMeshProUGUI TextYouhere;
    public TextMeshProUGUI NeedToDo;
    public GameObject Tutext1;
    public GameObject Tutext2;
    public GameObject Tutext3;
    public TextMeshProUGUI tutorialText1;
    public TextMeshProUGUI tutorialText2;
    public TextMeshProUGUI tutorialText3;

    [Header("PauseGame")]
    public GameObject PauseMenu;
    public GameObject EndGame;
    private bool isPause, Working;

    private Door DoorInterect;
    private Ghost GhostHit;
    private Event DoEvent;
    private CrossCheck CrossUse;
    private bool Holddown,LightOut,DialogueStory,EndD1;


    [Header("AllEvent")]
    public UnityEvent CheckFrontDoor;
    public UnityEvent CanExplore;
    public UnityEvent LightOutEvent;
    public UnityEvent BreakerCheck;
    public UnityEvent GetKey;
    public UnityEvent GhostEvent1;
    public UnityEvent GhostEvent2;
    public UnityEvent GhostEvent3;
    public UnityEvent GhostSpawn;
    public UnityEvent GetFinshDoll;
    public UnityEvent EventCloseDoor;
    public UnityEvent Radio;

    private void Start()
    {       

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
            if (!EndD1)
            {
                if (!isPause) PauseGame();
                else ResumeGame();
            }
            
        }

        #region Item Change

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (ItemSelect >= 2)
                ItemSelect = 0;
            else
                ItemSelect++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (ItemSelect <= 0)
                ItemSelect = 2;
            else
                ItemSelect--;
        }

        #endregion

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
                    if(StoryNow == 2)
                    {
                        if (DoorInterect.Lock == true)
                        {
                            Textdialogue.text = "Need to check front door first.";
                            dialogCheck = true;
                        }
                    }
                    if(StoryNow >= 5)
                    {
                        if (DoorInterect.Lock == true)
                        {
                            Textdialogue.text = "Need to find the key.";
                            dialogCheck = true;
                        }
                    }
                }
                if (hitInterect.collider.gameObject.tag == "Key")
                {
                    if (StoryNow < 5)
                    {

                        Textdialogue.text = "Key for back door.";
                        dialogCheck = true;
                    }
                    if( StoryNow == 5)
                    {
                        Textdialogue.text = "(Feel weird. Maybe THAT THING appears. Let's take the cross for sure.)";
                        dialogCheck = true;
                        GetKey.Invoke();
                        Destroy(hitInterect.collider.gameObject);
                    }
                }
                if (hitInterect.collider.gameObject.tag == "Radio")
                {
                    Radio.Invoke();
                }
                if (hitInterect.collider.gameObject.tag == "Bed")
                {
                    if (StoryNow < 6)
                    {
                        Textdialogue.text = "I can't sleep now";
                        dialogCheck = true;
                    }
                    if(StoryNow == 6)
                    {
                        Textdialogue.text = "**Amazing go to sleep cutscene play** ZZZ..";
                        dialogCheck = true;                    
                        EndD1 = true;
                    }
                }
                if (hitInterect.collider.gameObject.tag == "LightSwitch")
                {
                    DoEvent = hitInterect.collider.gameObject.GetComponent<Event>();
                    if(!LightOut)
                    DoEvent.TurnOnLight();
                    else
                        DoEvent.GhostLightOut();
                }
                if (hitInterect.collider.gameObject.tag == "Breaker")
                {
                    if (StoryNow == 5)
                    {
                        LightOut = false;
                        BreakerCheck.Invoke();
                    }
                }

            }
        }
        #endregion

        #region Show what can interect
        if (Physics.Raycast(Interect, out RaycastHit hitevent, Pickrange))
        {
            if (hitevent.collider.tag == "GhostEvent")
            {
                GhostEvent1.Invoke();
                Destroy(hitevent.collider.gameObject);
            }
            if (hitevent.collider.tag == "GhostEvent2")
            {
                GhostEvent2.Invoke();
                Destroy(hitevent.collider.gameObject);
            }
            if (hitevent.collider.tag == "GhostEvent3")
            {
                GhostEvent3.Invoke();
                Textdialogue.text = "(Did she just take my doll!?)";
                dialogCheck = true;
                Destroy(hitevent.collider.gameObject);
            }
            if (hitevent.collider.gameObject.tag == "Key")
            {
                ItemText.SetActive(true);
                ItemName.text = "Key  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Radio")
            {
                ItemText.SetActive(true);
                ItemName.text = "Radio  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Door")
            {
                ItemText.SetActive(true);
                ItemName.text = "Door  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Lantern")
            {
                ItemText.SetActive(true);
                ItemName.text = "Lantern  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Cross")
            {
                ItemText.SetActive(true);
                ItemName.text = "Cross  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Doll")
            {
                ItemText.SetActive(true);
                ItemName.text = "Doll  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "FinishDoll")
            {
                ItemText.SetActive(true);
                ItemName.text = "FinshDoll  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Cloth")
            {
                ItemText.SetActive(true);
                ItemName.text = "Cloth  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "DeskWorkShop")
            {
                ItemText.SetActive(true);
                ItemName.text = "DeskWorkShop  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Basket")
            {
                ItemText.SetActive(true);
                ItemName.text = "Basket";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "ForntDoor")
            {
                ItemText.SetActive(true);
                ItemName.text = "FrontDoor  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "LightSwitch")
            {
                ItemText.SetActive(true);
                ItemName.text = "LightSwitch  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Bed")
            {
                ItemText.SetActive(true);
                ItemName.text = "Bed  [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Breaker")
            {
                ItemText.SetActive(true);
                ItemName.text = "Breaker [E]";
                InterectItem = true;
            }
            else
            {
                InterectItem = false;
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

        if(Itemhave != 0)
        {
            Tutext3.SetActive(true);
            tutorialText3.text = "Select item [Scroll mouse]";
        }
        else
        {
            Tutext3.SetActive(false);
        }
        if (!Working)
        {
            //Drop item
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (CrossOnHand && showCross)
                {
                    if (curHpCross == 3)
                    {
                        CorssR.SetActive(false);
                        Attack = false;
                        DropCross();
                    }
                    if (curHpCross == 2)
                    {
                        CorssR.SetActive(false);
                        Attack = false;
                        DropCross2();
                    }
                    if (curHpCross == 1)
                    {
                        CorssR.SetActive(false);
                        Attack = false;
                        DropCross1();
                    }
                    Itemhave--;
                    CrossOnHand = false;
                    Tutext1.SetActive(false); Tutext2.SetActive(false);
                }

                if (Dollhave != 0)
                {
                    if (DollOnHand && showDoll)
                    {
                        DropDoll();
                        Itemhave--;
                        Dollhave--;
                        DollTotel.text = "x" + Dollhave;
                    }
                }

                if (Clothhave != 0)
                {
                    if (ClothOnHand && showCloth)
                    {
                        DropCloth();
                        Itemhave--;
                        Clothhave--;
                        ClothTotel.text = "x" + Clothhave;
                    }
                }

            }

            //Show Cross on hand
            if (ItemSelect == 0 && CrossOnHand)
            {
                CorssR.SetActive(true);
                showCross = true;
                if (!isPause)
                    Attack = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Attack [Hold left]";
                if (setTriggerCross)
                {
                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    setTriggerCross = false;
                    BlackCross.SetActive(false);
                }

            }
            else if (ItemSelect == 0 && !CrossOnHand)
            {
                CorssR.SetActive(false);
                showCross = false;
                Tutext1.SetActive(false);
                Attack = false;
                BlackCross.SetActive(true);
            }
            else
            {
                CorssR.SetActive(false);
                showCross = false;
                Attack = false;
                setTriggerCross = true;
                BlackCross.SetActive(true);
            }

            //Show Doll on hand
            if (ItemSelect == 1 && DollOnHand)
            {
                DollR.SetActive(true);
                showDoll = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                BlackDoll.SetActive(false);
            }
            else if (ItemSelect == 1 && !DollOnHand)
            {
                showDoll = false;
                Tutext1.SetActive(false);
                DollR.SetActive(false);
                BlackDoll.SetActive(true);
            }
            else
            {
                showDoll = false;
                DollR.SetActive(false);
                BlackDoll.SetActive(true);
            }

            // Show cloth on hand
            if (ItemSelect == 2 && ClothOnHand)
            {
                ClothR.SetActive(true);
                showCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                BlackCloth.SetActive(false);
            }
            else if (ItemSelect == 2 && !ClothOnHand)
            {
                Tutext1.SetActive(false);
                ClothR.SetActive(false);
                showCloth = false;
                BlackCloth.SetActive(true);
            }
            else
            {
                ClothR.SetActive(false);
                showCloth = false;
                BlackCloth.SetActive(true);
            }

            // if don't have item
            if (Itemhave == 0)
            {
                Tutext1.SetActive(false);
            }

            if (Clothhave == 0)
            {
                ClothR.SetActive(false);
                ClothOnHand = false;
            }
            if (Dollhave == 0)
            {
                DollR.SetActive(false);
                DollOnHand = false;
            }

            // Pick item
            Ray RPick = new Ray(pickUPPoint.position, pickUPPoint.forward);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(RPick, out RaycastHit hitInfo, Pickrange))
                {
                    if (Itemhave != 3)
                    {
                        if (!CrossOnHand)
                        {
                            if (hitInfo.collider.gameObject.tag == "Cross")
                            {
                                if (StoryNow >= 5)
                                {
                                    CrossUse = hitInfo.collider.gameObject.GetComponent<CrossCheck>();
                                    curHpCross = CrossUse.curHp;
                                    CrossOnHand = true;
                                    Attack = true;
                                    CorssR.SetActive(true);
                                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                                    Destroy(hitInfo.collider.gameObject);
                                    // print("Cross");
                                    DollR.SetActive(false);
                                    ClothR.SetActive(false);
                                    Itemhave++;
                                    ItemSelect = 0;
                                }
                                else
                                {
                                    Textdialogue.text = "(Not this time. Can only be used 2 times and only be held 1 on hand)";
                                    dialogCheck = true;
                                }
                            }
                        }
                        if (Dollhave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "Doll")
                            {
                                // DollAni.SetTrigger("OnHand");
                                DollOnHand = true;
                                DollR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                //print("doll");
                                CorssR.SetActive(false);
                                ClothR.SetActive(false);
                                Itemhave++;
                                Dollhave++;
                                ItemSelect = 1;
                                Inventory.SetActive(true);
                                DollTotel.text = "x" + Dollhave;
                            }
                        }
                        if (Clothhave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "Cloth")
                            {
                                ClothOnHand = true;
                                ClothR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                DollR.SetActive(false);
                                CorssR.SetActive(false);
                                Itemhave++;
                                Clothhave++;
                                ItemSelect = 2;
                                Inventory.SetActive(true);
                                ClothTotel.text = "x" + Clothhave;
                            }
                        }                       
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
                    Tutext2.SetActive(true);
                    tutorialText2.text = "Light off [F]";
                    Light.SetActive(true);
                    LanternAni.SetTrigger("LightUp");
                    LightOn = true;
                    LightOnHand = true;
                    pointLight.SetActive(true);
                    Destroy(hitInfo.collider.gameObject);
                }

                if (hitInfo.collider.gameObject.tag == "FinishDoll")
                {
                    GetFinshDoll.Invoke();
                    takeFinishDoll.AddTotalDoll();
                    Destroy(hitInfo.collider.gameObject);
                }
            }


        if (Input.GetKeyDown(KeyCode.F))
        {
            if (LightOnHand)
            {
                if (!LightOn)
                {
                    tutorialText2.text = "Light off [F]";
                    Light.gameObject.SetActive(true);
                    LanternAni.SetTrigger("LightUp");
                    LightOn = true;
                    pointLight.SetActive(true);
                }
                else
                {
                    tutorialText2.text = "Light on [F]";
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


        #region Need to do next
        if (StoryNow == 1) NeedToDo.text = "Explore bed room";
        if (StoryNow == 2)
        {
            NeedToDo.text = "Go to check front door";
            CheckFrontDoor.Invoke();
        }
        if (StoryNow == 3)
        {
            NeedToDo.text = "Go to back door or explore house";
            CanExplore.Invoke();
        }
        if (StoryNow == 4) NeedToDo.text = "Make doll at workshop";
        if (StoryNow == 5)
        {
            NeedToDo.text = "Find doll and cloth to finish job";
            if (DialogueStory)
            {
                Textdialogue.text = "(Ah.. out of tools. let's check the storage room)";
                dialogCheck = true;
                DialogueStory = false;
            }
        }
        if (StoryNow == 6)
        {
            if (DialogueStory)
            {
                NeedToDo.text = "Exorcise ghosts and go to bed";
                Textdialogue.text = "(That is for today. Now I need to do something to sleep in peace.)";
                dialogCheck = true;
                DialogueStory = false;
            }
        }
        #endregion

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
            LightOut = true;
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
            Textdialogue.text = "What a mass and now light out!?";
            dialogCheck = true;
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
            TextYouhere.text = "My Room";
        }
        if (other.gameObject.tag == "BedRoom2")
        {
            TextYouhere.text = "Bed room 2";
        }
        if (other.gameObject.tag == "BedRoom3")
        {
            TextYouhere.text = "Bed room 1";
        }
        if (other.gameObject.tag == "WalkWayF2")
        {
            TextYouhere.text = "Second Floor";
        }
        if (other.gameObject.tag == "WalkWayF1")
        {
            TextYouhere.text = "First Floor";
        }
        if (other.gameObject.tag == "ToiletF2")
        {
            TextYouhere.text = "Bath Room";
        }
        if (other.gameObject.tag == "FrontDoor")
        {
            TextYouhere.text = "Front Door";
        }
        if (other.gameObject.tag == "Library")
        {
            TextYouhere.text = "Library";
        }
        if (other.gameObject.tag == "DollMakerRoom")
        {
            TextYouhere.text = "Sewing Room";
        }
        if (other.gameObject.tag == "DinnerRoom")
        {
            TextYouhere.text = "Dining Room";
        }
        if (other.gameObject.tag == "Kitchen")
        {
            TextYouhere.text = "Kitchen";
        }
        if (other.gameObject.tag == "Storage")
        {
            TextYouhere.text = "Storage Room";
        }
        #endregion


        #region Story Text

        if (other.gameObject.tag == "Story")
        {
            Destroy(other.gameObject);
            StoryNow++;
            DialogueStory = true;
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
        if (EndD1)
        {
            EndGame.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
            Attack = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator PickItem2Delay()
    {
        CanvaDialog.SetActive(true) ;
        yield return new WaitForSeconds(5);
        Textdialogue.text = Dialogue[2];
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
    }

    #endregion

    public void WorkShopview()
    {
        Working = true;
    }
    public void QuitWorkShop()
    {
        Working = false;
    }

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
