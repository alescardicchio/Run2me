using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffectDestroyed : MonoBehaviour
{
    public float seconds = 1f;

     void Start() {
        StartCoroutine(change());
       
    }
    public IEnumerator change()
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
}
