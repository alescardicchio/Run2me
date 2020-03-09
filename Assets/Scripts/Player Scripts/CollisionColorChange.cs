using UnityEngine;
using System.Collections.Generic;

using System.Collections;

public class CollisionColorChange : MonoBehaviour
{

    public Color redcolor;
    public Color bluecolor;


    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("RedPaddle"))
        {
            Debug.Log("It's ALIVE and red");
            transform.GetComponent<Renderer>().material.color = redcolor;
        }

        if (collider.gameObject.CompareTag("BluePaddle"))
        {
            Debug.Log("It's ALIVE and blue");
            transform.GetComponent<Renderer>().material.color = bluecolor;
        }
    }
}
