using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffectDestroyed : MonoBehaviour
{

    public float seconds = 1f;

     void Start() {
        StartCoroutine(change());
       
    }
    public IEnumerator change()
    {
        // wait for 3 seconds
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
}
