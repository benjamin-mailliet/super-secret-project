using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded = true;
    private float groundCheckRadius = 0.1f;
    
    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        run();
        jump();
    }

    private void run() {
        var move = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rigidBody.velocity.y);
        print(move);
        print(rigidBody.velocity);
 
        if (move < 0)
            spriteRenderer.flipX = true;
        else if (move > 0)
            spriteRenderer.flipX = false;
    }

    private void jump() {
         if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            isGrounded = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }
}
