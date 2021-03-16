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
    float speed = 30;
    float decreaseSpeedOnJump = 1;
    private Vector2 moveDirection;

    // Jump
    float jumpForce = 50;
    [SerializeField] RectTransform groundCheck;
    [SerializeField] LayerMask groundLayer;
    private bool doJump = false;
    private bool isGrounded = true;

    // Wall jump
    [SerializeField] RectTransform wallCheckRight;
    [SerializeField] RectTransform wallCheckLeft;
    [SerializeField] LayerMask wallLayer;
    private bool isWalledRight = false;
    private bool isWalledLeft = false;
    private int wallJumpVelocity = 30;
    private float wallJumpDuration = 0.1f;
    private float wallJumpCurrentDuration = 0f;

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
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheck.rect.size, 1, groundLayer);
        isWalledRight = Physics2D.OverlapBox(wallCheckRight.position, wallCheckRight.rect.size, 1, wallLayer);
        isWalledLeft = Physics2D.OverlapBox(wallCheckLeft.position, wallCheckLeft.rect.size, 1, wallLayer);
        Move();
        Jump();
        Dash();
        WallSlide();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (doDash)
        {
            // FIX : lors du dash, on set le moveDirection à 0 pour éviter que le personnage se déplace tout seul ensuite
            moveDirection = Vector2.zero;
        }
        else
        {
            moveDirection = context.ReadValue<Vector2>();
            facingDirection = (moveDirection != Vector2.zero) ? moveDirection : facingDirection;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale >= 1.0f)
        {
            doJump = true;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!doDash && dashCurrentCooldown <= 0 && Time.timeScale >= 1.0f)
        {
            doDash = true;
            dashCurrentCooldown = dashCooldown;
            particleSystem.Play();
        }
    }

    private void Move()
    {
        if (wallJumpCurrentDuration > 0)
        {
            wallJumpCurrentDuration -= Time.fixedDeltaTime;
        }
        else
        {
            if (!isGrounded && moveDirection == Vector2.zero)
            {
                float xDirection;
                if (rigidBody.velocity.x > 0)
                {
                    xDirection = Mathf.Clamp(rigidBody.velocity.x - decreaseSpeedOnJump, 0, speed);
                }
                else
                {
                    xDirection = Mathf.Clamp(rigidBody.velocity.x + decreaseSpeedOnJump, -speed, 0);
                }
                rigidBody.velocity = new Vector2(xDirection, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(moveDirection.x * speed, rigidBody.velocity.y);
            }

            if (moveDirection.x < 0)
                spriteRenderer.flipX = true;
            else if (moveDirection.x > 0)
                spriteRenderer.flipX = false;
        }
    }

    private void Jump()
    {
        if (!doDash && doJump && (isGrounded || isWalledLeft || isWalledRight))
        {
            Vector2 newVelocity = new Vector2(rigidBody.velocity.x, jumpForce);
            if (isWalledLeft)
            {
                newVelocity.x = wallJumpVelocity;
                wallJumpCurrentDuration = wallJumpDuration;
            }
            else if (isWalledRight)
            {
                newVelocity.x = -wallJumpVelocity;
                wallJumpCurrentDuration = wallJumpDuration;
            }

            rigidBody.velocity = newVelocity;
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

    private void WallSlide()
    {
        if ((isWalledLeft || isWalledRight) && rigidBody.velocity.y < 0)
        {
            rigidBody.drag = 35;
        }
        else
        {
            rigidBody.drag = 0;
        }
    }

}