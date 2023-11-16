using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Event : MonoBehaviour
{

    public Animator Ghost1;
    public AudioSource Ghost1Sound;
    public AudioClip lmao;
    public Animator CutFinal;
    


    private void Awake()
    {
        Ghost1 = GetComponent<Animator>();
    }

    public void GhostEvent1()
    {
        Ghost1Sound.clip = lmao;
        Ghost1Sound.Play();
        Ghost1.Play("Event1", 0, 0);
        StartCoroutine(waitdelay());
    }

    public void GhostEvent2()
    {
        Ghost1Sound.clip = lmao;
        Ghost1Sound.Play();
        Ghost1.Play("Ghost1", 0, 0);
        StartCoroutine(waitdelay());
    }

    IEnumerator waitdelay()
    {
        yield return new WaitForSeconds(9);
        Ghost1.gameObject.SetActive(false);
    }

    public void Cutscene()
    {
        Ghost1Sound.clip = lmao;
        Ghost1Sound.Play();
        CutFinal.Play("FinalCutr",0,0);
    }

}
    