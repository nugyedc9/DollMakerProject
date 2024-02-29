using player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class PlayerAttack : MonoBehaviour 
{
    public InventoryManager inventoryManager;
    public PlayerPickUpItem playerPickUpItem;
    [SerializeField] bool attack;
    public bool Attack { get { return attack; } set { attack = value; } }
    public float DropSpeed;
    public Camera FpsCam;
    public GameObject projectile;
    public Transform RH;
    public Transform pickUPPoint;
    public float InterectRange;
    public bool  LightOn, CanDropItem, CrossOnHand, DollOnHand,
        ClothOnHand, ScissorOnHand, LightOnHand,
        RedClothOnHand, BlueClothOnHand, GreenClothOnHand, YellowClothOnHand
        ;

    public float Pickrange;
    private Vector3 destination;
    public PlayerChangeCam changeCam;

    [Header("Item Change")]
    public int ItemSelect = 0;
    public int Itemhave,Dollhave,Clothhave, Crosshave, ScissorHave,
        RedClothHave, GreenClothHave, BlueClothHave, YellowClothHave;



    [Header("Item On Hand")]
    public GameObject CorssR;
    public GameObject HolyLight;
    public Animator CorssAni;
    public GameObject DollR;
    public GameObject ClothR, ScissorR,
        RedClothR, BlueClothR, GreenClothR, YellowClothR;
    public GameObject[] itemInventory1, itemInventory2, itemInventory3;
    public GameObject Inventory, InvPoint1, InvPoint2, InvPoint3;
    public CanPlayMini1 takeFinishDoll;


    private bool showCross,showDoll,showCloth, showScissor,
        ShowRedCloth, ShowGreenCloth, ShowBlueCloth, ShowYellowCloth,
        setTriggerCross, box1 , box2, box3;

    [Header("Item Drop")]
    public GameObject CrossD;
    public GameObject CrossD2;
    public GameObject CrossD1;
    public GameObject DollD;
    public GameObject ClothD, ScissorD,
        RedClothD, BlueClothD, GreenClothD, YellowClothD
        ;
    private bool CrossInv1, CrossInv2, CrossInv3,
        DollInv1, DollInv2, DollInv3,
        ClothInv1, ClothInv2, ClothInv3
        , ScissorInv1, ScissorInv2, ScissorInv3,
        RedCloInv1, RedCloInv2, RedCloInv3,
        GreenCloInv1, GreenCloInv2, GreenCloInv3,
        BlueCloInv1, BlueCloInv2, BlueCloInv3,
        YellowCloInv1, YellowCloInv2, YellowCloInv3
        ;

    [Header("CrossAction")]
    [SerializeField] private float CurHpCross;
    public float curHpCross { get { return CurHpCross; } set { CurHpCross = value; } }
    private float CrossTimer;
    private bool crossruin;

    [Header("GetDesignCloth")]
    public DesignSelect designSelect;
    [SerializeField] int designNum;
    public int DesignNum { get { return designNum; } set {  designNum = value; } }


    [Header("PLayerLight")]
    public GameObject Light;
    public GameObject pointLight;
    public Animator LanternAni;

    [Header("Hp player")]
    public PlayerHp HpPlayer;
    private bool OpenEye;


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
    public Animator NeedToDoAnimate;

    [Header("Tutorial game")]
    public GameObject TStaetGame;
    public GameObject TWhatToDO;
    public GameObject THowToDoll;
    public GameObject TGhostCum;
    public GameObject THowToCross;
    public GameObject THowToHeal;

    [Header("Map")]
    public GameObject MapCanva;
    private bool MapOn;

    [Header("PauseGame")]
    public GameObject PauseMenu;
    public GameObject EndGame;
    private bool isPause, Working;

    [Header("Audio")]
    public AudioClip HitGhostSound;
    public AudioClip HitWindSound;
    public AudioSource HitAudio;
    public AudioClip LightOutLetsGooo;
    public AudioSource Ambience;
    public AudioClip AfterJumpGhost;
    public AudioSource InterectSound;

    [Header("PickItemSound")]
    public AudioClip LanternPickSound;
    public AudioClip CrossPickSound;
    public AudioClip DollPickSound;
    public AudioClip ClothPickSound;
    public AudioClip DropCrossSound;
    public AudioClip DropDollSound;
    public AudioClip DropClothSound;
    public AudioClip FinishDollPick;
    public AudioClip KeyPickSound;
    public AudioClip LightClickSound;
    public AudioClip BreakerClickSound;


    private Door DoorInterect;
    private GhostStateManager GhostHit;
    private Event DoEvent;
    private CrossCheck CrossUse;
    private bool Holddown,LightOut,DialogueStory,EndD1,CloseTurial, firstPickCross, GhostEx, FlashLightGet;


    [Header("AllEvent")]
    public UnityEvent CheckFrontDoor;
    public UnityEvent CanExplore;
    public UnityEvent Doorclose;
    public UnityEvent LightOutEvent;
    public UnityEvent BreakerCheck;
    public UnityEvent GetKey;
    public UnityEvent GhostEvent1;
    public UnityEvent GhostEvent12;
    public UnityEvent GhostEvent2;
    public UnityEvent GhostEvent21;
    public UnityEvent GhostEvent3;
    public UnityEvent GhostSpawn;
    public UnityEvent GetFinshDoll;
    public UnityEvent EventCloseDoor;
    public UnityEvent Radio;
    public UnityEvent EndgameCloseSound;

    private void Start()
    {       
            Light.SetActive(false);
          pointLight.SetActive(false);
    }

    void Update()
    {
        Ray r = new Ray(RH.position, RH.forward);

        if (Physics.Raycast(r, out RaycastHit hitCross, Pickrange))
        {
            if (hitCross.collider.gameObject.tag == "Cross")
            {
                if (Crosshave != 1)
                {
                    CrossUse = hitCross.collider.gameObject.GetComponent<CrossCheck>();
                }
            }
        }
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
                if (Physics.Raycast(r, out RaycastHit hitinfo, 5))
                {
                    if (hitinfo.collider.gameObject.tag == "Ghost")
                    {
                        if (curHpCross != 1)
                        {
                            GhostHit = hitinfo.collider.gameObject.GetComponent<GhostStateManager>();
                            GhostHit.Playerhit();
                            curHpCross--;
                            if (curHpCross == 2) CorssAni.SetTrigger("HitGhost");
                            if (curHpCross == 1) CorssAni.SetTrigger("HitGhost2");
                            CrossTimer = 4.5f;
                            crossruin = true;
                            HitAudio.clip = HitGhostSound;
                            HitAudio.Play();                           
                        }
                        //StartCoroutine(AttackReset());
                    }
                    else HolyLight.SetActive(false);
                }
            }
        }



        if(CrossTimer > 0)
        {
            CrossTimer -= Time.deltaTime;
            if (CrossTimer < 3.6 && CrossTimer > 2.6f)
            {
                HolyLight.SetActive(true);
            }
            else if (CrossTimer < 0)
            {
                HolyLight.SetActive(false);
                if (curHpCross == 1)
                {
                    inventoryManager.GetSelectedItem(true);
                }
            }
        }


        #endregion

        #region Close Cut roll Cloth
        /*#region CutRollCloth
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(r, out RaycastHit hitinfo, 2))
            {
                if(hitinfo.collider.gameObject.tag == "RollCloth")
                {
                    if (ScissorOnHand)
                    {
                        if (Itemhave != 3)
                        {
                            if (Clothhave != 3)
                            {
                                ClothOnHand = true;
                                DollOnHand = false;
                                CrossOnHand = false;
                                ScissorOnHand = false;
                                showCloth = true;

                                InterectSound.clip = ClothPickSound;
                                InterectSound.Play();

                                ClothR.SetActive(true);
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";
                                DollR.SetActive(false);
                                CorssR.SetActive(false);
                                ScissorR.SetActive(false);
                                Itemhave++;
                                Clothhave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[2].SetActive(true);
                                    ClothInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[2].SetActive(true);
                                    ClothInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[2].SetActive(true);
                                    ClothInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }

                            }
                        }
                    }
                }
            }
        }
        #endregion*/
        #endregion

        #region Map pause tutorial

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!EndD1 || CloseTurial)
            {
                if (!isPause) PauseGame();
                else ResumeGame();
            }            
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if(!MapOn)OpenMap();
            else CloseMap();
        }
        if(CloseTurial)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Attack = false;
        }

        #endregion

        #region Close Item change
    /*    #region Item Change

        #region Key1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemSelect = 0;
            if (ItemSelect == 0)
            {
                InvPoint1.SetActive(true);
                InvPoint2.SetActive(false);
                InvPoint3.SetActive(false);
            }
            if (CrossInv1)
            {
                CorssR.SetActive(true);
                CrossOnHand = true;
                showCross = true;
                if (!isPause)
                    Attack = true;
                Tutext1.SetActive(true);
                Tutext2.SetActive(true);
                tutorialText1.text = "Attack [Hold left]";
                tutorialText2.text = "Drop [G]";
                if (setTriggerCross)
                {
                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    setTriggerCross = false;
                }
                if (curHpCross == 3) itemInventory1[0].SetActive(true); else itemInventory1[0].SetActive(false);
                if (curHpCross == 2) itemInventory1[3].SetActive(true); else itemInventory1[3].SetActive(false);
                if (curHpCross == 1) itemInventory1[4].SetActive(true); else itemInventory1[4].SetActive(false);
            }
            else
            {
                CrossOnHand = false;
                CorssR.SetActive(false);
                showCross = false;
                Attack = false;
                if (!setTriggerCross)
                {
                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    setTriggerCross = true;
                }
            }
            

            if (DollInv1)
            {
                DollOnHand = true;
                DollR.SetActive(true);
                showDoll = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                DollOnHand = false;
                showDoll = false;
                DollR.SetActive(false);
            }

            if (ClothInv1)
            {
                ClothOnHand = true;
                ClothR.SetActive(true);
                showCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                ClothOnHand = false;
                ClothR.SetActive(false);
                showCloth = false;
            }
            if (ScissorInv1)
            {
                ScissorOnHand = true;
                ScissorR.SetActive(true);
                showScissor = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                ScissorOnHand = false;
                ScissorR.SetActive(false);
                showScissor = false;
            }
            if (RedCloInv1)
            {
                RedClothOnHand = true;
                RedClothR.SetActive(true);
                ShowRedCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                RedClothOnHand = false;
                RedClothR.SetActive(false);
                ShowRedCloth = false;
            }
            if (GreenCloInv1)
            {
                GreenClothOnHand = true;
                GreenClothR.SetActive(true);
                ShowGreenCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                GreenClothOnHand = false;
                GreenClothR.SetActive(false);
                ShowGreenCloth = false;
            }
            if (BlueCloInv1)
            {
                BlueClothOnHand = true;
                BlueClothR.SetActive(true);
                ShowBlueCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                BlueClothOnHand = false;
                BlueClothR.SetActive(false);
                ShowBlueCloth = false;
            }
            if (YellowCloInv1)
            {
                YellowClothOnHand = true;
                YellowClothR.SetActive(true);
                ShowYellowCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                YellowClothOnHand = false;
                YellowClothR.SetActive(false);
                ShowYellowCloth = false;
            }

            if (!box1)
            {
                Tutext1.SetActive (false);
                Tutext2.SetActive (false); 
            }

        }
        #endregion

        #region Key2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ItemSelect = 1;
            if (ItemSelect == 1)
            {
                InvPoint1.SetActive(false);
                InvPoint2.SetActive(true);
                InvPoint3.SetActive(false);
            }
            if (CrossInv2)
            {
                CrossOnHand = true;
                CorssR.SetActive(true);
                showCross = true;
                if (!isPause)
                    Attack = true;
                Tutext1.SetActive(true);
                Tutext2.SetActive(true);
                tutorialText1.text = "Attack [Hold left]";
                tutorialText2.text = "Drop [G]";
                if (setTriggerCross)
                {
                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    setTriggerCross = false;
                }
                if (curHpCross == 3) itemInventory2[0].SetActive(true); else itemInventory2[0].SetActive(false);
                if (curHpCross == 2) itemInventory2[3].SetActive(true); else itemInventory2[3].SetActive(false);
                if (curHpCross == 1) itemInventory2[4].SetActive(true); else itemInventory2[4].SetActive(false);

            } 
            else
            {
                CrossOnHand = false;
                CorssR.SetActive(false);
                showCross = false;
                Attack = false;
                if (!setTriggerCross)
                {
                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    setTriggerCross = true;
                }
            }
            if (ItemSelect == 1 && !CrossOnHand)
            {
                CorssR.SetActive(false);
                showCross = false;
                Attack = false;
            }
           

            if (DollInv2)
            {
                DollOnHand = true;
                DollR.SetActive(true);
                showDoll = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                DollOnHand = false;
                showDoll = false;
                DollR.SetActive(false);
            }

            if (ClothInv2)
            {
                ClothOnHand =true;
                ClothR.SetActive(true);
                showCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                ClothOnHand = false;
                ClothR.SetActive(false);
                showCloth = false;
            }
            if (ScissorInv2)
            {
                ScissorOnHand = true;
                ScissorR.SetActive(true);
                showScissor = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                ScissorOnHand = false;
                ScissorR.SetActive(false);
                showScissor = false;
            }
            if (RedCloInv2)
            {
                RedClothOnHand = true;
                RedClothR.SetActive(true);
                ShowRedCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                RedClothOnHand = false;
                RedClothR.SetActive(false);
                ShowRedCloth = false;
            }
            if (GreenCloInv2)
            {
                GreenClothOnHand = true;
                GreenClothR.SetActive(true);
                ShowGreenCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                GreenClothOnHand = false;
                GreenClothR.SetActive(false);
                ShowGreenCloth = false;
            }
            if (BlueCloInv2)
            {
                BlueClothOnHand = true;
                BlueClothR.SetActive(true);
                ShowBlueCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                BlueClothOnHand = false;
                BlueClothR.SetActive(false);
                ShowBlueCloth = false;
            }
            if (YellowCloInv2)
            {
                YellowClothOnHand = true;
                YellowClothR.SetActive(true);
                ShowYellowCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                YellowClothOnHand = false;
                YellowClothR.SetActive(false);
                ShowYellowCloth = false;
            }
            if (!box2)
            {
                Tutext1.SetActive(false);
                Tutext2.SetActive(false);
            }

        }
        #endregion

        #region Key3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ItemSelect = 2;
            if (ItemSelect == 2)
            {
                InvPoint1.SetActive(false);
                InvPoint2.SetActive(false);
                InvPoint3.SetActive(true);
            }
            if (CrossInv3)
            {
                CrossOnHand = true;
                CorssR.SetActive(true);
                showCross = true;
                if (!isPause)
                    Attack = true;

                Tutext1.SetActive(true);
                Tutext2.SetActive(true);
                tutorialText1.text = "Attack [Hold left]";
                tutorialText2.text = "Drop [G]";
                if (setTriggerCross)
                {
                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    setTriggerCross = false;
                }

                if (curHpCross == 3) itemInventory3[0].SetActive(true); else itemInventory3[0].SetActive(false);
                if (curHpCross == 2) itemInventory3[3].SetActive(true); else itemInventory3[3].SetActive(false);
                if (curHpCross == 1) itemInventory3[4].SetActive(true); else itemInventory3[4].SetActive(false);
            }
            else
            {
                CrossOnHand = false;
                CorssR.SetActive(false);
                showCross = false;
                Attack = false;
                if (!setTriggerCross)
                {
                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                    setTriggerCross = true;
                }
            }
            

            if (DollInv3)
            {
                DollOnHand = true;
                DollR.SetActive(true);
                showDoll = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                DollOnHand = false;
                showDoll = false;
                DollR.SetActive(false);
            }
            if (ClothInv3)
            {
                ClothOnHand = true;
                ClothR.SetActive(true);
                showCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                ClothOnHand = false;
                ClothR.SetActive(false);
                showCloth = false;
            }
            if (ScissorInv3)
            {
                ScissorOnHand = true;
                ScissorR.SetActive(true);
                showScissor = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                ScissorOnHand = false;
                ScissorR.SetActive(false);
                showScissor = false;
            }
            if (RedCloInv3)
            {
                RedClothOnHand = true;
                RedClothR.SetActive(true);
                ShowRedCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                RedClothOnHand = false;
                RedClothR.SetActive(false);
                ShowRedCloth = false;
            }
            if (GreenCloInv3)
            {
                GreenClothOnHand = true;
                GreenClothR.SetActive(true);
                ShowGreenCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                GreenClothOnHand = false;
                GreenClothR.SetActive(false);
                ShowGreenCloth = false;
            }
            if (BlueCloInv3)
            {
                BlueClothOnHand = true;
                BlueClothR.SetActive(true);
                ShowBlueCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                BlueClothOnHand = false;
                BlueClothR.SetActive(false);
                ShowBlueCloth = false;
            }
            if (YellowCloInv3)
            {
                YellowClothOnHand = true;
                YellowClothR.SetActive(true);
                ShowYellowCloth = true;
                Tutext1.SetActive(true);
                tutorialText1.text = "Drop [G]";
                tutorialText2.text = "";
            }
            else
            {
                YellowClothOnHand = false;
                YellowClothR.SetActive(false);
                ShowYellowCloth = false;
            }
            if (!box3)
            {
                Tutext1.SetActive(false);
                Tutext2.SetActive(false);
            }
        }
        #endregion

        #endregion*/
        #endregion

        #region Interect evnet
        Ray Interect = new Ray(pickUPPoint.position, pickUPPoint.forward);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(Interect, out RaycastHit hitInterect, Pickrange))
            {
                //  print(hitInterect.collider.gameObject);

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
               
      
                if (hitInterect.collider.gameObject.tag == "Bed")
                {
                  
                }
                if (hitInterect.collider.gameObject.tag == "LightSwitch")
                {
                    DoEvent = hitInterect.collider.gameObject.GetComponent<Event>();
                    if (!LightOut)
                    {
                        DoEvent.TurnOnLight();
                        InterectSound.clip = LightClickSound;
                        InterectSound.Play();
                    }
                    else
                    {
                        DoEvent.GhostLightOut();
                        InterectSound.clip = LightClickSound;
                        InterectSound.Play();
                    }
                }
                if (hitInterect.collider.gameObject.tag == "Breaker")
                {
                    if (StoryNow == 5)
                    {
                        LightOut = false;
                        BreakerCheck.Invoke();
                        InterectSound.clip = BreakerClickSound;
                        InterectSound.Play();
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
                Ambience.clip = AfterJumpGhost;
                Ambience.Play();
                Destroy(hitevent.collider.gameObject);
            }
            if (hitevent.collider.tag == "GhostEvent21")
            {
                GhostEvent21.Invoke();
                Ambience.clip = AfterJumpGhost;
                Ambience.Play();
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
                ItemName.text = "Flash Light  [E]";
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
            else if (hitevent.collider.gameObject.tag == "WorkShopDesk")
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
            else if (hitevent.collider.gameObject.tag == "Scissors")
            {
                ItemText.SetActive(true);
                ItemName.text = "Scissors [E]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "EyeWash")
            {
                ItemText.SetActive(true);
                ItemName.text = "EyeWash [E]";
                InterectItem = true;
            }
             else if (hitevent.collider.gameObject.tag == "MachineMiniGame")
            {
                ItemText.SetActive(true);
                ItemName.text = "Machine [Left Click]";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "DeskWorkShop")
            {
                if (!changeCam.CloseInterectShow)
                {
                    ItemText.SetActive(true);
                    ItemName.text = "Change View [E]";
                    InterectItem = true;
                }
            }
            else if (hitevent.collider.gameObject.tag == "DeskPushClothToDoll")
            {
                if (!changeCam.CloseInterectShow)
                {
                    ItemText.SetActive(true);
                    ItemName.text = "Change View [E]";
                    InterectItem = true;
                }
            }
            else if (hitevent.collider.gameObject.tag == "RollCloth")
            {
                if (playerPickUpItem.HaveScissor)
                {
                    ItemText.SetActive(true);
                    ItemName.text = "Cut [Left Click]";
                    InterectItem = true;
                }
                else if(!playerPickUpItem.HaveScissor)
                {
                    ItemText.SetActive(true);
                    ItemName.text = "Find a scrissor";
                    InterectItem = true;
                }
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

        // For new inventory

        #region Close Pick up Item

       #region Pick drop item

     /*   if (Itemhave != 0)
        {
            Tutext3.SetActive(true);
            tutorialText3.text = "Select item [1],[2],[3]";
        }
        else
        {
            Tutext3.SetActive(false);
        }
        if (!Working)
        {
            //Drop item
            #region Drop Item
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (ItemSelect == 0)
                {
                    box1 = false;
                    if (CrossOnHand && showCross)
                    {
                        InterectSound.clip = DropCrossSound;
                        InterectSound.Play();
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
                        Crosshave--;
                        CrossOnHand = false;
                        Tutext1.SetActive(false); Tutext2.SetActive(false);
                        CorssR.SetActive(false);
                        showCross = false;
                        Attack = false;
                        setTriggerCross = true;
                        if (CrossInv1)
                        {
                            if (curHpCross == 3) itemInventory1[0].SetActive(false);
                            if (curHpCross == 2) itemInventory1[3].SetActive(false);
                            if (curHpCross == 1) itemInventory1[4].SetActive(false);
                            CrossInv1 = false;
                        }
                    }


                        if (DollOnHand && showDoll)
                        {
                        InterectSound.clip = DropDollSound;
                        InterectSound.Play();
                        DropDoll();
                            Itemhave--;
                            Dollhave--;
                            Tutext1.SetActive(false);
                            showDoll = false;
                            DollR.SetActive(false);
                            if (DollInv1)
                            {
                                itemInventory1[1].SetActive(false);
                                DollInv1 = false;
                            }
                        }
                    

                        if (ClothOnHand && showCloth)
                        {

                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();
                        DropCloth();
                            Itemhave--;
                            Clothhave--;
                            Tutext1.SetActive(false);
                            ClothR.SetActive(false);
                            showCloth = false;
                            if (ClothInv1)
                            {
                                itemInventory1[2].SetActive(false);
                                ClothInv1 = false;
                            }

                        }
                    if (ScissorOnHand && showScissor)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropScissor();
                        Itemhave--;
                        ScissorHave--;
                        Tutext1.SetActive(false);
                        ScissorR.SetActive(false);
                        showScissor = false;
                        if (ScissorInv1)
                        {
                            itemInventory1[5].SetActive(false);
                            ScissorInv1 = false;
                        }
                    }
                    if (RedClothOnHand && ShowRedCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropRedCloth();
                        Itemhave--;
                        RedClothHave--;
                        Tutext1.SetActive(false);
                        RedClothR.SetActive(false);
                        ShowRedCloth = false;
                        if (RedCloInv1)
                        {
                            itemInventory1[6].SetActive(false);
                            RedCloInv1 = false;
                        }
                    }
                    if (BlueClothOnHand && ShowBlueCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropBlueCloth();
                        Itemhave--;
                        BlueClothHave--;
                        Tutext1.SetActive(false);
                        BlueClothR.SetActive(false);
                        ShowBlueCloth = false;
                        if (BlueCloInv1)
                        {
                            itemInventory1[7].SetActive(false);
                            BlueCloInv1 = false;
                        }
                    }
                    if (GreenClothOnHand && ShowGreenCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropGreenCloth();
                        Itemhave--;
                        GreenClothHave--;
                        Tutext1.SetActive(false);
                        GreenClothR.SetActive(false);
                        ShowGreenCloth = false;
                        if (GreenCloInv1)
                        {
                            itemInventory1[8].SetActive(false);
                            GreenCloInv1 = false;
                        }
                    }
                    if (YellowClothOnHand && ShowYellowCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropYellowCloth();
                        Itemhave--;
                        YellowClothHave--;
                        Tutext1.SetActive(false);
                        YellowClothR.SetActive(false);
                        ShowYellowCloth = false;
                        if (YellowCloInv1)
                        {
                            itemInventory1[9].SetActive(false);
                            YellowCloInv1 = false;
                        }
                    }

                }
                if (ItemSelect == 1)
                {
                    box2 = false;
                    if (CrossOnHand && showCross)
                    {
                        InterectSound.clip = DropCrossSound;
                        InterectSound.Play();

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
                        Crosshave--;
                        CrossOnHand = false;
                        Tutext1.SetActive(false); Tutext2.SetActive(false);
                        CorssR.SetActive(false);
                        showCross = false;
                        Attack = false;
                        setTriggerCross = true;
                        if (CrossInv2)
                        {
                            if (curHpCross == 3) itemInventory2[0].SetActive(false);
                            if (curHpCross == 2) itemInventory2[3].SetActive(false);
                            if (curHpCross == 1) itemInventory2[4].SetActive(false);
                            CrossInv2 = false;
                        }
                    }


                        if (DollOnHand && showDoll)
                        {
                        InterectSound.clip = DropDollSound;
                        InterectSound.Play();

                        DropDoll();
                            Itemhave--;
                            Dollhave--;
                            Tutext1.SetActive(false);
                            showDoll = false;
                            DollR.SetActive(false);
                            if (DollInv2)
                            {
                                itemInventory2[1].SetActive(false);
                                DollInv2 = false;
                            }
                        }

                    if (ClothOnHand && showCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropCloth();
                        Itemhave--;
                        Clothhave--;
                        Tutext1.SetActive(false);
                        ClothR.SetActive(false);
                        showCloth = false;
                        if (ClothInv2)
                        {
                            itemInventory2[2].SetActive(false);
                            ClothInv2 = false;
                        }

                    }
                    if (ScissorOnHand && showScissor)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropScissor();
                        Itemhave--;
                        ScissorHave--;
                        Tutext1.SetActive(false);
                        ScissorR.SetActive(false);
                        showScissor = false;
                        if (ScissorInv2)
                        {
                            itemInventory2[5].SetActive(false);
                            ScissorInv2 = false;
                        }
                    }
                    if (RedClothOnHand && ShowRedCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropRedCloth();
                        Itemhave--;
                        RedClothHave--;
                        Tutext1.SetActive(false);
                        RedClothR.SetActive(false);
                        ShowRedCloth = false;
                        if (RedCloInv2)
                        {
                            itemInventory2[6].SetActive(false);
                            RedCloInv2 = false;
                        }
                    }
                    if (BlueClothOnHand && ShowBlueCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropBlueCloth();
                        Itemhave--;
                        BlueClothHave--;
                        Tutext1.SetActive(false);
                        BlueClothR.SetActive(false);
                        ShowBlueCloth = false;
                        if (BlueCloInv2)
                        {
                            itemInventory2[7].SetActive(false);
                            BlueCloInv2 = false;
                        }
                    }
                    if (GreenClothOnHand && ShowGreenCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropGreenCloth();
                        Itemhave--;
                        GreenClothHave--;
                        Tutext1.SetActive(false);
                        GreenClothR.SetActive(false);
                        ShowGreenCloth = false;
                        if (GreenCloInv2)
                        {
                            itemInventory2[8].SetActive(false);
                            GreenCloInv2 = false;
                        }
                    }
                    if (YellowClothOnHand && ShowYellowCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropYellowCloth();
                        Itemhave--;
                        YellowClothHave--;
                        Tutext1.SetActive(false);
                        YellowClothR.SetActive(false);
                        ShowYellowCloth = false;
                        if (YellowCloInv2)
                        {
                            itemInventory2[9].SetActive(false);
                            YellowCloInv2 = false;
                        }
                    }

                }

                if (ItemSelect == 2)
                {
                    box3 = false;
                    if (CrossOnHand && showCross)
                    {
                        InterectSound.clip = DropCrossSound;
                        InterectSound.Play();

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
                        Crosshave--;
                        CrossOnHand = false;
                        Tutext1.SetActive(false); Tutext2.SetActive(false);
                        CorssR.SetActive(false);
                        showCross = false;
                        Attack = false;
                        setTriggerCross = true;
                        if (CrossInv3)
                        {
                            if (curHpCross == 3) itemInventory3[0].SetActive(false);
                            if (curHpCross == 2) itemInventory3[3].SetActive(false);
                            if (curHpCross == 1) itemInventory3[4].SetActive(false);
                            CrossInv3 = false;
                        }                       
                    }


                    if (DollOnHand && showDoll)
                    {
                        InterectSound.clip = DropDollSound;
                        InterectSound.Play();

                        DropDoll();
                        Itemhave--;
                        Dollhave--;
                        Tutext1.SetActive(false);
                        showDoll = false;
                        DollR.SetActive(false);
                        if (DollInv3)
                        {
                            itemInventory3[1].SetActive(false);
                            DollInv3 = false;
                        }
                    }

                    if (ClothOnHand && showCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropCloth();
                        Itemhave--;
                        Clothhave--;
                        Tutext1.SetActive(false);
                        ClothR.SetActive(false);
                        showCloth = false;
                        if (ClothInv3)
                        {
                            itemInventory3[2].SetActive(false);
                            ClothInv3 = false;
                        }
                    }

                    if (ScissorOnHand && showScissor)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropScissor();
                        Itemhave--;
                        ScissorHave--;
                        Tutext1.SetActive(false);
                        ScissorR.SetActive(false);
                        showScissor = false;
                        if (ScissorInv3)
                        {
                            itemInventory3[5].SetActive(false);
                            ScissorInv3 = false;
                        }
                    }
                    if (RedClothOnHand && ShowRedCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropRedCloth();
                        Itemhave--;
                        RedClothHave--;
                        Tutext1.SetActive(false);
                        RedClothR.SetActive(false);
                        ShowRedCloth = false;
                        if (RedCloInv3)
                        {
                            itemInventory3[6].SetActive(false);
                            RedCloInv3 = false;
                        }
                    }
                    if (BlueClothOnHand && ShowBlueCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropBlueCloth();
                        Itemhave--;
                        BlueClothHave--;
                        Tutext1.SetActive(false);
                        BlueClothR.SetActive(false);
                        ShowBlueCloth = false;
                        if (BlueCloInv3)
                        {
                            itemInventory3[7].SetActive(false);
                            BlueCloInv3 = false;
                        }
                    }
                    if (GreenClothOnHand && ShowGreenCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropGreenCloth();
                        Itemhave--;
                        GreenClothHave--;
                        Tutext1.SetActive(false);
                        GreenClothR.SetActive(false);
                        ShowGreenCloth = false;
                        if (GreenCloInv3)
                        {
                            itemInventory3[8].SetActive(false);
                            GreenCloInv3 = false;
                        }
                    }
                    if (YellowClothOnHand && ShowYellowCloth)
                    {
                        InterectSound.clip = DropClothSound;
                        InterectSound.Play();

                        DropYellowCloth();
                        Itemhave--;
                        YellowClothHave--;
                        Tutext1.SetActive(false);
                        YellowClothR.SetActive(false);
                        ShowYellowCloth = false;
                        if (YellowCloInv3)
                        {
                            itemInventory3[9].SetActive(false);
                            YellowCloInv3 = false;
                        }
                    }

                }
            }
     */
            #endregion
        /*
            // if don't have item
            if (Itemhave == 0)
            {
                Tutext1.SetActive(false); Tutext2.SetActive(false);
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
            if(ScissorHave == 0)
            {
                ScissorR.SetActive(false);
                ScissorOnHand = false;
            }

            #region Pick Up Item
            // Pick item
            Ray RPick = new Ray(pickUPPoint.position, pickUPPoint.forward);
            Debug.DrawRay(pickUPPoint.position, pickUPPoint.forward);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(RPick, out RaycastHit hitInfo, Pickrange))
                {
                    if (Itemhave != 3)
                    {
                        if (StoryNow >= 0)
                        {
                            #region Cross
                            if (hitInfo.collider.gameObject.tag == "Cross")
                            {
                                if (Crosshave != 1)
                                {
                                    CrossUse = hitInfo.collider.gameObject.GetComponent<CrossCheck>();
                                    curHpCross = CrossUse.curHp;
                                    Crosshave++;


                                    CloseOnHand();
                                    CrossOnHand = true;
                                    Attack = true;

                                    InterectSound.clip = CrossPickSound;
                                    InterectSound.Play();

                                    CorssR.SetActive(true);
                                    if (curHpCross == 3) CorssAni.SetTrigger("OnHand");
                                    if (curHpCross == 2) CorssAni.SetTrigger("OnHand2");
                                    if (curHpCross == 1) CorssAni.SetTrigger("OnHand3");
                                    Destroy(hitInfo.collider.gameObject);

                                    // print("Cross");
                                    Tutext1.SetActive(true);
                                    Tutext2.SetActive(true);
                                    tutorialText1.text = "Attack [Hold left]";
                                    tutorialText2.text = "Drop [G]";
                                    DollR.SetActive(false);
                                    ClothR.SetActive(false);
                                    ScissorR.SetActive(false);
                                    Itemhave++;
                                    Inventory.SetActive(true);
                                    if (!firstPickCross)
                                    {
                                        StartCoroutine(DelayTutorialhowToCross());
                                        firstPickCross = true;
                                        StopAttack();
                                    }
                                    if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                    {
                                        if (curHpCross == 3) itemInventory1[0].SetActive(true);
                                        if (curHpCross == 2) itemInventory1[3].SetActive(true);
                                        if (curHpCross == 1) itemInventory1[4].SetActive(true);
                                        CrossInv1 = true;
                                        ItemSelect = 0;
                                        box1 = true;
                                        InvPoint1.SetActive(true);
                                        InvPoint2.SetActive(false);
                                        InvPoint3.SetActive(false);
                                    }
                                    else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                    {
                                        if (curHpCross == 3) itemInventory2[0].SetActive(true);
                                        if (curHpCross == 2) itemInventory2[3].SetActive(true);
                                        if (curHpCross == 1) itemInventory2[4].SetActive(true);
                                        CrossInv2 = true;
                                        ItemSelect = 1;
                                        box2 = true;
                                        InvPoint1.SetActive(false);
                                        InvPoint2.SetActive(true);
                                        InvPoint3.SetActive(false);
                                    }
                                    else if (Itemhave == 3 && !box3)
                                    {
                                        if (curHpCross == 3) itemInventory3[0].SetActive(true);
                                        if (curHpCross == 2) itemInventory3[3].SetActive(true);
                                        if (curHpCross == 1) itemInventory3[4].SetActive(true);
                                        CrossInv3 = true;
                                        ItemSelect = 2;
                                        box3 = true;
                                        InvPoint1.SetActive(false);
                                        InvPoint2.SetActive(false);
                                        InvPoint3.SetActive(true);
                                    }
                                }
                                else
                                {
                                    Textdialogue.text = "(I can only have one.)";
                                    dialogCheck = true;
                                }
                            }
                        }

                       *//* else if (StoryNow < 5)
                        {
                            if (hitInfo.collider.gameObject.tag == "Cross")
                            {
                                Textdialogue.text = "(Not this time. Can only be used 2 times and only be held 1 on hand.)";
                                dialogCheck = true;
                            }
                        }*//*
#endregion 

                        #region Doll
                        if (Dollhave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "Doll")
                            {
                                // DollAni.SetTrigger("OnHand");
                                CloseOnHand();
                                DollOnHand = true;
                                showDoll = true;

                                InterectSound.clip = DollPickSound;
                                InterectSound.Play();

                                DollR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                //print("doll");
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";
                                CorssR.SetActive(false);
                                ClothR.SetActive(false);
                                ScissorR.SetActive(false);
                                Itemhave++;
                                Dollhave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[1].SetActive(true);
                                    DollInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[1].SetActive(true);
                                    DollInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[1].SetActive(true);
                                    DollInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }
                            }
                        }
                        #endregion

                        #region Cloth
                        if (Clothhave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "Cloth")
                            {
                                CloseOnHand();
                                ClothOnHand = true;
                                showCloth = true;

                                InterectSound.clip = ClothPickSound;
                                InterectSound.Play();

                                ClothR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";
                                DollR.SetActive(false);
                                CorssR.SetActive(false);
                                ScissorR.SetActive(false);
                                Itemhave++;
                                Clothhave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1  || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[2].SetActive(true);
                                    ClothInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[2].SetActive(true);
                                    ClothInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[2].SetActive(true);
                                    ClothInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }
                            }
                        }
#endregion

                        #region Scissor
                        if (ScissorHave != 1)
                        {
                            if (hitInfo.collider.gameObject.tag == "Scissors")
                            {
                                CloseOnHand();
                                ScissorOnHand = true;
                                showScissor = true;

                                InterectSound.clip = ClothPickSound;
                                InterectSound.Play();

                                ScissorR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";
                                DollR.SetActive(false);
                                CorssR.SetActive(false);
                                ClothR.SetActive(false);
                                Itemhave++;
                                ScissorHave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[5].SetActive(true);
                                    ScissorInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[5].SetActive(true);
                                    ScissorInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[5].SetActive(true);
                                    ScissorInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }
                            }
                        }
                        #endregion

                        #region RedCloth
                        if (RedClothHave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "RedCloth")
                            {
                                CloseOnHand();
                                RedClothOnHand = true;
                                ShowRedCloth = true;

                                InterectSound.clip = ClothPickSound;
                                InterectSound.Play();

                                CloseItemInInv();
                                RedClothR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";                             
                                Itemhave++;
                                RedClothHave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[6].SetActive(true);
                                    RedCloInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[6].SetActive(true);
                                    RedCloInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[6].SetActive(true);
                                    RedCloInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }
                            }
                        }
                        #endregion

                        #region BlueCloth
                        if (BlueClothHave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "BlueCloth")
                            {
                                CloseOnHand();
                                BlueClothOnHand = true;
                                ShowBlueCloth = true;

                                InterectSound.clip = ClothPickSound;
                                InterectSound.Play();

                                CloseItemInInv();
                                BlueClothR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";
                                Itemhave++;
                                BlueClothHave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[7].SetActive(true);
                                    BlueCloInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[7].SetActive(true);
                                    BlueCloInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[7].SetActive(true);
                                    BlueCloInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }
                            }
                        }
                        #endregion

                        #region GreenCloth
                        if (GreenClothHave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "GreenCloth")
                            {
                                CloseOnHand();
                                GreenClothOnHand = true;
                                ShowGreenCloth = true;

                                InterectSound.clip = ClothPickSound;
                                InterectSound.Play();

                                CloseItemInInv();
                                GreenClothR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";
                                Itemhave++;
                                GreenClothHave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[8].SetActive(true);
                                    GreenCloInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[8].SetActive(true);
                                    GreenCloInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[8].SetActive(true);
                                    GreenCloInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }
                            }
                        }
                        #endregion

                        #region YellowCloth
                        if (YellowClothHave != 3)
                        {
                            if (hitInfo.collider.gameObject.tag == "YellowCloth")
                            {
                                CloseOnHand();
                                YellowClothOnHand = true;
                                ShowYellowCloth = true;

                                InterectSound.clip = ClothPickSound;
                                InterectSound.Play();

                                CloseItemInInv();
                                YellowClothR.SetActive(true);
                                Destroy(hitInfo.collider.gameObject);
                                Tutext1.SetActive(true);
                                tutorialText1.text = "Drop [G]";
                                tutorialText2.text = "";
                                Itemhave++;
                                YellowClothHave++;
                                Inventory.SetActive(true);
                                if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                                {
                                    itemInventory1[9].SetActive(true);
                                    YellowCloInv1 = true;
                                    ItemSelect = 0;
                                    box1 = true;
                                    InvPoint1.SetActive(true);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                                {
                                    itemInventory2[9].SetActive(true);
                                    YellowCloInv2 = true;
                                    ItemSelect = 1;
                                    box2 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(true);
                                    InvPoint3.SetActive(false);
                                }
                                else if (Itemhave == 3 && !box3)
                                {
                                    itemInventory3[9].SetActive(true);
                                    YellowCloInv3 = true;
                                    ItemSelect = 2;
                                    box3 = true;
                                    InvPoint1.SetActive(false);
                                    InvPoint2.SetActive(false);
                                    InvPoint3.SetActive(true);
                                }
                            }
                           
                        } 
                        #endregion
                    }
                }

            }
            #endregion
        }
        #endregion*/

#endregion 

        // Have drop item on desk
        #region Pick up only

        Ray LPick = new Ray(pickUPPoint.position, pickUPPoint.forward);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(LPick, out RaycastHit hitInfo, Pickrange))
            {
                if (hitInfo.collider.gameObject.tag == "Lantern")
                {                
                    Light.SetActive(true);
                    LightOn = true;
                    LightOnHand = true;
                    pointLight.SetActive(true);
                    FlashLightGet = true;
                    Destroy(hitInfo.collider.gameObject);
                }

                /*if (hitInfo.collider.gameObject.tag == "Scissors")
                {
                    if (HpPlayer.curHp == 1)
                    {
                       // HpPlayer.openEyes();
                        OpenEye = true;
                    }
                    else
                    {
                        Textdialogue.text = "(Might be used to something but not this time.)";
                        dialogCheck = true;
                    }
                }*/

                if (hitInfo.collider.gameObject.tag == "EyeWash")
                {
                    if (HpPlayer.curHp < 4)
                    {
                        HpPlayer.Heal();                     
                        Destroy(hitInfo.collider.gameObject);
                    }
                 
                }

                #region Drop on desk
          /*      if (hitInfo.collider.gameObject.tag == "WorkShopDesk")
                {
                    if (Clothhave > 0 || Dollhave > 0)
                    {
                       
                        if (Crosshave == 1)
                        {
                            Itemhave = 1;
                            if (CrossInv1) box1 = true;
                            if (CrossInv2) box2 = true;
                            if (CrossInv3) box3 = true;
                        }


                        //Doll on hand
                        Tutext1.SetActive(false);
                        showDoll = false;
                        DollR.SetActive(false);
                        if (ItemSelect == 0)
                        {
                            if (DollInv1)
                            {
                                itemInventory1[1].SetActive(false);
                                takeFinishDoll.AddDollOnDesk(1);
                                Dollhave--;
                                Itemhave--;
                                box1 = false;
                                DollInv1 = false;
                            }
                        }
                        else if (ItemSelect == 1)
                        {
                            if (DollInv2)
                            {
                                itemInventory2[1].SetActive(false);
                                takeFinishDoll.AddDollOnDesk(1);
                                box2 = false;
                                Dollhave--;
                                Itemhave--;
                                DollInv2 = false;
                            }
                        }
                        else if (ItemSelect == 2)
                        {
                            if (DollInv3)
                            {
                                itemInventory3[1].SetActive(false);
                                takeFinishDoll.AddDollOnDesk(1);
                                box3 = false;
                                Dollhave--;
                                Itemhave--;
                                DollInv3 = false;
                            }
                        }
                        // Cloth on hand
                        showCloth = false;
                        ClothR.SetActive(false);
                        if (ItemSelect == 0)
                        {
                            if (ClothInv1)
                            {
                                itemInventory1[2].SetActive(false);
                                takeFinishDoll.AddClothOndesk(1);
                                box1 = false;
                                Clothhave--;
                                Itemhave--;
                                ClothInv1 = false;
                            }
                        }
                        else if (ItemSelect == 1)
                        {
                            if (ClothInv2)
                            {
                                itemInventory2[2].SetActive(false);
                                takeFinishDoll.AddClothOndesk(1);
                                box2 = false;
                                Clothhave--;
                                Itemhave--;
                                ClothInv2 = false;
                            }
                        }
                        else if (ItemSelect == 2)
                        {
                            if (ClothInv3)
                            {
                                itemInventory3[2].SetActive(false);
                                takeFinishDoll.AddClothOndesk(1);
                                box3 = false;
                                Clothhave--;
                                Itemhave--;
                                ClothInv3 = false;
                            }
                        }
                    }

                }*/
                #endregion
            }
        }

        #region Check item on hand to active mini game
        if(ItemSelect == 0)
        {
            if (DollInv1 || ClothInv1) changeCam.ItemOnHand();
            else changeCam.NoItem();
        }
        else if(ItemSelect == 1)
        {
            if (DollInv2 || ClothInv2) changeCam.ItemOnHand();
            else changeCam.NoItem();
        }
        else if(ItemSelect == 2)
        {
            if(DollInv3 || ClothInv3) changeCam.ItemOnHand();
            else changeCam.NoItem();
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FlashLightGet)
            {
                if (!LightOn)
                {
                    InterectSound.clip = LanternPickSound;
                    InterectSound.Play();
                    Light.gameObject.SetActive(true);
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

        #region Need to do next
        if (StoryNow == 1)
        {
            NeedToDo.text = "Explore bed room";
            if (DialogueStory)
            {
                CanvaDialog.SetActive(true);
                Textdialogue.text = "Who knocking on the door this time!?";
                StartCoroutine(DelayTutorialStartGame());
                DialogueStory = false;
            }
        }
        if (StoryNow == 2)
        {
            NeedToDo.text = "Go to check front door";
            CheckFrontDoor.Invoke();
        }
        if (StoryNow == 3)
        {
            if (DialogueStory)
            {
                StartCoroutine(DelayTutorialWhatToDo());
                DialogueStory = false;
            }
            NeedToDo.text = "Go to sewing room or explore house";
            CanExplore.Invoke();
        }
        if (StoryNow == 4)
        {
            NeedToDo.text = "Make doll at workshop";
            if (DialogueStory)
            {
                Time.timeScale = 0;
                THowToDoll.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                CloseTurial = true;
                DialogueStory = false;
            }
        }
        if (StoryNow == 5)
        {
            NeedToDo.text = "Find doll and cloth to finish job";

            if (DialogueStory)
            {
                Textdialogue.text = "(Ah.. out of tools. let's check the storage room)";
                dialogCheck = true;
                Doorclose.Invoke();
                DialogueStory = false;
            }
        }
        if (StoryNow == 6)
        {
            if (DialogueStory)
            {
                NeedToDo.text = "Go to bed";
                Textdialogue.text = "(That is for today. Now I need to do something to sleep in peace.)";
                dialogCheck = true;
                DialogueStory = false;
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
   public void DropDoll()
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
   public void DropCloth()
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

    public void DropScissor()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingScissor(RH);

    }

    public void DropRedCloth()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingRedcloth(RH);

    }
    public void DropBlueCloth()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingBluecloth(RH);

    }
    public void DropGreenCloth()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingGreencloth(RH);

    }
    public void DropYellowCloth()
    {
        Ray ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
        {
            destination = ray.GetPoint(1000);
        }

        DropingYellowcloth(RH);

    }

    #region Throw item prefabs


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
    void DropingScissor(Transform FirePoint)
    {
        var projectileOBj = Instantiate(ScissorD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    void DropingRedcloth(Transform FirePoint)
    {
        var projectileOBj = Instantiate(RedClothD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    void DropingGreencloth(Transform FirePoint)
    {
        var projectileOBj = Instantiate(GreenClothD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    void DropingBluecloth(Transform FirePoint)
    {
        var projectileOBj = Instantiate(BlueClothD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    void DropingYellowcloth(Transform FirePoint)
    {
        var projectileOBj = Instantiate(YellowClothD, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * DropSpeed;
    }
    #endregion



#endregion

    public void StopAttack()
    {
        Attack = false;
    }
    public void CanAttack()
    {
        if (CrossOnHand)
        {
            StartCoroutine(DelayCanAtack());
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
            Time.timeScale = 0;
            TGhostCum.SetActive(true);
            CloseTurial = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
        if (other.gameObject.tag == "Make6Dolls")
        {
            Make6Dolls();
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
            NeedToDoAnimate.Play("ToDoAmiate", 0, 0);
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
    }

    IEnumerator DelayEndind()
    {
        CanvaDialog.SetActive(true);    
        yield return new WaitForSeconds(1);
        if (EndD1)
        {
            EndGame.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
            Attack = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CanvaDialog.SetActive(false);
            dialogCheck = false;
            EndgameCloseSound.Invoke();
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

    IEnumerator DelayTutorialStartGame()
    {
        yield return new WaitForSeconds(6);
        Textdialogue.text = "Really annoying....";
        yield return new WaitForSeconds(2);
        dialogCheck = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        TStaetGame.SetActive(true);
        CloseTurial = true;
    }


    IEnumerator DelayTutorialWhatToDo()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        TWhatToDO.SetActive(true);
        CloseTurial = true;
    }
    IEnumerator DelayTutorialhowToCross()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        THowToCross.SetActive(true);
        CloseTurial = true;      
    }
    IEnumerator DelayCanAtack()
    {
        yield return new WaitForSeconds(0.1f);
        Attack = true;
    }

    IEnumerator DelayHolyLight()
    {
        yield return new WaitForSeconds(0.5f);
        HolyLight.SetActive(true);
        if (GhostEx)
        {
            if (curHpCross == 2) CorssAni.SetTrigger("HitGhost");
            if (curHpCross == 1) CorssAni.SetTrigger("HitGhost2");
            GhostEx = false;    
        }
        yield return new WaitForSeconds(1f);
        HolyLight.SetActive(false);
        yield return new WaitForSeconds(2f);
        if(curHpCross == 1)
        {
            inventoryManager.GetSelectedItem(true);
        }
    }

    #endregion

    #region Enter work shop view
    public void WorkShopview()
    {
        Working = true;
    }
    public void QuitWorkShop()
    {
        Working = false;
    }
    #endregion

    public void DelayTHeal()
    {
        Time.timeScale = 0;
        CloseTurial = true;
    }

    #region close all tutorial
    public void CloseAllTutorial()
    {
        if (CloseTurial)
        {
            TStaetGame.SetActive(false);
            TWhatToDO.SetActive(false);
            THowToDoll.SetActive(false);
            TGhostCum.SetActive(false);
            THowToCross.SetActive(false);
            THowToHeal.SetActive(false);
            Time.timeScale = 1;
            CloseTurial = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CanAttack();
        }
    }
    #endregion

    #region CloseOnHand
    public void CloseOnHand()
    {
        DollOnHand = false;
        CrossOnHand = false;
        ClothOnHand = false;
        RedClothOnHand = false;
        GreenClothOnHand = false;
        BlueClothOnHand = false;
        YellowClothOnHand = false;
        ScissorOnHand = false;
    }

    public void CloseItemInInv()
    {
        DollR.SetActive(false);
        CorssR.SetActive(false);
        ClothR.SetActive(false);
        ScissorR.SetActive(false);
        RedClothR.SetActive(false);
        GreenClothR.SetActive(false);
        BlueClothR.SetActive(false);
        YellowClothR.SetActive(false);
    }
    #endregion

    public void CrossRuin()
    {
        if (crossruin)
        {
            CrossUse.curHp = curHpCross;
            crossruin = false;
        }
    }

    private bool GetDollLast;
    public void GetAllDolls()
    {
        if (!GetDollLast)
        {
            StoryNow++;
            DialogueStory = true;
            GetDollLast = true;
        }
    }
    bool FindDoll;
    public void Make6Dolls()
    {
        FindDoll = true;
        NeedToDo.text = "Collect All Dolls";
    }

    public void Mouseselect(int i)
    {
        ItemSelect = i;
        if (ItemSelect == 0)
        {
            InvPoint1.SetActive(true);
            InvPoint2.SetActive(false);
            InvPoint3.SetActive(false);
        }
        else if (ItemSelect == 1)
        {
            InvPoint1.SetActive(false);
            InvPoint2.SetActive(true);
            InvPoint3.SetActive(false);
        }
        else if (ItemSelect == 2)
        {
            InvPoint1.SetActive(false);
            InvPoint2.SetActive(false);
            InvPoint3.SetActive(true);
        }
    }

    #region Player drag Item on Desk
    public void pushItemInbasket()
    {
        if (Clothhave > 0 || Dollhave > 0)
        {

            //Doll on hand
          
            // Cloth on hand
            showCloth = false;
            ClothR.SetActive(false);
            if (ItemSelect == 0)
            {
                if (ClothInv1)
                {
                    itemInventory1[2].SetActive(false);
                    box1 = false;
                    Clothhave--;
                    Itemhave--;
                    ClothInv1 = false;
                }
            }
            else if (ItemSelect == 1)
            {
                if (ClothInv2)
                {
                    itemInventory2[2].SetActive(false);
                    box2 = false;
                    Clothhave--;
                    Itemhave--;
                    ClothInv2 = false;
                }
            }
            else if (ItemSelect == 2)
            {
                if (ClothInv3)
                {
                    itemInventory3[2].SetActive(false);
                    box3 = false;
                    Clothhave--;
                    Itemhave--;
                    ClothInv3 = false;
                }
            }
        }
    }
    #endregion

    #region ClothDesign Did't use
    /*
    public void GetclothDesign()
    {
        if (Itemhave != 3)
        {
            #region RedCloth
            if (DesignNum == 0)
            {
                if (RedClothHave != 3)
                {
                    CloseOnHand();
                    RedClothOnHand = true;
                    ShowRedCloth = true;

                    InterectSound.clip = ClothPickSound;
                    InterectSound.Play();

                    CloseItemInInv();
                    RedClothR.SetActive(true);
                    Tutext1.SetActive(true);
                    tutorialText1.text = "Drop [G]";
                    tutorialText2.text = "";
                    Itemhave++;
                    RedClothHave++;
                    Inventory.SetActive(true);
                    if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                    {
                        itemInventory1[6].SetActive(true);
                        RedCloInv1 = true;
                        ItemSelect = 0;
                        box1 = true;
                        InvPoint1.SetActive(true);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                    {
                        itemInventory2[6].SetActive(true);
                        RedCloInv2 = true;
                        ItemSelect = 1;
                        box2 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(true);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 3 && !box3)
                    {
                        itemInventory3[6].SetActive(true);
                        RedCloInv3 = true;
                        ItemSelect = 2;
                        box3 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(true);
                    }

                }
            }
            #endregion

            #region BlueCloth
            if (DesignNum == 1)
            {
                if (BlueClothHave != 3)
                {
                    CloseOnHand();
                    BlueClothOnHand = true;
                    ShowBlueCloth = true;

                    InterectSound.clip = ClothPickSound;
                    InterectSound.Play();

                    CloseItemInInv();
                    BlueClothR.SetActive(true);
                    Itemhave++;
                    BlueClothHave++;
                    Inventory.SetActive(true);
                    if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                    {
                        itemInventory1[7].SetActive(true);
                        BlueCloInv1 = true;
                        ItemSelect = 0;
                        box1 = true;
                        InvPoint1.SetActive(true);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                    {
                        itemInventory2[7].SetActive(true);
                        BlueCloInv2 = true;
                        ItemSelect = 1;
                        box2 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(true);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 3 && !box3)
                    {
                        itemInventory3[7].SetActive(true);
                        BlueCloInv3 = true;
                        ItemSelect = 2;
                        box3 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(true);
                    }

                }
            }
            #endregion

            #region GreenCloth
            if (DesignNum == 2)
            {
                if (GreenClothHave != 3)
                {
                    CloseOnHand();
                    GreenClothOnHand = true;
                    ShowGreenCloth = true;

                    InterectSound.clip = ClothPickSound;
                    InterectSound.Play();

                    CloseItemInInv();
                    GreenClothR.SetActive(true);
                    Itemhave++;
                    GreenClothHave++;
                    Inventory.SetActive(true);
                    if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                    {
                        itemInventory1[8].SetActive(true);
                        GreenCloInv1 = true;
                        ItemSelect = 0;
                        box1 = true;
                        InvPoint1.SetActive(true);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                    {
                        itemInventory2[8].SetActive(true);
                        GreenCloInv2 = true;
                        ItemSelect = 1;
                        box2 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(true);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 3 && !box3)
                    {
                        itemInventory3[8].SetActive(true);
                        GreenCloInv3 = true;
                        ItemSelect = 2;
                        box3 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(true);
                    }

                }
        }
            #endregion

            #region YellowCloth
            if (DesignNum == 3)
            {
                if (YellowClothHave != 3)
                {
                    CloseOnHand();
                    YellowClothOnHand = true;
                    ShowYellowCloth = true;

                    InterectSound.clip = ClothPickSound;
                    InterectSound.Play();

                    CloseItemInInv();
                    YellowClothR.SetActive(true);
                    Itemhave++;
                    YellowClothHave++;
                    Inventory.SetActive(true);
                    if (Itemhave == 1 && !box1 || Itemhave == 2 && !box1 || Itemhave == 3 && !box1)
                    {
                        itemInventory1[9].SetActive(true);
                        YellowCloInv1 = true;
                        ItemSelect = 0;
                        box1 = true;
                        InvPoint1.SetActive(true);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 2 && !box2 || Itemhave == 3 && !box2)
                    {
                        itemInventory2[9].SetActive(true);
                        YellowCloInv2 = true;
                        ItemSelect = 1;
                        box2 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(true);
                        InvPoint3.SetActive(false);
                    }
                    else if (Itemhave == 3 && !box3)
                    {
                        itemInventory3[9].SetActive(true);
                        YellowCloInv3 = true;
                        ItemSelect = 2;
                        box3 = true;
                        InvPoint1.SetActive(false);
                        InvPoint2.SetActive(false);
                        InvPoint3.SetActive(true);
                    }

                }
            }*/
    #endregion


    #region DropDisign Did' use
    /*
    public void DropDesignColor()
    {
        #region I1
        if (ItemSelect == 0)
        {
            box1 = false;
            if (RedClothOnHand && ShowRedCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                RedClothHave--;
                Tutext1.SetActive(false);
                RedClothR.SetActive(false);
                ShowRedCloth = false;
                if (RedCloInv1)
                {
                    itemInventory1[6].SetActive(false);
                    RedCloInv1 = false;
                }
            }
            if (BlueClothOnHand && ShowBlueCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                BlueClothHave--;
                Tutext1.SetActive(false);
                BlueClothR.SetActive(false);
                ShowBlueCloth = false;
                if (BlueCloInv1)
                {
                    itemInventory1[7].SetActive(false);
                    BlueCloInv1 = false;
                }
            }
            if (GreenClothOnHand && ShowGreenCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                GreenClothHave--;
                Tutext1.SetActive(false);
                GreenClothR.SetActive(false);
                ShowGreenCloth = false;
                if (GreenCloInv1)
                {
                    itemInventory1[8].SetActive(false);
                    GreenCloInv1 = false;
                }
            }
            if (YellowClothOnHand && ShowYellowCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                YellowClothHave--;
                Tutext1.SetActive(false);
                YellowClothR.SetActive(false);
                ShowYellowCloth = false;
                if (YellowCloInv1)
                {
                    itemInventory1[9].SetActive(false);
                    YellowCloInv1 = false;
                }
            }
        }
        #endregion
        
        #region I2
        if (ItemSelect == 1)
        {
            box2 = false;
            if (RedClothOnHand && ShowRedCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                RedClothHave--;
                Tutext1.SetActive(false);
                RedClothR.SetActive(false);
                ShowRedCloth = false;
                if (RedCloInv2)
                {
                    itemInventory2[6].SetActive(false);
                    RedCloInv2 = false;
                }
            }
            if (BlueClothOnHand && ShowBlueCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                BlueClothHave--;
                Tutext1.SetActive(false);
                BlueClothR.SetActive(false);
                ShowBlueCloth = false;
                if (BlueCloInv2)
                {
                    itemInventory2[7].SetActive(false);
                    BlueCloInv2 = false;
                }
            }
            if (GreenClothOnHand && ShowGreenCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                GreenClothHave--;
                Tutext1.SetActive(false);
                GreenClothR.SetActive(false);
                ShowGreenCloth = false;
                if (GreenCloInv2)
                {
                    itemInventory2[8].SetActive(false);
                    GreenCloInv2 = false;
                }
            }
            if (YellowClothOnHand && ShowYellowCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                YellowClothHave--;
                Tutext1.SetActive(false);
                YellowClothR.SetActive(false);
                ShowYellowCloth = false;
                if (YellowCloInv2)
                {
                    itemInventory2[9].SetActive(false);
                    YellowCloInv2 = false;
                }
            }
        }
        #endregion

        #region I3
        if (ItemSelect == 2)
        {
            box3 = false;
            if (RedClothOnHand && ShowRedCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                RedClothHave--;
                Tutext1.SetActive(false);
                RedClothR.SetActive(false);
                ShowRedCloth = false;
                if (RedCloInv3)
                {
                    itemInventory3[6].SetActive(false);
                    RedCloInv3 = false;
                }
            }
            if (BlueClothOnHand && ShowBlueCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                BlueClothHave--;
                Tutext1.SetActive(false);
                BlueClothR.SetActive(false);
                ShowBlueCloth = false;
                if (BlueCloInv3)
                {
                    itemInventory3[7].SetActive(false);
                    BlueCloInv3 = false;
                }
            }
            if (GreenClothOnHand && ShowGreenCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                GreenClothHave--;
                Tutext1.SetActive(false);
                GreenClothR.SetActive(false);
                ShowGreenCloth = false;
                if (GreenCloInv3)
                {
                    itemInventory3[8].SetActive(false);
                    GreenCloInv3 = false;
                }
            }
            if (YellowClothOnHand && ShowYellowCloth)
            {
                InterectSound.clip = DropClothSound;
                InterectSound.Play();

                Itemhave--;
                YellowClothHave--;
                Tutext1.SetActive(false);
                YellowClothR.SetActive(false);
                ShowYellowCloth = false;
                if (YellowCloInv3)
                {
                    itemInventory3[9].SetActive(false);
                    YellowCloInv3 = false;
                }
            }
        }*/
        #endregion
    


    #region Map

    public void OpenMap()
    {
        MapOn = true;
        MapCanva.SetActive(true);
    }

    public void CloseMap()
    {
        MapOn = false;
        MapCanva.SetActive(false);
    }

    #endregion

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