using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    
    public float speed = 20f;
    public Rigidbody2D rigidbody;
    public GameObject impactEffect;
    public int AttackDamage = 50;

    void Start() {
        rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Enemy" || target.tag == "Troll") {
            Debug.Log("Proiettile: 'Nemico colpito'");
            target.GetComponent<OtherEnemiesDie>().TakeDamage(AttackDamage);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
}
