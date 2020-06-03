using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SC_LoginSystem : MonoBehaviour
{
    public enum CurrentWindow { Login, Register }
    public CurrentWindow currentWindow = CurrentWindow.Login;

    string loginEmail = "";
    string loginPassword = "";
    string registerEmail = "";
    string registerPassword1 = "";
    string registerPassword2 = "";
    string registerUsername = "";
    string errorMessage = "";

    bool isWorking = false;
    bool registrationCompleted = false;
    bool isLoggedIn = false;

    //Logged-in user data
    string userName = "";
    string userEmail = "";

    string rootURL = "https://runtome.azurewebsites.net/"; //Path where php files are located

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
        if (!isLoggedIn)
        {
            if (currentWindow == CurrentWindow.Login)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 125, Screen.height / 2 - 115, 250, 230), LoginWindow, "Login");
            }
            if (currentWindow == CurrentWindow.Register)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 125, Screen.height / 2 - 165, 250, 330), RegisterWindow, "Register");
            }
        }

        GUI.Label(new Rect(5, 5, 500, 25), "Status: " + (isLoggedIn ? "Logged-in Username: " + userName + " Email: " + userEmail : "Logged-out"));
        if (isLoggedIn)
        {
            if (GUI.Button(new Rect(5, 30, 100, 25), "Log Out"))
            {
                isLoggedIn = false;
                userName = "";
                userEmail = "";
                currentWindow = CurrentWindow.Login;
            }
        }

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

    void LoginWindow(int index)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }
        if (registrationCompleted)
        {
            GUI.color = Color.green;
            GUILayout.Label("Registration Completed!");
        }

        GUI.color = Color.white;
        GUILayout.Label("Email:");
        loginEmail = GUILayout.TextField(loginEmail);
        GUILayout.Label("Password:");
        loginPassword = GUILayout.PasswordField(loginPassword, '*');

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(LoginEnumerator());
        }

        GUILayout.FlexibleSpace();

        GUILayout.Label("Do not have account?");
        if (GUILayout.Button("Register", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Register;
        }
    }

    void RegisterWindow(int index)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }

        GUI.color = Color.white;
        GUILayout.Label("Email:");
        registerEmail = GUILayout.TextField(registerEmail, 254);
        GUILayout.Label("Username:");
        registerUsername = GUILayout.TextField(registerUsername, 20);
        GUILayout.Label("Password:");
        registerPassword1 = GUILayout.PasswordField(registerPassword1, '*', 19);
        GUILayout.Label("Password Again:");
        registerPassword2 = GUILayout.PasswordField(registerPassword2, '*', 19);

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(RegisterEnumerator());
        }

        GUILayout.FlexibleSpace();

        GUILayout.Label("Already have an account?");
        if (GUILayout.Button("Login", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Login;
        }
    }

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email", registerEmail);
        form.AddField("username", registerUsername);
        form.AddField("password1", registerPassword1);
        form.AddField("password2", registerPassword2);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    ResetValues();
                    registrationCompleted = true;
                    currentWindow = CurrentWindow.Login;
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }

    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email", loginEmail);
        form.AddField("password", loginPassword);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    userName = dataChunks[1];
                    userEmail = dataChunks[2];
                    isLoggedIn = true;

                    ResetValues();
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }

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
}