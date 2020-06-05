using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharactersController : MonoBehaviour
{
    public GameObject boy;
    public GameObject girl;
    
    public void playWithBoy() {
        girl.SetActive(false);
        boy.SetActive(true);
        SceneManager.LoadScene("gameLvl1");     
    }

 

    public void playWithGirl() {
        boy.SetActive(false);
        girl.SetActive(true);
        SceneManager.LoadScene("gameLvl1");
    }
}