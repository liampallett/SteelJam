using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float speed = 0f;
    public float jumpForce = 7f; // Jump strength
    public Rigidbody2D myRigidbody;
    public Transform groundCheck; // Assign a child object at player's feet
    public LayerMask groundLayer; // Layer for ground platforms
    public float groundCheckDistance = 0.1f; // Small distance for raycast

    void Update()
    {
        // Horizontal movement
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            speed = 5f;
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            speed = -5f;
        }
        else
        {
            speed = 0f;
        }

        // Jump input
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed && IsGrounded())
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        myRigidbody.linearVelocity = new Vector2(speed, myRigidbody.linearVelocity.y);
    }

    // Check if player is on the ground
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
}
