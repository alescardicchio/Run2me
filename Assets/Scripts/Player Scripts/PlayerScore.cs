using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public bool isAlive;
    private Rigidbody2D playerBody;

    //public bool Heart1;
    //public bool Heart2;
    //public bool Heart3;
    //private float jumpForce = 250f;
    //private float retry = -0.4f;
    
    void Awake() {
        playerBody = GetComponent<Rigidbody2D>();
        isAlive = true;
        //Heart1 = true;
        //Heart2 = true;
        //Heart3 = true;
    }

    void Update() {
        // Se il player esce fuori dalla mappa, muore:
        if(transform.position.y <= -5f) {
            if(isAlive) {
                isAlive = false;               
                GameplayController.instance.decrementLife();
                transform.position = new Vector3(0, 100000, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target) {
        //Vector2 vett = new Vector2(retry, jumpForce);

        // Se il player è a contatto con un diamante:
        if (target.tag == "Collectable") {
            GameplayController.instance.incrementScore(); // Incrementa di 1 i diamanti raccolti.
            FindObjectOfType<AudioManager>().Play("Diamante");
            target.gameObject.SetActive(false); // Quando il player 'tocca' il diamante, questo viene rimosso dalla scena.
        }
        //Se il player è entrato a contatto con un nemico:
        if(target.tag == "Enemy") {
            if(isAlive) {
                isAlive = false;    // Se il player tocca il nemico, muore.               
                GameplayController.instance.decrementLife();
                transform.position = new Vector3(0, 100000, 0);
            }
            /*int life = GameplayController.instance.lifeScore;
            if (life == 2)
            {
                Heart1 = false;
                playerBody.AddForce(vett);
            }
            if (life == 1)
            {
                Heart2 = false;
                playerBody.AddForce(vett);
            }
            if (life <= 0)
            {
                Debug.Log("Vita persa!");
                Heart3 = false;
                isAlive = false;
                transform.position = new Vector3(0, 100000, 0);
            }*/
        }
        // Verifica se il player ha raggiunto l'uscita.
        if(target.tag == "Exit") {
            //Time.timeScale = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}



/* Oppure come prima:
 * public class PlayerScore : MonoBehaviour
{
    public bool isAlive;

    void Awake()
    {
        isAlive = true;
    }


    void OnTriggerEnter2D(Collider2D target) {

        // Verifica se il player è a contatto con un diamante.
        if (target.tag == "Collectable")
        {
            GameplayController.instance.incrementScore(); // Incrementa di 1 i diamanti raccolti.
            Debug.Log("Diamante raccolto!");
            target.gameObject.SetActive(false); // Quando il player 'tocca' il diamante, questo viene rimosso dalla scena.
        }
        // Verifica se il player è entrato a contatto con un nemico.
        if (target.tag == "Enemy")
        {
            GameplayController.instance.decrementLife();
            if (GameplayController.instance.lifeScore <= 0)
            {
                Debug.Log("Vita persa!");
                isAlive = false;
                transform.position = new Vector3(0, 100000, 0);
            }
        }
        // Verifica se il player ha raggiunto l'uscita.
        if(target.tag == "Exit") {
            Time.timeScale = 0f;
        }
    }
}
 */
