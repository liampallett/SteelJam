using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 5.0f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;
    public int jumpCount = 2;

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
        float movementInput = Input.GetAxis("Horizontal") * movementSpeed;
        rb.linearVelocity = new Vector2(movementInput, rb.linearVelocity.y);  // Keep y velocity for jumping and falling

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
