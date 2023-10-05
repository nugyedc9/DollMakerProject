using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAttack : MonoBehaviour
{
    public float ShootSpeed = 30;
    public float DropSpeed;
    public Camera FpsCam;
    public GameObject projectile;
    public Transform RH;
    public Transform pickUPPoint;
    public float InterectRange;
    public bool Attack, LightOn, CanDropItem, CrossOnHand, DollOnHand, ClothOnHand;
    public float Pickrange;
    private Vector3 destination;

    public AudioClip ShootSound;
    AudioSource ShootAudio;

    [Header("PlayerHit")]
    public GameObject IdleP;
    public GameObject AttackP;

    [Header("PLayerLight")]
    public GameObject Light;

    [Header("Item On Hand")]
    public GameObject CorssR;
    public GameObject DollR;
    public GameObject ClothR;
    [Header("Item Drop")]
    public GameObject CrossD;
    public GameObject DollD;
    public GameObject ClothD;

    private void Start()
    {
        ShootAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Ray r = new Ray(RH.position, RH.forward);

        #region Attack
        if (Attack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine("AttackReset");
                /*ShootAudio.clip = ShootSound;
                ShootAudio.Play();*/
                if (Physics.Raycast(r, out RaycastHit hitinfo, InterectRange))
                {
                    if (hitinfo.collider.gameObject.tag == "Ghost") 
                        Shoot();
                }
                   
            }
        }
        #endregion

        if (CanDropItem)
        {
            if (Input.GetKeyDown(KeyCode.Q))
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
        else
        {
            Ray RPick = new Ray(pickUPPoint.position, pickUPPoint.forward);
            if (Input.GetKeyDown(KeyCode.E)) {
                if (Physics.Raycast(RPick, out RaycastHit hitInfo, Pickrange))
                {
                    if (hitInfo.collider.gameObject.tag == "Cross")
                    {
                     CrossOnHand = true;
                     Attack = true;
                     CorssR.SetActive(true);
                     CanDropItem = true;
                     Destroy(hitInfo.collider.gameObject);
                   // print("Cross");
                    }
                    if (hitInfo.collider.gameObject.tag == "Doll")
                    {
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

        #region LightUP
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!LightOn)
            {
                Light.gameObject.SetActive(true);
                LightOn = true;
            }
            else
            {
                Light.gameObject.SetActive(false);
                LightOn = false;
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
        AttackP.SetActive(true);
        IdleP.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        AttackP.SetActive(false);
        IdleP.SetActive(true);
    }
    public void StopAttack()
    {
        Attack = false;
    }
    public void CanAttack()
    {
        Attack = true;
    }
}

