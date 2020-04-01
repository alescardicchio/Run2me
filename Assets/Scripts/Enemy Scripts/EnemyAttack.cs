using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public static bool isAttacking = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("PlayerGirl"))
        {
            anim.SetBool("isAttacking", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("PlayerGirl"))
        {
            anim.SetBool("isAttacking", false);
        }
    }

}
