using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class combatScript : MonoBehaviour
{
    public int caseV = 0;
    public int dmgP;
    public int dmgE;
    public int kiCP;
    public int kiCE;
    public GameObject enemySprite;
    public GameObject playerSprite;
    public GameObject explosionE;
    public GameObject explosionP;
    public GameObject panelMenuInicio;
    public GameObject panelAnunciadorP;
    public GameObject panelAnunciadorE;
    public GameObject anuncioDamageP;
    public GameObject anuncioDefenseP;
    public GameObject enemyDecidingG;
    public GameObject enemyNormalAttack;
    public GameObject noKiRemaining;
    public GameObject winGameObject;
    public GameObject loseGameObject;
    public TMP_Text noKiRemainingText;
    public TMP_Text anuncioDamageTextP;
    public TMP_Text anuncioDefenseTextP;
    public TMP_Text enemyDecidingTextE;
    public TMP_Text enemyNormalAttackText;
    public TMP_Text winText;
    public TMP_Text loseText;
    public Player player;
    public Enemy enemy;
    public bool Defense = false;

    void Update()
    {
        // Si la vida del jugador o del enemigo llega a 0, se ejecuta la funcion respectiva de victoria o derrota //
        
        if (enemy.hpE == 0)
        {
            enemySprite.SetActive(false);
            explosionE.SetActive(true);
            win();
        }

        if (player.hpP == 0)
        {
            playerSprite.SetActive(false);
            explosionP.SetActive(true);
            lose();
        }
    }

    
    // Funcion de derrota // 
    public void lose()
    {
        enemyNormalAttack.SetActive(false);
        loseGameObject.SetActive(true);
        loseText.text = ("No puede ser! Trunks ha sido derrotado!");
        StartCoroutine(loseTransition());


        IEnumerator loseTransition()
        {
            yield return new WaitForSecondsRealtime(5);
            SceneManager.LoadScene("loseScene");

        }

    }
    
    // Funcion de victoria // 
    public void win ()
    {
        panelAnunciadorP.SetActive(true);
        winGameObject.SetActive(true);
        winText.text = ("El enemigo ha sido derrotado! Has Ganado!");
        StartCoroutine(winTransition());


        IEnumerator winTransition()
        {
            yield return new WaitForSecondsRealtime(5);
            SceneManager.LoadScene("winScene");

        }

    }
    
    // Funcion de ataque del jugador, declaro una variable de daño, le asigno un numero entre el 20 y el 60, el juego comprueba si el daño es superior a la vida del enemigo o no y se separa en 2 caminos //

    // Si el daño era superior a la vida del enemigo setea su vida en 0 y pasa a la condicion de victoria

    // Si el daño es inferior a la vida del enemigo entonces se encarga de descontar y pasa a la funcion de esperar // 
    public void playerAttack()
    {
        anuncioDefenseP.SetActive(false);
        panelMenuInicio.SetActive(false);
        dmgP = Random.Range(21, 61);
        if (enemy.hpE < dmgP)
        {
            enemy.hpE = 0;
        }
        if (enemy.hpE > dmgP)
        {
            enemy.hpE -= dmgP;
            panelAnunciadorP.SetActive(true);
            anuncioDamageP.SetActive(true);
            anuncioDamageTextP.text = (" Trunks ha atacado! inflingiendo un total de " + dmgP + " de daño");
            StartCoroutine(waitTimeAP());
        }



        IEnumerator waitTimeAP()
        {
            yield return new WaitForSecondsRealtime(5);
            panelAnunciadorP.SetActive(false);
            anuncioDamageP.SetActive(false);
            enemyChoose();
        }
    }
    
    // Funcion de defensa, al igual que en el codigo de ataque elige una cierta cantidad de energia a usar y la guarda en una variable, comprueba si el costo es mayor o no a la energia actual del jugador //

    // Si el costo es superior a la energia del jugador no hace nada y despliega un mensaje indicando esto, pasando al turno del enemigo

    // Si el costo es inferior a la energia del jugador entonces descuenta, activa un booleano el cual fuerza al enemigo a realizar 0 de daño y despues desactiva este booleano para que pueda seguir dañando al jugador en otros turnos //
    public void playerDefense()
    {
        panelMenuInicio.SetActive(false);
        anuncioDamageP.SetActive(false);
        kiCP = Random.Range(1, 11);
        if (player.KiP < kiCP)
        {
            player.KiP -= 0;
           panelAnunciadorP.SetActive(true);
            noKiRemaining.SetActive(true);
            noKiRemainingText.text = ("Trunks ha intentado usar Defensa pero ya no tiene KI!");
            StartCoroutine(NOwaitTimeAPD());
        }
        if (player.KiP >= kiCP)
        {
            player.KiP -= kiCP;
            Defense = true;
            panelAnunciadorP.SetActive(true);
            anuncioDefenseP.SetActive(true);
            anuncioDefenseTextP.text = (" Trunks ha utilizado Defensa! usando " + kiCP + " de ki ");
            StartCoroutine(waitTimeAPD());
        }

        IEnumerator waitTimeAPD()
        {
            yield return new WaitForSecondsRealtime(5);
            panelAnunciadorP.SetActive(false);
            anuncioDefenseP.SetActive(false);
            enemyChoose();
        }

        IEnumerator NOwaitTimeAPD()
        {
            yield return new WaitForSecondsRealtime(5);
            panelAnunciadorP.SetActive(false);
            noKiRemaining.SetActive(false);
            enemyChoose();
        }
    }
    
    // Es una funcion que se encarga de brindar un espacio entre el turno del jugador y del turno enemigo, mediante un switch activa el ataque correspondiente de este ultimo //
    public void enemyChoose()
    {
        panelAnunciadorP.SetActive(false);
        StartCoroutine(enemyDecidingg());

        
        IEnumerator enemyDecidingg()
        {
            panelAnunciadorE.SetActive(true);
            enemyDecidingG.SetActive(true);
            enemyDecidingTextE.text = ("Esperando al enemigo...");
            yield return new WaitForSecondsRealtime(5);
            enemyDecidingG.SetActive(false);
            switch (caseV)
            {
                default:enemyNormalAttackF();
                    break;
            }
        }
    }

    // El codigo de ataque del enemigo es exactamente igual al codigo de ataque del jugador con la diferencia de que emplea sus propias variables para dañarlo a este //

    // Lo unico distinto vendria a ser los ifs que se derivan en caso de que el Jugador haya seleccionado Defensa, el cual activara un booleano que le dira a la variable de daño que haga 0 en el ataque // 
    public void enemyNormalAttackF()
    {
        enemyNormalAttack.SetActive(true);
        dmgE = Random.Range(25, 41);
        if (player.hpP < dmgE )
        {
            player.hpP = 0;
        }
        
        if (player.hpP > dmgE)
        {
            if (Defense == true)
            {
                dmgE = 0;
                player.hpP -= dmgE;
                Defense = false;
            }

            if (Defense == false)
            {
                
                player.hpP -= dmgE;
                enemyNormalAttackText.text = ("El enemigo ha usado un ataque normal, causando " + dmgE + " de daño");
                StartCoroutine(waitTimeAE());
            }
        }
        


        IEnumerator waitTimeAE()
        {
            yield return new WaitForSecondsRealtime(5);
            enemyDecidingG.SetActive(false);
            enemyNormalAttack.SetActive(false);
            panelAnunciadorE.SetActive(false);
            panelMenuInicio.SetActive(true);
        }
    }
}
