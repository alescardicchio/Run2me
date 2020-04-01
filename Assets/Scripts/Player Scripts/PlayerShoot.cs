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

    public void playerShoot(bool shooting) {
        if(shooting) {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            FindObjectOfType<AudioManager>().Play("Shoot");
            anim.SetBool("Shoot", true);    
        }
        else {
            anim.SetBool("Shoot", false);
        }
    }
}
