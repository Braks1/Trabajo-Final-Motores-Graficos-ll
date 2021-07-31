using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kiBar : MonoBehaviour
{
    public Slider sliderK;

    public void setMaxHealth(int health)
    {
        sliderK.maxValue = health;
        sliderK.value = health;
    }

    public void setHealth(int health)
    {
        sliderK.value = health;
    }
}
