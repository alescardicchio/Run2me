using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    public bool isAlive;
    private Rigidbody2D playerBody;
    private Animator anim;
    public GameObject AttackUI;
    public GameObject ShootUI;
    public GameObject LevelCompleteUI;
    public GameObject SubmittingText;
    public GameObject TouchscreenUI;
    public GameObject ScoresUI;
    
    string userEmail = "run2me@gmail.com";
    string userName = "Run2me";
    string rootURL = "https://runtome.azurewebsites.net/"; //Path where php files are located

    void Awake() {
        anim = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
        isAlive = true;
    }

    void Update() {
        // Se il player esce fuori dalla mappa, muore:
        if(transform.position.y <= -5f) {
            if(isAlive) {
                isAlive = false;
                FindObjectOfType<AudioManager>().Play("OhNo");
                GameplayController.instance.decrementLife();
                transform.position = new Vector3(0, 100000, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target) {

        // Se il player è a contatto con un diamante:
        if (target.tag == "Collectable") {
            GameplayController.instance.incrementScore(); // Incrementa di 1 i diamanti raccolti.
            FindObjectOfType<AudioManager>().Play("Diamante");
            target.gameObject.SetActive(false); // Quando il player 'tocca' il diamante, questo viene rimosso dalla scena.
        }

        // Se il player è a contatto con un' arma:
        if (target.tag == "Gun") {
            AttackUI.SetActive(false);
            ShootUI.SetActive(true);
            target.gameObject.SetActive(false);
        }

        //Se il player è entrato a contatto con un nemico:
        if(target.tag == "Enemy") {
            PlayerDie();
        }

        // Se il player calpesta una delle prime delle piattaforme.
        if(target.tag == "Ground_Timer") {
            TimerController.instance.BeginTimer();
            Debug.Log("Timer partito!");
        }

        // Se il player ha raggiunto l'uscita.
        if(target.tag == "Exit") {
            TimerController.instance.EndTimer();
            LevelCompleteUI.SetActive(true);
            TouchscreenUI.SetActive(false);
            ScoresUI.SetActive(false);
            LevelCompleteUI.GetComponent<LevelController>().ShowLevelDialog();
        }

        // Se il player è nell'ultimo livello ed ha raggiunto l'uscita.
        if(target.tag == "EndGame") {
            TouchscreenUI.SetActive(false);
            ScoresUI.SetActive(false);
            LevelCompleteUI.SetActive(true);
            LevelCompleteUI.GetComponent<LevelController>().ShowLevelDialog();
            StartCoroutine(SubmitScore(GameManager.instance.globalScore));
        }
    }

    public void loseSubmitScore() {
        StartCoroutine(SubmitScore(GameManager.instance.globalScore));
        SubmittingText.SetActive(true);
        StartCoroutine(Waiting());
    }

     public IEnumerator Waiting() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayerDie() {
        anim.SetTrigger("Dead");
        if(isAlive) {
            isAlive = false;    // Se il player tocca il nemico, muore.
            GameplayController.instance.decrementLife();
        }
    }


        IEnumerator SubmitScore(int score_value)
    {
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
}
