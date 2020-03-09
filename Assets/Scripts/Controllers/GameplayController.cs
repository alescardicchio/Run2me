using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    private Text scoreText;
    private Text lifeText;
    private int score = 0;
    public int lifeScore = 3;
    float colorModifier = 1.0f;


    void Awake() {
        makeInstance();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
    }

    //Per modificare il colore... modificare o eliminare
    private void modeColor()
    {
        SpriteRenderer sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sprRend.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        if (colorModifier < 1.0f)
            colorModifier += Time.deltaTime;
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
        this.lifeScore -= 1;
        //modeColor();
        colorModifier = 0.0f;
        if (lifeScore >= 0) {
        lifeText.text = this.lifeScore.ToString();
        }
    }
}
