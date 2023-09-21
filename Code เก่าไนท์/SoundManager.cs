using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource source;

    public AudioClip btnclick;

    public void PlayClickSound()
    {
        source.PlayOneShot(btnclick);
    }
}

