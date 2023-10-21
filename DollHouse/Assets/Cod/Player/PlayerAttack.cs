using player;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;


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

    public AudioClip HitGhostSound;   
    public AudioClip HitWindSound;
    public AudioSource HitAudio;


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


    private Door DoorInterect;
    private Event Ghostevent;

    public UnityEvent LightOutEvent;
    public UnityEvent GetKey;
    public UnityEvent GhostEvent;
    public UnityEvent EventCloseDoor;

    private void Start()
    {
        //ShootAudio = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        Ray r = new Ray(RH.position, RH.forward);
        if(CrossOnHand) Attack = true;
        #region Attack
        if (Attack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                CorssAni.SetTrigger("AttackCorss");
               HitAudio.clip = HitWindSound;
                
                HitAudio.Play();
                if (Physics.Raycast(r, out RaycastHit hitinfo, InterectRange))
                {
                    if (hitinfo.collider.gameObject.tag == "Ghost")
                    {
                        HitAudio.clip = HitGhostSound;
                        StartCoroutine(AttackReset());
                    }
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
                    Destroy(hitInterect.collider.gameObject);
                }
            }
        }
        if (Physics.Raycast(Interect, out RaycastHit hitevent, Pickrange))
        {
            if (hitevent.collider.tag == "GhostEvent")
            {
                GhostEvent.Invoke();
                Destroy(hitevent.collider.gameObject);  
            }
        }




        if (CanDropItem)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
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
                    CanDropItem= false;
                    ClothOnHand = false;
                    DropCloth();
                }
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
    }


}