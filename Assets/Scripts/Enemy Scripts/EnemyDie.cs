using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    //private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
      //  anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Die()
    {
        //anim.SetBool("Die", true);
        //transform.gameObject.tag = "Untagged";
        //Destroy(gameObject, 1.25f);
        Destroy(gameObject);
    }
}
