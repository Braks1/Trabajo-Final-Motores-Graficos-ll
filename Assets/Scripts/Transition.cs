using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator bar;
    
    // Funcion encargada de aparecer la animacion de parpadeo //
    public void appearBar()
    {
        bar.Play("fade");
    }

}
