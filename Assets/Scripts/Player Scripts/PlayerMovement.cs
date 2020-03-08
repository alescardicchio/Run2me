using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float runSpeed = 30f, maxVelocity = 4f;
    private float jumpForce = 350f;
    private bool isGrounded = false;    // Questa variabile consentirà di stabilire se il giocatore dopo aver saltato la prima volta ha toccato terra, evitando così che continui a saltare quando è già sospeso in aria.

    private Rigidbody2D playerBody;
    private Animator anim;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playerMove();
        playerJump();
    }

    private void playerMove() {
        float forceX = 0f;
        float velocity = Mathf.Abs(playerBody.velocity.x);

        float horizontalMove = Input.GetAxisRaw("Horizontal");

        if(horizontalMove > 0) {
            if(velocity < maxVelocity) {
                forceX = runSpeed;
            }

            Vector3 temp = transform.localScale;    // Consente di mantenere il player rivolto nel verso giusto quando corre.
            temp.x = 0.4f;  // Evita dunque che sia rivolto a destra mentre corre a sinistra.
            transform.localScale = temp;

            anim.SetBool ("Run", true);

        } else if(horizontalMove < 0) {
            if(velocity < maxVelocity) {
                forceX = -runSpeed;
            }

            Vector3 temp = transform.localScale;    // Consente di mantenere il player rivolto nel verso giusto quando corre.
            temp.x = -0.4f;  // Evita dunque che sia rivolto a destra mentre corre a sinistra.
            transform.localScale = temp;

            anim.SetBool ("Run", true);

        } else {
            anim.SetBool("Run", false);
        }

        playerBody.AddForce(new Vector2(forceX, 0));
    }

    private void playerJump() {
        if(Input.GetKey(KeyCode.Space)) {
            if(isGrounded) {
                isGrounded = false; // Stiamo per saltare di nuovo, ciò significa che non siamo atterrati a questo punto.     
                playerBody.AddForce(new Vector2(0, jumpForce));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target) {  // Questa funzione verifica che il nostro player collide con un'altro oggetto, il terreno in questo caso.
        if(target.tag == "Ground") {
            isGrounded = true;
        }
    }
}
