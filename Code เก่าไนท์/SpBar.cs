using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpBar : MonoBehaviour
{
    public Slider SliderSp;

    public void SetMaxSP(float Health)
    {
        SliderSp.maxValue = Health;
        SliderSp.value = Health;
    }

    public void SetSP(float Health)
    {
        SliderSp.value = Health;
    }
}
