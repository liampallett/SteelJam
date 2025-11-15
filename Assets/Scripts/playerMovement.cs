using System.Diagnostics;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 5.0f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement
        float movementInput = Input.GetAxis("Horizontal") * movementSpeed;
        rb.linearVelocity = new Vector2(movementInput, rb.linearVelocityY);

        // jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
