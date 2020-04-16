using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    public Text timeCounter;
    private System.TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;

    void Awake() {
        instance = this;
    }

    void Start() {
        timerGoing = false;
    }

    public void BeginTimer() {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer() {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer() {
        while(timerGoing) {
            elapsedTime += Time.deltaTime;
            timePlaying = System.TimeSpan.FromSeconds(elapsedTime);
            timeCounter.text = timePlaying.ToString("mm':'ss'.'ff");

            yield return null;
        }
    }
}
