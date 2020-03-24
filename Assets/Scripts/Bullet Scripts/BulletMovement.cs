using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    
    public float speed = 20f;
    public Rigidbody2D rigidbody;
    public GameObject impactEffect;

    void Start() {
        rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Enemy") {
            target.GetComponent<EnemyDie>().Die();
        }
        if(target.tag == "Troll")
        {
            target.GetComponent<TrollDie>().Die();
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
}
