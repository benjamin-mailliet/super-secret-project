using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour, PlayerActionAsset.IPlayerActions
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

    private Gamepad gamepad;
    private Vector2 move;

    PlayerActionAsset playerActions;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerActions = new PlayerActionAsset();
        playerActions.Player.SetCallbacks(this);
    }

    public void OnEnable() {
        Debug.Log("Enabling player controls!");
        playerActions.Player.Enable();
    }

    public void OnDisable() {
        Debug.Log("Disabling player controls!");
        playerActions.Player.Disable();
    }

    void Update() {
        Move();
    }

    void FixedUpdate() {        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (doJump) {
            rigidBody.velocity = new Vector2(move.x * speed, jumpForce);
            doJump = false;
        } else {
            rigidBody.velocity = new Vector2(move.x * speed, rigidBody.velocity.y);
        }
    }

    private void Move() {
        if (move.x < 0)
            spriteRenderer.flipX = true;
        else if (move.x > 0)
            spriteRenderer.flipX = false;
    }

    public void OnMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) {
        Debug.Log("JUMP");
        if (isGrounded && context.performed) {
            doJump = true;
        }
    }
}