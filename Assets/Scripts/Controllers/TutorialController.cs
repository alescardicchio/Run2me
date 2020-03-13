using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialMenu;
    public GameObject touchscreenMenu;
    public GameObject scoreMenu;

    void Start() {
        if(!GameManager.instance.playerDied_GameRestarted) {
            startTutorial();
        }
        else {
            tutorialMenu.SetActive(false);
        }
    }

    public void startTutorial() {
        tutorialMenu.SetActive(true);
        touchscreenMenu.SetActive(false);
    }

    public void show2ND_DiamondsLives() {

    }
  
    public void show3RD_JoystickShootPause() {
        touchscreenMenu.SetActive(true);
        scoreMenu.SetActive(false);
    }

    public void show4TH() {
        touchscreenMenu.SetActive(false);
    }

    public void StartGame() {
        touchscreenMenu.SetActive(true);
        scoreMenu.SetActive(true);
        tutorialMenu.SetActive(false);
    }

}
