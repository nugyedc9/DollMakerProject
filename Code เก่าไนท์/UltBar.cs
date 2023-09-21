using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltBar : MonoBehaviour
{
    public Slider SliderUlt;

    public void SetLowUlt(float Health)
    {
        SliderUlt.minValue = Health;
        SliderUlt.value = Health;
    }

    public void SetSP(float Health)
    {
        SliderUlt.value = Health;
    }
}
