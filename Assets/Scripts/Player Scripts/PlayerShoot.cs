using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    private Animator anim;
    public Button fire;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        /*if(Input.GetButtonDown("Fire1")) {
            anim.SetBool("Shoot", true);    
            StartCoroutine(ExecuteAfterTime(0.03f));
        } else {
            anim.SetBool("Shoot", false);
        }*/
    }

    public void playerShoot(bool shooting) {
        if(shooting) {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            anim.SetBool("Shoot", true);    
            //StartCoroutine(ExecuteAfterTime(0.03f));
        }
        else {
            anim.SetBool("Shoot", false);
        }
    }

    /*IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);
        playerShoot(shooting);
    }*/
}
