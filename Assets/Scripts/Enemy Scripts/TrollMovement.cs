using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollMovement : MonoBehaviour
{

    public float walkSpeed = 1.5f;
    public bool walkLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        trollWalk();
    }
    
    private void trollWalk()
    {
        Vector3 temp = transform.position;
        Vector3 tempScale = transform.localScale;
        if(walkLeft)
        {
            temp.x += walkSpeed * Time.deltaTime;
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else
        {
            temp.x -= walkSpeed * Time.deltaTime;
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.position = temp;
        transform.localScale = tempScale;
    }

    IEnumerator changeDirection()
    {
        yield return new WaitForSeconds(3f);    // Attende un tempo pari a 3 secondi.
        Flip();   // Cambia direzione.
        StartCoroutine(changeDirection());
    }

    public void Flip() {
        walkLeft = !walkLeft;
    }
}
