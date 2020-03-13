using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollMovement : MonoBehaviour
{

    public float walkSpeed = 1.5f;
    public bool walkLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trollWalk();
    }
    
    private void trollWalk()
    {
        Vector3 temp = transform.position;
    }
}
