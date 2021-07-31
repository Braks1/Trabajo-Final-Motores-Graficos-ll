using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{


    // capacidad maxima stats //

    public int maxHpP;
    public int maxKiP;
    public int maxExpP = 50;
    public int levelP = 1;

    // valores stats // 


    public int hpP;
    public int KiP;
    public int ExpP;

    // utileria para llamar a objetos y scripts // 
    
    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator ar;
    public healthBar hbar;
    public kiBar kbar;
    public expBar ebar;
    public TMP_Text lvlText;
    public TMP_Text hpText;
    public TMP_Text kiText;
    public TMP_Text expText;
    public bool dialog = false;
    bool preventExecute;

    void Start()
    {


        // declaro los stats maximos

        maxHpP = 190;
        maxKiP = 90;
        
        // le asigno los valores de los stats maximos a los actuales //

        hpP = maxHpP;
        KiP = maxKiP;
        ExpP = 0;
        
        // Asocio el valor maximo de los sliders de las barras con el valor maximo de los stats
        
        hbar.setMaxHealth(maxHpP);
        kbar.setMaxHealth(maxKiP);
        ebar.setMaxExp(maxExpP);
        ebar.setMinExp(ExpP);
        



    }
    void Update()
    {
        // prevengo valores negativos y valores que superen a la capacidad maxima de los stats // 
       
        if (hpP <= 0)
        {
            hpP = 0;
        }

        if (hpP >= maxHpP)
        {
            hpP = maxHpP;
        }

        if (KiP <= 0)
        {
            KiP = 0;
        }

        if (KiP >= maxKiP)
        {
            KiP = maxKiP;
        }

        // actualizo barras de stats en tiempo real //

        hbar.slider.value = hpP;
        kbar.sliderK.value = KiP;
        ebar.sliderEXP.value = ExpP;

        // animaciones movimiento

        if (dialog == false)
        {
            if (preventExecute == false)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    ar.Play("Derecha");
                    preventExecute = true;
                }
            }

            if (preventExecute == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    ar.Play("Izquierda");
                    preventExecute = true;
                }
            }
            if (preventExecute == false)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    ar.Play("Arriba");
                    preventExecute = true;
                }
            }
            if (preventExecute == false)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    ar.Play("Abajo");
                    preventExecute = true;

                }
            }

        }

        // animaciones en idle

        if (Input.GetKeyUp(KeyCode.S))
            {
                ar.Play("idleAbajo");
                preventExecute = false;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ar.Play("idleArriba");
                preventExecute = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                ar.Play("idleDerecha");
                preventExecute = false;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                ar.Play("idleIzquierda");
                preventExecute = false;
            }
        

        // Comando para dar experiencia //

        if (Input.GetKeyDown(KeyCode.Space))
        {
            giveExp(5);
        }

        void giveExp(int value)
        { 
            ExpP += value;
            ebar.setExp(ExpP);
        }

        // Si excedo experiencia maxima, ejecuto funcion de subir de nivel y seteo el stat de experiencia a 0 y aumento en 1 la variable de nivel //


        if (ExpP >= maxExpP)
        {
            ExpP = 0;
            ebar.setExp(ExpP);
            levelP++;
            levelUp();
        }
        
        // Codigo encargado de mostrar la informacion en tiempo real en la pantalla acerca de las stats // 
        
        lvlText.text = ("Nivel: " + levelP);
        hpText.text = ("" + hpP);
        kiText.text = ("" + KiP);
        expText.text = ("" + ExpP);
    }

   

    // Funcion para subir de nivel, se encarga de subirle cierta cantidad de puntos a las variables declaradas y rellena stats al subir de nivel //
    void levelUp()
    {
        maxHpP += 30;
        maxKiP += 20;
        maxExpP += 50;
        ebar.setMaxExp(maxExpP);
        hbar.setMaxHealth(maxHpP);
        kbar.setMaxHealth(maxKiP);
        hpP = maxHpP;
        KiP = maxKiP;
    }

    void FixedUpdate()
    {
        // El movimiento esta colocado en un fixedupdate para que las colisiones no se bugeen, el bool de false previene el movimiento al llegar a determinada parte del mapa //
        
        if (dialog == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * movementSpeed * Time.fixedDeltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * movementSpeed * Time.fixedDeltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * movementSpeed * Time.fixedDeltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * movementSpeed * Time.fixedDeltaTime);
            }
        }
        
    }


}
