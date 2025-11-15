using JetBrains.Annotations;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 5.0f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;
    public int jumpCount = 2;
    public float dashCooldown = 0f;
    private float dashTime;
    public float dashSpeed;
    private bool isDashing;
    public LayerMask groundLayer;  // Ground layer mask to check for floor

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
{
    // Check if the player is on the ground
    CheckIfGrounded();

    // Horizontal movement
    float movementInput = Input.GetAxis("Horizontal");

    // Normal movement when not dashing
    if (!isDashing)
    {
        rb.linearVelocity = new Vector2(movementInput * movementSpeed, rb.linearVelocity.y);  // Keep y velocity for jumping and falling
    }

    // Jumping
    if (Input.GetButtonDown("Jump"))
    {
        if (isGrounded || jumpCount == 2)
        {
            Jump();
            jumpCount = 1;
        }
        else if (jumpCount == 1)
        {
            Jump();
            jumpCount = 0;
        }
    }

    // Dash cooldown logic
    dashCooldown -= Time.deltaTime;  // Countdown the dash cooldown
    if(dashCooldown < 0)
    {
        dashCooldown = 0;
    }
    
    // Dash logic (if player presses LeftShift and dash is ready)
    if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0f && !isDashing)
    {
        // Dash in the direction of movement (positive for right, negative for left)
        if (movementInput != 0f)
        {
            isDashing = true;  // Start dashing
            dashCooldown = 1f;  // Reset cooldown
            dashTime = 0.4f;  // Set dash duration (adjust as needed)
            rb.linearVelocity = new Vector2(movementInput > 0f ? dashSpeed : -dashSpeed, rb.linearVelocity.y);  // Dash in the horizontal direction
        }
    }

    // Handle dash duration
    if (isDashing)
    {
        dashTime -= Time.deltaTime;
        if (dashTime <= 0f)
        {
            isDashing = false;  // End dash after duration
        }
    }
}


    void CheckIfGrounded()
    {
        // Raycast to check if the bottom of the player is grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        
        // Visualize the ray for debugging in Scene view
        Debug.DrawRay(transform.position, Vector2.down * 0.3f, Color.red);

        // Check if the ray hit something in the ground layer
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);  // Only change the y velocity for jumping
    }
}
