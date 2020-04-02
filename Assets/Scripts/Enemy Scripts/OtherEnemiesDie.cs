using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherEnemiesDie : MonoBehaviour
{

    public static int maxHealth = 100;
    public int currentHealth;
    
    public HealthBar healthBar;

    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage) {
        
        Debug.Log("Health prima:"+currentHealth);
        Debug.Log("Damage taken: "+damage);

        currentHealth -= damage;

        Debug.Log("Health dopo: "+currentHealth);

        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {

        // QUI SI POTREBBE AGGIUNGERE L'ANIMAZIONE PER LA MORTE DEL NEMICO..
        
        Destroy(gameObject);
    }
}
