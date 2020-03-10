using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public void Die() {
        // Qui si potrebbe implementare qualche animazione per la 'morte' del nemico.
        // Al momento scomparirà semplicemente dalla scena.
        Destroy(gameObject);
    }
}
