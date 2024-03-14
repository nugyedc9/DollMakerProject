using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialNote : MonoBehaviour
{
    public UnityEvent Getcom;

    [Header("---- Audio ----")]
    public AudioSource AudioSound;
    public void SelectThisDesign()
    {
        AudioSound.Play();
        Destroy(gameObject);
    }

    public void OnEnable()
    {
        Getcom.Invoke();
    }
}
