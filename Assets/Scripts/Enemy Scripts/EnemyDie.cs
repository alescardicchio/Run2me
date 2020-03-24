using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public void Die() {
        Destroy(gameObject);
    }
}
