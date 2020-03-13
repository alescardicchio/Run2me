using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    private Text scoreText;
    private Text lifeText;
    private int score;
    private int lifeScore;
    
    //float colorModifier = 1.0f;


    void Awake() {
        makeInstance();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
    }

    //Per modificare il colore... modificare o eliminare
    /*private void modeColor()
    {
        SpriteRenderer sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sprRend.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        if (colorModifier < 1.0f)
            colorModifier += Time.deltaTime;
    }*/

    void OnEnable() {
        SceneManager.sceneLoaded += levelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= levelFinishedLoading;
    }

    void levelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if(scene.name != "MainMenu") {  // Se la scena non è il menu iniziale, dunque ci troviamo in uno dei livelli del gioco.
            if(!GameManager.instance.playerDied_GameRestarted) { // E' il primo avvio del gioco, quindi il giocatore avrà 0 diamanti e 3 vite iniziali.
                score = 0;
                lifeScore = 3;
            } else {    // Il player e' morto almeno una volta
                score = GameManager.instance.score;
                lifeScore = GameManager.instance.lifeScore;
            }
            scoreText.text = score.ToString();
            lifeText.text = lifeScore.ToString();
        }
    }

    private void makeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    public void incrementScore() {
        score++;
        scoreText.text = score.ToString();
    }

    public void decrementLife() {
        lifeScore--;
        //modeColor();
        //colorModifier = 0.0f;
        if (lifeScore >= 0) {
            lifeText.text = lifeScore.ToString();
        }
        StartCoroutine(playerDied());
    }

    public GameObject boy;
    public GameObject girl;
    
    IEnumerator playerDied() {
        yield return new WaitForSeconds(1.5f);
        // Non abbiamo più vite, game over :
        if(lifeScore == 0) {
                // ==> SI POTREBBE IMPLEMENTARE UNA SCRITTA OPPURE UN AUDIO 'GAME-OVER' !
            SceneManager.LoadScene("MainMenu");
        } else {
            // Il player e' morto ma ha comunque delle vite rimanenti :
            GameManager.instance.playerDied_GameRestarted = true;
            GameManager.instance.score = 0;
            GameManager.instance.lifeScore = lifeScore;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       }
    }
}
