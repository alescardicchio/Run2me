using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private GameObject player;
    private PlayerScore playerScore;
    private float minX = 0f, maxX = 143.4f, minY = 0f;
    
    void Start() {
        player = GameObject.FindWithTag("Player");
        playerScore = player.GetComponent<PlayerScore>();
    }

    
    void Update() {
        followPlayer();
    }

    private void followPlayer() {
        if(playerScore.isAlive) {
            Vector3 temp = transform.position;
            temp.x = player.transform.position.x;
            if(temp.x < minX) {
                temp.x = minX;
            } else if(temp.x > maxX) {
                temp.x = maxX;
            }
            temp.y = player.transform.position.y;
            if(temp.y < minY) {
                temp.y = minY;
            }
            //temp.y = player.transform.position.y;

            transform.position = temp;
        }
    }
}
