using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherEnemiesDie : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;
    private Animator animator;
    public GameObject deathEffect;

    void Start() {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage) {
        
        Debug.Log("Danno preso: "+damage);

        animator.SetTrigger("Hurt");

        currentHealth -= damage;

        Debug.Log("Salute dopo: "+currentHealth);

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {

        if(gameObject.tag == "Troll") {
            GameManager.instance.trollScore++;
            GameObject deathEff = Instantiate(deathEffect, transform.position, Quaternion.identity);
            deathEff.transform.localScale = new Vector3(5.6f, 5.6f, 1f);
        }
        else if(gameObject.tag == "Enemy") {
            if(GameObject.Find("Canvas/Touchscreen/Shoot").activeSelf) {
                GameObject deathEff = Instantiate(deathEffect, transform.position, Quaternion.identity);
                deathEff.transform.localScale = new Vector3(4f, 4f, 1f);
            }    
            GameManager.instance.spiderScore++;
        }
        
        Destroy(gameObject);
    }
}
