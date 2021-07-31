using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuButton : MonoBehaviour
{
    public Animator ba;
    public GameObject boton;
    
    // Funcion encargada de desplegar el menu mostrando el objetivo del juego en curso //
    
    public void startAnimation()
    {
        ba.Play("objectivePress", -1, 0f);
        StartCoroutine(restart());
    }

    IEnumerator restart()
    {
        boton.SetActive(false);
        yield return new WaitForSecondsRealtime(7.3f);
        boton.SetActive(true);
    }
}
