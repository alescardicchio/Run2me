using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    
    public bool isAlive;

    void Awake() {
        isAlive = true; // DA CAMBIARE.
    }


    void OnTriggerEnter2D(Collider2D target) {

        // Verifica se il player è a contatto con un diamante.
        if(target.tag == "Collectable") {
            GameplayController.instance.incrementScore(); // Incrementa di 1 i diamanti raccolti.
            Debug.Log("Diamante raccolto!");
            target.gameObject.SetActive(false); // Quando il player 'tocca' il diamante, questo viene rimosso dalla scena.
        }
        // Verifica se il player è a contatto con un nemico.
        if(target.tag == "Enemy") {
            GameplayController.instance.decrementLife(); // DA CAMBIARE!
            isAlive = false;
            transform.position = new Vector3(0, 100000, 0);
        }
        // Verifica se il player ha raggiunto l'uscita.
        if(target.tag == "Exit") {
            Time.timeScale = 0f;
        }
    }
}
