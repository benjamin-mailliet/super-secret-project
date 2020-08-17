using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private float moveAxis; 
    private bool doJump = false;
    private bool isGrounded = true;
    private float groundCheckRadius = 0.1f;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    
    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Move();
        Jump();
    }

    void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (doJump) {
            rigidBody.velocity = new Vector2(moveAxis * speed, jumpForce);
            doJump = false;
        } else {
            rigidBody.velocity = new Vector2(moveAxis * speed, rigidBody.velocity.y);
        }
    }

    private void Move() {
        moveAxis = Input.GetAxis("Horizontal");
        if (moveAxis < 0)
            spriteRenderer.flipX = true;
        else if (moveAxis > 0)
            spriteRenderer.flipX = false;
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !doJump) {
            doJump = true;
        }
    }
}