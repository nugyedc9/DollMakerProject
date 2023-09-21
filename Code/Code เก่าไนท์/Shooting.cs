using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float Range = 100f;
    public float projectilespeed = 30;
    public Camera FpsCam;
    public GameObject projectile;
    public Transform RH;
    public PlayerSP P;
    public int Mana;

    private Vector3 destination;

    public AudioClip ShootSound;
    AudioSource ShootAudio;



    private void Start()
    {
        ShootAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (P.CurSp > Mana)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ShootAudio.clip = ShootSound;
                ShootAudio.Play();
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

        ShootMagic(RH);

    }

    void ShootMagic(Transform FirePoint)
    {
        var projectileOBj = Instantiate(projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileOBj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectilespeed;
        if (P != null)
        {
            P.ManaUse(Mana);
        }
    }



}
