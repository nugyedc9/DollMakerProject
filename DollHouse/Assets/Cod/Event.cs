using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{

    public Animator Ghost1;
    public AudioSource Ghost1Sound;
    public AudioClip lmao;
    


    private void Awake()
    {
        Ghost1 = GetComponent<Animator>();
    }

    public void GhostEvent1()
    {
        Ghost1Sound.clip = lmao;
        Ghost1Sound.Play();
        Ghost1.Play("Ghost1", 0, 0);
        StartCoroutine(waitdelay());
    }

    IEnumerator waitdelay()
    {
        yield return new WaitForSeconds(1);
        Ghost1.gameObject.SetActive(false);
    }

}
    