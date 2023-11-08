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
    public GameObject DollD;
    public GameObject ClothD;

    [Header("CrossAction")]
    public float curHpCross;
    private bool crossruin;

    [Header("CanvaDialogue")]
    public GameObject CanvaDialog;
    public Image CanvaImage;
    public Sprite PickItem1;
    public Sprite PickItem2;
    public Sprite GoRadioPlay;
    public Sprite FinishWork1;
    public Sprite LightOut;
    public Sprite Getlantern;
    public Sprite DoorLockEvent;
    public Sprite GhostSpawn;
    public Sprite BeQuiet;

    [Header("CanvaInterect")]
    public Image InterectAble;
    public Sprite InterectSprite;
    public Sprite pointSprite; 
    public GameObject ItemText;
    public TextMeshProUGUI ItemName;
    private bool dialogCheck, InterectItem;


    private Door DoorInterect;
    private Ghost GhostHit;
    private CrossCheck CrossUse;
    public bool Holddown;

    [Header("AllEvent")]
    public UnityEvent LightOutEvent;
    public UnityEvent GetKey;
    public UnityEvent GhostEvent;
    public UnityEvent EventCloseDoor;
    public UnityEvent Radio;

    private void Start()
    {       
    }

    void Update()
    {
        Ray r = new Ray(RH.position, RH.forward);


        #region Attack
        if (CrossOnHand) Attack = true;
        if (Attack)
        {
            if (Input.GetMouseButtonDown(0))
            {

                CorssAni.SetTrigger("AttackCorss");
                HitAudio.clip = HitWindSound;
                HitAudio.Play();
                Holddown = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CorssAni.SetTrigger("NotAttack");
                Holddown = false;
            }
        } else Holddown = false;
        if (Holddown)
        {
            if (Physics.Raycast(r, out RaycastHit hitinfo, 5))
            {
                if (hitinfo.collider.gameObject.tag == "Ghost")
                {
                    crossruin = true;
                    HitAudio.clip = HitGhostSound;
                    GhostHit = hitinfo.collider.gameObject.GetComponent<Ghost>();
                    GhostHit.PlayerHitGhost();

                    //StartCoroutine(AttackReset());
                }
            }
        }


        #endregion

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
                    print("GetKey");
                    GetKey.Invoke();
                    CanvaImage.sprite = GhostSpawn;
                    StartCoroutine(GhostSpawnDelay());
                    Destroy(hitInterect.collider.gameObject);
                }
                if (hitInterect.collider.gameObject.tag == "Radio")
                {
                    Radio.Invoke();
                }

            }
        }

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
                ItemName.text = "Key";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Radio")
            {
                ItemText.SetActive(true);
                ItemName.text = "Radio";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Door")
            {
                ItemText.SetActive(true);
                ItemName.text = "Door";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Lantern")
            {
                ItemText.SetActive(true);
                ItemName.text = "Lantern";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Cross")
            {
                ItemText.SetActive(true);
                ItemName.text = "Cross";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Doll")
            {
                ItemText.SetActive(true);
                ItemName.text = "Doll";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Cloth")
            {
                ItemText.SetActive(true);
                ItemName.text = "Cloth";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "DeskWorkShop")
            {
                ItemText.SetActive(true);
                ItemName.text = "DeskWorkShop";
                InterectItem = true;
            }
            else if (hitevent.collider.gameObject.tag == "Basket")
            {
                ItemText.SetActive(true);
                ItemName.text = "Basket";
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


        if (CanDropItem)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (CrossOnHand)
                {
                    CorssR.SetActive(false);
                    Attack = false;
                    DropCross();
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
                        CorssAni.SetTrigger("OnHand");
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
            }
        }

        #region LightUP
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



    public void StopAttack()
    {
        Attack = false;
    }
    public void CanAttack()
    {
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

    public void OnTriggerEnter(Collider other)
    {
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
            CanvaImage.sprite = PickItem1;
            dialogCheck = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "GoRadioPlay")
        {
            CanvaImage.sprite = GoRadioPlay;
            dialogCheck = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "LightOutEvent")
        {
            CanvaImage.sprite = LightOut;
            StartCoroutine(LightOutDelay());
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "ClostDoorEvent")
        {
            CanvaImage.sprite = DoorLockEvent;
            dialogCheck = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "BeQuiet")
        {
            CanvaImage.sprite = BeQuiet;
            dialogCheck = true;
            Destroy(other.gameObject);
        }

    }

    public void PickItem2Event()
    {
        CanvaImage.sprite = FinishWork1;
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
        CanvaImage.sprite = PickItem2;
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
    }
    IEnumerator LightOutDelay()
    {
        CanvaDialog.SetActive(true);
        yield return new WaitForSeconds(5);
        CanvaImage.sprite = Getlantern;
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
    }
    IEnumerator GhostSpawnDelay()
    {
        CanvaDialog.SetActive(true);
        yield return new WaitForSeconds(5);
        CanvaImage.sprite = BeQuiet;
        yield return new WaitForSeconds(5);
        CanvaDialog.SetActive(false);
    }

    public void CrossRuin()
    {
        if (crossruin)
        {
            curHpCross--;
            CrossUse.curHp = curHpCross;
            crossruin = false;
        }
    }


}