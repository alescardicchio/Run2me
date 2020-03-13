using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Debug.Log("Quitting");
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
