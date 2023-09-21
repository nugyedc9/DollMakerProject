using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpbar : MonoBehaviour
{
    public Slider SliderHp;

    public void SetMaxHp(float Health)
    {
        SliderHp.maxValue = Health;
        SliderHp.value = Health;
    }

    public void SetHP(float Health)
    {
        SliderHp.value = Health;
    }

}
