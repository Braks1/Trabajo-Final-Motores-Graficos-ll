using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int maxHpE;
    public int maxKiE;
    public int hpE;
    public int KiE;

    public TMP_Text hpTextE;
    public TMP_Text kiTextE;
    public healthBarE healthBarE;
    public kiBarE kiBarE;

    void Start()
    {
        // Declaro las stats maximas del enemigo y a partir de estas establezco el valor de los stats actuales //
        
        maxHpE = 250;
        maxKiE = 110;
        hpE = maxHpE;
        KiE = maxKiE;
        healthBarE.setMaxHealth(maxHpE);
        kiBarE.setMaxHealth(maxKiE);
    }

    void Update()
    {
        // Codigo para prevenir valores negativos y que el enemigo sobrepase su capacidad maxima //
        
        if (hpE <= 0)
        {
            hpE = 0;
        }

        if (hpE >= maxHpE)
        {
            hpE = 250;
        }

        if (KiE <= 0)
        {
            KiE = 0;
        }

        if (KiE >= maxKiE)
        {
            KiE = 110;
        }

        // Codigo encargado de hacer que el stat enemigo actual este conectado a la barra de vida y por ende este suba y baje //
        
        healthBarE.slider.value = hpE;
        kiBarE.sliderK.value = KiE;

        hpTextE.text = ("" + hpE);
        kiTextE.text = ("" + KiE);
    }

}
