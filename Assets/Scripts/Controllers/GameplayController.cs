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
    public int score;
    public int lifeScore;
    
    public GameObject GameOverUI;
    public GameObject TouchscreenUI;

    void Awake() {
        makeInstance();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
    }
/*
    void OnEnable() {
        SceneManager.sceneLoaded += levelFinishedLoading;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= levelFinishedLoading;
    }

    void levelFinishedLoading(Scene scene, LoadSceneMode mode) {
        if(scene.name != "MainMenu") {  // Se la scena non è il menu iniziale ci troviamo in uno dei livelli del gioco.
            if(!GameManager.instance.playerDied_GameRestarted) { 
                // E' il primo avvio del gioco e ci troviamo nel livello iniziale, il giocatore avrà 0 diamanti e 3 vite.
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
    */
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
        if (lifeScore >= 0) {
            lifeText.text = lifeScore.ToString();
        }
        StartCoroutine(playerDied());
    }

    IEnumerator playerDied() {
        yield return new WaitForSeconds(1.2f);
        // Non abbiamo più vite, game over :
        if(lifeScore == 0) {
            // => Si potrebbe inserire anche un audio 'GameOver' 
            FindObjectOfType<AudioManager>().Play("GameOver");
            GameOverUI.SetActive(true);
            TouchscreenUI.SetActive(false);
        } else {
            // Il player e' morto ma ha comunque delle vite rimanenti :
            GameManager.instance.playerDied_GameRestarted = true;
            GameManager.instance.score = 0;
            GameManager.instance.lifeScore = lifeScore;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       }
    }
}
