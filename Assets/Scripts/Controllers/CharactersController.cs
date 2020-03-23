using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharactersController : MonoBehaviour
{
    public GameObject boy;
    public GameObject girl;
    public GameObject TutorialUI;
    
    public void playWithBoy() {
        girl.SetActive(false);
        boy.SetActive(true);
        TutorialUI.GetComponent<TutorialController>().startTutorial();
        SceneManager.LoadScene("gameLvl1");     
    }

    public void playWithGirl() {
        boy.SetActive(false);
        girl.SetActive(true);
        TutorialUI.GetComponent<TutorialController>().startTutorial();
        SceneManager.LoadScene("gameLvl1");
    }

}
