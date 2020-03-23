using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void Quit() {
        Application.Quit();
        // DA QUI POTREBBE ANDARE AL MENU IN CUI SALVA LE STATS DELLA PARTITA..
    }

    public void Retry() {
        SceneManager.LoadScene("gameLvl1");
        GameManager.instance.playerDied_GameRestarted = false;
    }
}
