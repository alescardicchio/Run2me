using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int lifeScore;
    public int spiderScore;
    public int trollScore;
    public int winterScore;
    public bool playerDied_GameRestarted;
    public bool tutorialDone;
    
    void Awake() {
        MakeSingleton();
    }

    private void MakeSingleton() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
