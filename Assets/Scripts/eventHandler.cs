using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventHandler : MonoBehaviour
{
    public Transition transition;
    public GameObject hud;
    public GameObject bar1;
    public GameObject combat;
    public GameObject objectt;
    public Player player;
    public Animator combatMenu;
    
    // El onTriggerEnter permite al jugador entrar en combate al llegar a determinada parte //
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            hud.SetActive(false);
            bar1.SetActive(true);
            objectt.SetActive(false);
            player.dialog = true;
            transition.appearBar();
            StartCoroutine(startCombatTimer());

            
        }
        IEnumerator startCombatTimer()
        {
            yield return new WaitForSecondsRealtime(3);
            combat.SetActive(true);
            hud.SetActive(true);
            yield return new WaitForSecondsRealtime(2);
            combatMenu.Play("menuSlide");

        }
    }


}
