using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondLevelMapController : MonoBehaviour 
{
    public Animator sndLvlAnim;
    public Animator thrdLvlAnim;
    public Animator arrowsAnim;
    public GameObject[] lvl1Stars;

    void Start() {
        lvl1Stars[GameManager.instance.lvl1Stars].SetActive(true);
    }

    public void InitializeStars(int starsCollected) {
        if(starsCollected == 1) {
            sndLvlAnim.SetTrigger("Set1Star");
        }
        if(starsCollected == 2) {
            sndLvlAnim.SetTrigger("Set2Star");
        }
        if(starsCollected == 3) {
            sndLvlAnim.SetTrigger("Set3Star");
        }
        StartCoroutine(UnlockArrow());
    }

    public IEnumerator UnlockArrow()
    {
        yield return new WaitForSeconds(3.5f);
        arrowsAnim.SetTrigger("SetArrow2");
        StartCoroutine(UnlockNextLevel());
    }
    
    public IEnumerator UnlockNextLevel() {
        yield return new WaitForSeconds(1.5f);    
        sndLvlAnim.SetTrigger("UnlockLvl3");
    }

    public void WinterWorldButton() {
        SceneManager.LoadScene("WinterLand");
    }
}
