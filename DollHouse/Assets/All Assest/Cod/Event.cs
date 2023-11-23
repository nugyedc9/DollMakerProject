using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
public class Event : MonoBehaviour
{
    [Header("GhostThing")]
    public Animator Ghost1;
    public AudioSource GhostSound;
    public AudioClip Ghost1Clip;
    public AudioClip Ghost2Clip;
    public AudioClip Ghost3Clip;
    public Animator CutFinal;

    [Header("Light on / off")]
    public GameObject LightSwitchOn;
    public GameObject LightSwitchOff;
    public GameObject LightOn;
    public GameObject LightOff;
    public AudioSource LightSound;
    public bool TurnLight;



    private void Awake()
    {
        Ghost1 = GetComponent<Animator>();
    }

    public void GhostEvent1()
    {
        GhostSound.clip = Ghost1Clip;
        GhostSound.Play();
        Ghost1.Play("Event1", 0, 0);
        StartCoroutine(waitdelay());
    }

    public void GhostEvent2()
    {
        GhostSound.clip = Ghost2Clip;
        GhostSound.Play();
        Ghost1.Play("Ghost1", 0, 0);
        StartCoroutine(waitdelay());
    }
    public void GhostEvent21()
    {
        GhostSound.clip = Ghost2Clip;
        GhostSound.Play();
        Ghost1.Play("Ghost1Another", 0, 0);
        StartCoroutine(waitdelay());
    }
    public void GhostEvent3()
    {
        GhostSound.clip = Ghost3Clip;
        GhostSound.Play();
        Ghost1.Play("4DollEvent", 0, 0);
        StartCoroutine(waitdelay());
    }

    public void TurnOnLight()
    {
        LightSound.Play();

            if (!TurnLight)
            {
                LightOn.SetActive(true);
                LightOff.SetActive(false);
                LightSwitchOn.SetActive(true);
                LightSwitchOff.SetActive(false);
                TurnLight = true;
            }
            else
            {
                LightOn.SetActive(false);
                LightOff.SetActive(true);
                LightSwitchOn.SetActive(false);
                LightSwitchOff.SetActive(true);
                TurnLight = false;
            }
    }

    public void GhostLightOut()
    {
        LightOff.SetActive(true);
        if (!TurnLight)
        {
            LightSwitchOn.SetActive(true);
            LightSwitchOff.SetActive(false);
        }
        else
        {
            LightSwitchOn.SetActive(false);
            LightSwitchOff.SetActive(true);
        }
    }


    IEnumerator waitdelay()
    {
        yield return new WaitForSeconds(9);
        Ghost1.gameObject.SetActive(false);
    }


}
    