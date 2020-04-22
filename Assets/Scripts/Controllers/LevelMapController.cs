using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMapController : MonoBehaviour 
{
    public Animator fstLvlAnim;
    public Animator sndLvlAnim;
    public Animator arrowsAnim;

    public void InitializeStars(int starsCollected) {
        if(starsCollected == 1) {
            fstLvlAnim.SetTrigger("Set1Star");
        }
        if(starsCollected == 2) {
            fstLvlAnim.SetTrigger("Set2Star");
        }
        if(starsCollected == 3) {
            fstLvlAnim.SetTrigger("Set3Star");
        }
        StartCoroutine(UnlockArrow());
    }

    public IEnumerator UnlockArrow()
    {
        yield return new WaitForSeconds(3.4f);
        arrowsAnim.SetTrigger("SetArrow1");
        StartCoroutine(UnlockNextLevel());
    }
    
    public IEnumerator UnlockNextLevel() {
        yield return new WaitForSeconds(1.3f);    
        sndLvlAnim.SetTrigger("UnlockLvl2");
    }

    public void ForestWorldButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
