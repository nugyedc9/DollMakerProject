using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNote : MonoBehaviour
{

    [Header("---- Audio ----")]
    public AudioSource AudioSound;
    public void SelectThisDesign()
    {
        AudioSound.Play();
        Destroy(gameObject);
    }


}
