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
    public GameObject[] enemies;
    
    public GameObject LevelMapUI;
    private int starsCollected;


    public void GoToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLevelDialog() {

        diamondText.text = GameManager.instance.score.ToString();

        if(SceneManager.GetActiveScene().name == "gameLvl1") {
            Level1Complete();
        }

        if(SceneManager.GetActiveScene().name == "woodland") {
            Level2Complete();
        }
        
        if(SceneManager.GetActiveScene().name == "WinterLand") {
            Level3Complete();
        }
}

    private void Level1Complete() {
        enemies[0].SetActive(true);
        enemies[1].SetActive(false);
        enemies[2].SetActive(false);

        enemyText.text = GameManager.instance.spiderScore.ToString();
        scoreText.text = (10*(GameManager.instance.lifeScore + GameManager.instance.score + GameManager.instance.spiderScore)).ToString();

        GameManager.instance.globalScore += int.Parse(scoreText.text);

        SetStars(int.Parse(scoreText.text), 120);
    }

    private void Level2Complete() {
        enemies[0].SetActive(false);
        enemies[1].SetActive(true);
        enemies[2].SetActive(false);

        enemyText.text = GameManager.instance.trollScore.ToString();
        scoreText.text = (10*(GameManager.instance.lifeScore + GameManager.instance.score + GameManager.instance.trollScore)).ToString();

        GameManager.instance.globalScore += int.Parse(scoreText.text);

        SetStars(int.Parse(scoreText.text), 220);
    }

    private void Level3Complete() {
        enemies[0].SetActive(false);
        enemies[1].SetActive(false);
        enemies[2].SetActive(true);
        
        enemyText.text = GameManager.instance.winterScore.ToString();
        scoreText.text = (10*(GameManager.instance.lifeScore + GameManager.instance.score + GameManager.instance.winterScore)).ToString();
        
        GameManager.instance.globalScore += int.Parse(scoreText.text);

        SetStars(int.Parse(scoreText.text), 320);
    }

    private void SetStars(int actualScore, int maxScore) {

        if(actualScore > 0 && actualScore <= maxScore/3) {
            stars[0].SetActive(true);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
            starsCollected = 1;
        }
        if(actualScore > maxScore/3 && actualScore <= maxScore/1.5) {
            stars[0].SetActive(false);
            stars[1].SetActive(true);
            stars[2].SetActive(false);
            starsCollected = 2;
        }
        if(actualScore > maxScore/1.5 && actualScore <= maxScore) {
           stars[0].SetActive(false);
           stars[1].SetActive(false);
           stars[2].SetActive(true);
           starsCollected = 3;
        }
    }

    public void NextButtonLvl1() {

        this.gameObject.SetActive(false);
        LevelMapUI.SetActive(true);
        Debug.Log("starsCollected value is " + starsCollected);
        LevelMapUI.GetComponent<FirstLevelMapController>().InitializeStars(starsCollected);
    }

    public void NextButtonLvl2() {

        this.gameObject.SetActive(false);
        LevelMapUI.SetActive(true);
        Debug.Log("starsCollected value is " + starsCollected);
        LevelMapUI.GetComponent<SecondLevelMapController>().InitializeStars(starsCollected);
    }
}