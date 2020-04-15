using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{ 
    public Text scoreText;
    public Text diamondText;
    public Text enemyText;
    public GameObject[] stars;
 
    public void ShowLevelDialog() {

        diamondText.text = GameManager.instance.score.ToString();

        if(SceneManager.GetActiveScene().name == "gameLvl1") {
            enemyText.text = GameManager.instance.spiderScore.ToString();
            scoreText.text = (10*(GameManager.instance.lifeScore + GameManager.instance.score + GameManager.instance.spiderScore)).ToString();
        }
        if(SceneManager.GetActiveScene().name == "woodland") {
            enemyText.text = GameManager.instance.trollScore.ToString();
            scoreText.text = (10*(GameManager.instance.lifeScore + GameManager.instance.score + GameManager.instance.trollScore)).ToString();
        }
        if(SceneManager.GetActiveScene().name == "WinterLand") {
            enemyText.text = GameManager.instance.winterScore.ToString();
            scoreText.text = (10*(GameManager.instance.lifeScore + GameManager.instance.score + GameManager.instance.winterScore)).ToString();
        }

        SetStars(int.Parse(scoreText.text));
}

    private void SetStars(int actualScore) {

        if(actualScore > 0 && actualScore <= 40) {
            stars[0].SetActive(true);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }
        if(actualScore > 40 && actualScore <= 80) {
            stars[0].SetActive(false);
            stars[1].SetActive(true);
            stars[2].SetActive(false);
        }
        if(actualScore > 80 && actualScore <= 120) {
           stars[0].SetActive(false);
           stars[1].SetActive(false);
           stars[2].SetActive(true);
        }
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}