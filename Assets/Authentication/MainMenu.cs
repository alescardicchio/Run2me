using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text playerDisplay;
    // Start is called before the first frame update
    void Start()
    {
        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "player: " + DBManager.username;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
