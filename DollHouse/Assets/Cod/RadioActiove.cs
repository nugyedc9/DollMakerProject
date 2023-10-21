using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioActiove : MonoBehaviour
{
    public AudioSource Radioa;
    public bool playSound;

    private void Start()
    {
        Radioa.Stop();
    }


    public void ActiveRadio()
    {
        if (!playSound)
        {
            playSound = true;
            Radioa.Play();
        }
        else
        {
            playSound=false;
            Radioa.Stop();
        }
    }

}
