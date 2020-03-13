using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float runSpeed = 4f; 
    //public float maxVelocity = 4f;
    private float jumpForce = 350f;
    private bool isGrounded = false;    // Questa variabile consentirà di stabilire se il giocatore dopo aver saltato la prima volta ha toccato terra, evitando così che continui a saltare quando è già sospeso in aria.
    private bool facingRight = true;

    private Rigidbody2D playerBody;
    private Animator anim;
    public Joystick joystick;
    

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        playerMove();
        playerJump();
    }

    private void playerMove() {
        //float forceX = 0f;
        //float velocity = Mathf.Abs(playerBody.velocity.x);

        //  float horizontalMove = Input.GetAxisRaw("Horizontal");
        float horizontalMove = joystick.Horizontal;

        if(horizontalMove > .4f) {
            //if(velocity < maxVelocity) {
                //forceX = runSpeed;
                playerBody.velocity = new Vector2(runSpeed, playerBody.velocity.y);
            //}
            if(!facingRight) {
                playerFlip();
            }
            anim.SetBool ("Run", true);

        } else if(horizontalMove < -.4f) {
            //if(velocity < maxVelocity) {
              //  forceX = -runSpeed;
              playerBody.velocity = new Vector2(-runSpeed, playerBody.velocity.y);
            //}
            if(facingRight) {
                playerFlip();
            }
            anim.SetBool ("Run", true);

        } else {
            horizontalMove = 0f;
            anim.SetBool("Run", false);
        }

        //playerBody.AddForce(new Vector2(forceX, 0));
    }


    private void playerFlip() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void playerJump() {
        float verticalMove = joystick.Vertical;
        if(verticalMove > .5f) {
            anim.SetBool("Jump", true);
            if(isGrounded) {
                isGrounded = false; // Stiamo per saltare di nuovo, ciò significa che non siamo atterrati a questo punto.     
                playerBody.AddForce(new Vector2(0, jumpForce));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target) {  // Questa funzione verifica che il nostro player collide con un'altro oggetto, il terreno in questo caso.
        if(target.tag == "Ground") {
            isGrounded = true;
            anim.SetBool("Jump", false);
        }
    }
}
