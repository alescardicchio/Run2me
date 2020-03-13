using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiBlackMovement : MonoBehaviour
{
    public float walkSpeed = 1.5f;
    public bool walkLeft;


    void Start()
    {
        StartCoroutine(changeDirection());
    }


    void Update()
    {
        enemyWalk();
    }

    private void enemyWalk()
    {
        Vector3 temp = transform.position;
        Vector3 tempScale = transform.localScale;
        if (walkLeft)
        {
            temp.x -= walkSpeed * Time.deltaTime;
            tempScale.x = -Mathf.Abs(tempScale.x);
        }
        else
        {
            temp.x += walkSpeed * Time.deltaTime;
            tempScale.x = Mathf.Abs(tempScale.x);
        }

        transform.position = temp;
        transform.localScale = tempScale;
    }

    IEnumerator changeDirection()
    {
        yield return new WaitForSeconds(2.5f);    // Attende un tempo pari a 3 secondi.
        walkLeft = !walkLeft;   // Cambia direzione.
        StartCoroutine(changeDirection());
    }
}
