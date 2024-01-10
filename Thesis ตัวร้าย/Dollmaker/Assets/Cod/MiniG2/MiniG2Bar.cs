using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniG2Bar : MonoBehaviour
{
    public Slider SliderBar;

    public void SetMaxBar(float max)
    {
        SliderBar.maxValue = max;
        SliderBar.value = 0;  
    }

    public void SetMinBar(float min)
    {
        SliderBar.value = min;
    }
}
