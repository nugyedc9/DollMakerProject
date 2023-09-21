using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{

    public AudioMixer Master;


    public void SetSoundEffectLV(float SFXLV)
    {
        Master.SetFloat("SoundEffect", SFXLV);
    }
    public void SetSoundMusicLV(float SFXLV)
    {
        Master.SetFloat("Music", SFXLV);
    }
    public void SetSoundMasterLV(float SFXLV)
    {
        Master.SetFloat("Master", SFXLV);
    }
}
