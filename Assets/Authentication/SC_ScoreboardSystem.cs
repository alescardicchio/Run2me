using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SC_ScoreboardSystem : MonoBehaviour
{
    [SerializeField] GameObject[] entry;
    
    string rootURL = "https://runtome.azurewebsites.net/"; //Path where php files are located


    //Leaderboard
    //Vector2 leaderboardScroll = Vector2.zero;
    //bool showLeaderboard = false;
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

    string userEmail = "mela.danza@hotmail.it";
    string userName = "mela";

    void Start() { 
        StartCoroutine(GetLeaderboard());  
    }

    void Update() {
        printScores();
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

/*
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
*/
    /*
    public void postScore() {
        if(GameManager.instance.gameEnd) {
            StartCoroutine(SubmitScore(GameManager.instance.globalScore));
        }
    }

    
    IEnumerator SubmitScore(int score_value)
    {
        submittingScore = true;

        Debug.Log("Submitting Score...");

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
                    Debug.Log("New Score Submitted!");
                }
                else
                {
                    print(responseText);
                }
            }
        }
    }
    */

    public void printScores() {
        for(int i=0; i<leaderboardUsers.Length; i++) {
            entry[i].SetActive(true);
            entry[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = (i+1).ToString();
            entry[i].gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = leaderboardUsers[i].username;
            entry[i].gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = leaderboardUsers[i].score.ToString();
        }
    }

    IEnumerator GetLeaderboard()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", userEmail);
        form.AddField("username", userName);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "leaderboard.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
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
                        Debug.Log(leaderboardUsers[i-1].username);
                        Debug.Log("Score dell'utente preso!");
                        Debug.Log("Ecco lo score dell'utente: "+user.score);
                    }
                }
                else
                {
                    Debug.Log(responseText);
                }
            }
        }
    }
}
