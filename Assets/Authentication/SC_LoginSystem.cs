using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SC_LoginSystem : MonoBehaviour
{
    //public enum CurrentWindow { Login, Register }
    //public CurrentWindow currentWindow = CurrentWindow.Login;

    [SerializeField] InputField loginEmail;
    [SerializeField] InputField loginPassword;
    [SerializeField] Button loginButton;
    [SerializeField] Button registerButton;
    [SerializeField] Text errorMessage;
    [SerializeField] InputField registerEmail;
    [SerializeField] InputField registerPassword1;
    [SerializeField] InputField registerPassword2;
    [SerializeField] InputField registerUsername;

    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject registerMenu;
    
    bool isWorking = false;
    bool registrationCompleted = false;
    bool isLoggedIn = false;

    string rootURL = "https://runtome.azurewebsites.net/"; //Path where php files are located

/*
    //Leaderboard
    Vector2 leaderboardScroll = Vector2.zero;
    bool showLeaderboard = false;
    int currentScore = 0; //It's recommended to obfuscate this value to protect against hacking (search 'obfuscation' on sharpcoderblog.com to learn how to do it)
    int previousScore = 0;
    float submitTimer; //Delay score submission for optimization purposes
    bool submittingScore = false;
    int highestScore = 0;
    int playerRank = -1;
    [System.Serializable]
    public class LeaderboardUser
    {
        public string username;
        public int score;
    }
    LeaderboardUser[] leaderboardUsers;

    //Leaderboard
    void Update()
    {
        if (isLoggedIn)
        {
            //Submit score if it was changed
            if (currentScore != previousScore && !submittingScore)
            {
                if (submitTimer > 0)
                {
                    submitTimer -= Time.deltaTime;
                }
                else
                {
                    previousScore = currentScore;
                    StartCoroutine(SubmitScore(currentScore));
                }
            }
            else
            {
                submitTimer = 3; //Wait 3 seconds when it's time to submit again
            }
        }
    }

    void OnGUI()
    {
        //Leaderboard
        if (showLeaderboard)
        {
            GUI.Window(1, new Rect(Screen.width / 2 - 300, Screen.height / 2 - 225, 600, 450), LeaderboardWindow, "Leaderboard");
        }
        if (!isLoggedIn)
        {
            showLeaderboard = false;
            currentScore = 0;
        }
        else
        {
            GUI.Box(new Rect((Screen.width / 2) - 65, 5, 120, 25), currentScore.ToString());

            if (GUI.Button(new Rect(5, 60, 100, 25), "Leaderboard"))
            {
                showLeaderboard = !showLeaderboard;
                if (!isWorking)
                {
                    StartCoroutine(GetLeaderboard());
                }
            }
        }
    }

    //Leaderboard
    void LeaderboardWindow(int index)
    {
        if (isWorking)
        {
            GUILayout.Label("Loading...");
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUI.color = Color.green;
            GUILayout.Label("Your Rank: " + (playerRank > 0 ? playerRank.ToString() : "Not ranked yet"));
            GUILayout.Label("Highest Score: " + highestScore.ToString());
            GUI.color = Color.white;
            GUILayout.EndHorizontal();

            leaderboardScroll = GUILayout.BeginScrollView(leaderboardScroll, false, true);

            for (int i = 0; i < leaderboardUsers.Length; i++)
            {
                GUILayout.BeginHorizontal("box");
                if (leaderboardUsers[i].username == userName)
                {
                    GUI.color = Color.green;
                }
                GUILayout.Label((i + 1).ToString(), GUILayout.Width(30));
                GUILayout.Label(leaderboardUsers[i].username, GUILayout.Width(230));
                GUILayout.Label(leaderboardUsers[i].score.ToString());
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
    }
    */

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToLogin() {
        registerMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void OnRegisterButtonClicked ()
	{
		StartCoroutine (RegisterEnumerator ());
	}

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage.text = "";

        WWWForm form = new WWWForm();
        form.AddField("email", registerEmail.text);
        form.AddField("username", registerUsername.text);
        form.AddField("password1", registerPassword1.text);
        form.AddField("password2", registerPassword2.text);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                errorMessage.text = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    //ResetValues();
                    registrationCompleted = true;
                    Debug.Log("Nuovo utente registrato con successo!");
                    ReturnToLogin();
                }
                else
                {
                    errorMessage.text = responseText;
                }
            }
        }

        isWorking = false;
    }


	public void OnLoginButtonClicked ()
	{
		StartCoroutine (LoginEnumerator ());
	}

    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage.text = "";

        WWWForm form = new WWWForm();
        form.AddField("email", loginEmail.text);
        form.AddField("password", loginPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (!www.isNetworkError)
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    Debug.Log("Loggato con successo!");
                    isLoggedIn = true;
                    GoToMainMenu();
                }
                 else
                {
                    errorMessage.text = responseText;
                }
            }
        }
        isWorking = false;
    }

/*
    void ResetValues()
    {
        errorMessage = "";
        loginEmail = "";
        loginPassword = "";
        registerEmail = "";
        registerPassword1 = "";
        registerPassword2 = "";
        registerUsername = "";
    }
    //Leaderboard
    IEnumerator SubmitScore(int score_value)
    {
        submittingScore = true;

        print("Submitting Score...");

        WWWForm form = new WWWForm();
        form.AddField("email", userEmail);
        form.AddField("username", userName);
        form.AddField("score", score_value);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "score_submit.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                print(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    print("New Score Submitted!");
                }
                else
                {
                    print(responseText);
                }
            }
        }

        submittingScore = false;
    }

    IEnumerator GetLeaderboard()
    {
        isWorking = true;

        WWWForm form = new WWWForm();
        form.AddField("email", userEmail);
        form.AddField("username", userName);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "leaderboard.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                print(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("User"))
                {
                    string[] dataChunks = responseText.Split('|');
                    //Retrieve our player score and rank
                    if (dataChunks[0].Contains(","))
                    {
                        string[] tmp = dataChunks[0].Split(',');
                        highestScore = int.Parse(tmp[1]);
                        playerRank = int.Parse(tmp[2]);
                    }
                    else
                    {
                        highestScore = 0;
                        playerRank = -1;
                    }

                    //Retrieve player leaderboard
                    leaderboardUsers = new LeaderboardUser[dataChunks.Length - 1];
                    for (int i = 1; i < dataChunks.Length; i++)
                    {
                        string[] tmp = dataChunks[i].Split(',');
                        LeaderboardUser user = new LeaderboardUser();
                        user.username = tmp[0];
                        user.score = int.Parse(tmp[1]);
                        leaderboardUsers[i - 1] = user;
                    }
                }
                else
                {
                    print(responseText);
                }
            }
        }

        isWorking = false;
    }
    */
}