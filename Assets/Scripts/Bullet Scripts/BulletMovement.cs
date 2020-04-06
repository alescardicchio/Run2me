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
        if(target.tag == "EnemyX") {
            target.GetComponent<OtherEnemiesDie>().TakeDamage(AttackDamage);
        }
        if(target.tag == "Enemy") {
            target.GetComponent<RagnoDie>().Die();
        }
        if(target.tag == "Troll")
        {
            target.GetComponent<TrollDie>().Die();
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
}
