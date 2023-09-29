using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAttack : MonoBehaviour
{
    public float projectilespeed = 30;
    public Camera FpsCam;
    public GameObject projectile;
    public Transform RH;
    public bool Attack;

    private Vector3 destination;

    public AudioClip ShootSound;
    AudioSource ShootAudio;

    [Header("PlayerHit")]
    public GameObject IdleP;
    public GameObject AttackP;

    private void Start()
    {
        ShootAudio = GetComponent<AudioSource>();
        Attack = true;
    }

    void Update()
    {
        if (Attack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine("AttackReset");
                /*ShootAudio.clip = ShootSound;
                ShootAudio.Play();*/
                Shoot();
            }
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

    void Attacking(Transform FirePoint)
    {
        var projectileOBj = Instantiate(projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectilespeed;
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

