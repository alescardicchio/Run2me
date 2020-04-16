using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public bool isAlive;
    private Rigidbody2D playerBody;
    private Animator anim;
    public GameObject AttackUI;
    public GameObject ShootUI;
    public GameObject LevelCompleteUI;

    void Awake() {
        anim = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        isAlive = true;
    }

    void Update() {
        // Se il player esce fuori dalla mappa, muore:
        if(transform.position.y <= -5f) {
            if(isAlive) {
                isAlive = false;
                FindObjectOfType<AudioManager>().Play("OhNo");
                GameplayController.instance.decrementLife();
                transform.position = new Vector3(0, 100000, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target) {

        // Se il player è a contatto con un diamante:
        if (target.tag == "Collectable") {
            GameplayController.instance.incrementScore(); // Incrementa di 1 i diamanti raccolti.
            FindObjectOfType<AudioManager>().Play("Diamante");
            target.gameObject.SetActive(false); // Quando il player 'tocca' il diamante, questo viene rimosso dalla scena.
        }

        // Se il player è a contatto con un' arma:
        if (target.tag == "Gun") {
            AttackUI.SetActive(false);
            ShootUI.SetActive(true);
            target.gameObject.SetActive(false);
        }

        //Se il player è entrato a contatto con un nemico:
        if(target.tag == "Enemy") {
            PlayerDie();
        }

        // Se il player calpesta una delle prime delle piattaforme.
        if(target.tag == "Ground_Timer") {
            TimerController.instance.BeginTimer();
            Debug.Log("Timer partito!");
        }

        // Se il player ha raggiunto l'uscita.
        if(target.tag == "Exit") {
            TimerController.instance.EndTimer();
            LevelCompleteUI.SetActive(true);
            GameObject.Find("Canvas/Touchscreen").SetActive(false);
            GameObject.Find("Scores").SetActive(false);
            LevelCompleteUI.GetComponent<LevelController>().ShowLevelDialog();
        }
    }

    public void PlayerDie() {
        anim.SetTrigger("Dead");
        if(isAlive) {
            isAlive = false;    // Se il player tocca il nemico, muore.
            GameplayController.instance.decrementLife();
        }
    }
}
