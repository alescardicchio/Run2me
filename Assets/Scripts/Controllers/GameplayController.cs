using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    private Text scoreText;
    private Text lifeText;
    private int score;
    private int lifeScore;


    void Awake() {
        makeInstance();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
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
        // if(lifeScore >= 0) {
        lifeText.text = lifeScore.ToString();
        // }
    }
}
