using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expBar : MonoBehaviour
{
    public Slider sliderEXP;

    public void setMaxExp(int exp)
    {
        sliderEXP.maxValue = exp;
        sliderEXP.value = exp;
    }

    public void setMinExp(int exp)
    {
        sliderEXP.minValue = exp;
        sliderEXP.value = exp;
    }

    public void setExp(int exp)
    {
        sliderEXP.value = exp;
    }
}
