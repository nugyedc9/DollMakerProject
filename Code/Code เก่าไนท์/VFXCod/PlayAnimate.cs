using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayAnimate : MonoBehaviour
{
    public VisualEffect AffPlay;


    private void Start()
    {
        AffPlay.Stop();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            AffPlay.Play();
        }

        else
            AffPlay.Stop();
    }

  
}
