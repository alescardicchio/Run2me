using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int AttackDamage = 50;
    public LayerMask enemyLayers;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void Attack() {

        // Avvia l'animazione
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (var enemy in hitEnemies) {
            enemy.GetComponent<OtherEnemiesDie>().TakeDamage(AttackDamage);
        }
    }

    void OnDrawGizmosSelected() {

        if(attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
