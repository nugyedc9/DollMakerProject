using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HoldShoot : MonoBehaviour
{
    public float Range = 100f;
    public float projectilespeed = 30;
    public Camera FpsCam;
    public GameObject projectile;
    public Transform RH;
    public PlayerSP P;
    public float Mana;

    private Vector3 destination;

    public VisualEffect AffPlay;

    public AudioClip HoldSound;
    AudioSource HoldAudio;

    private void Start()
    {
        AffPlay.Stop();
        HoldAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (P.CurSp > Mana)
        {
            if (Input.GetButton("Fire1"))
            {
                AffPlay.Play();
                HoldAudio.clip = HoldSound;
                HoldAudio.Play();
                Shoot();
            }
            else
                AffPlay.Stop();
        }
        else if (P.CurSp < Mana)
        {
            AffPlay.Stop();
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
        P.ManaUse(Mana);
    }
}
