using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour, PlayerActionAsset.IPlayerActions
{

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Gamepad gamepad;
    PlayerActionAsset playerActions;

    private float moveAxis;

    // Move
    [SerializeField] float speed;
    private Vector2 moveDirection;

    // Jump
    [SerializeField] float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private bool doJump = false;
    private bool isGrounded = true;
    private float groundCheckRadius = 0.1f;

    // Dash
    private bool doDash = false;
    private float dashSpeed = 200f;
    private float dashDuration = 0.05f;
    private float dashCurrentDuration = 0f;
    private float dashCooldown = 0.4f;
    private float dashCurrentCooldown = 0f;
    private Vector2 facingDirection;
    private ParticleSystem particleSystem;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        playerActions = new PlayerActionAsset();
        playerActions.Player.SetCallbacks(this);
    }

    public void OnEnable()
    {
        Debug.Log("Enabling player controls!");
        playerActions.Player.Enable();
    }

    public void OnDisable()
    {
        Debug.Log("Disabling player controls!");
        playerActions.Player.Disable();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Move();
        Jump();
        Dash();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (doDash) {
            // lors du dash, on set le moveDirection à 0 pour éviter que le personnage bouge tout seul ensuite
            moveDirection = Vector2.zero;
        } else {
            moveDirection = context.ReadValue<Vector2>();
            facingDirection = (moveDirection != Vector2.zero) ? moveDirection : facingDirection;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!doDash && isGrounded && context.performed) {
            doJump = true;
        }     
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!doDash && dashCurrentCooldown <= 0) {
            doDash = true;
            dashCurrentCooldown = dashCooldown;
            particleSystem.Play();
        }
    }

    private void Move()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * speed, rigidBody.velocity.y);

        if (moveDirection.x < 0)
            spriteRenderer.flipX = true;
        else if (moveDirection.x > 0)
            spriteRenderer.flipX = false;
    }

    private void Jump()
    {
        if (doJump)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }

        doJump = false;
    }

    private void Dash()
    {
        if (doDash)
        {
            if (dashCurrentDuration >= dashDuration)
            {
                particleSystem.Stop();
                doDash = false;
                dashCurrentDuration = 0;
            }
            else
            {
                rigidBody.velocity = new Vector2(facingDirection.x * dashSpeed, 0);
                dashCurrentDuration += Time.fixedDeltaTime;
            }
        }

        dashCurrentCooldown -= Time.fixedDeltaTime;
    }
}