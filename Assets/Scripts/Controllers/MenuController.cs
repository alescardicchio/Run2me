using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame() {
        GameManager.instance.playerDied_GameRestarted = false;
        SceneManager.LoadScene("gameLvl1");
    }

    public void ShowLeaderBoard() {
        SceneManager.LoadScene("Scoreboard");
    }
}
