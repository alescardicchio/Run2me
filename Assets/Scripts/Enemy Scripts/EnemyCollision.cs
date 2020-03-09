using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    float speed = 2.0f;
    GameObject player;
    GameObject enemy;

    void OnCollisionEnter(Collision collision)
    {
        //Se c'è collisione con un corpo rigido
        if (collision.rigidbody != null)
        {
            //Se la collisione è con il player
            if (collision.rigidbody.name == "Player")
            {
                //allora distruggi il nemico
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        //Il nemico cerca il player
        GameObject _p = GameObject.Find("Player");
        //Se l'ha trovato, definiscilo
        if (_p != null)
        {
            player = _p;
        }
    }
}
