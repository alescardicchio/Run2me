using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public Transform player;
    private bool playerDead = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(playerIsClose() && !playerDead) {
            Attack();
            animator.SetBool("Attack", true);
        }
        else {
            animator.SetBool("Attack", false);
        }
    }

    private bool playerIsClose() {
        return (Vector2.Distance(transform.position, player.position) < 2.7f) && 
        ((transform.localScale.x > 0 && player.localScale.x < 0) || (transform.localScale.x < 0 && player.localScale.x > 0));
    }

    public void Attack() {

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (var player in hitPlayer) {
            player.GetComponent<PlayerScore>().PlayerDie();
            playerDead = true;
        }
    }

    void OnDrawGizmosSelected() {

        if(attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
