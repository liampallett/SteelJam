using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float speed = 2f;
    public Transform fallDetection;
    public float groundCheckDistance = 1f;

    public float directionOfGuy = 1;

    void Update()
    {
        // Check if there's ground ahead
        RaycastHit2D hit = Physics2D.Raycast(fallDetection.position, Vector2.down, groundCheckDistance);
        Debug.DrawRay(fallDetection.position, Vector2.down * groundCheckDistance, Color.yellow);

        if (hit.collider == false)
        {
            directionOfGuy = -directionOfGuy;
        }
    }

    void FixedUpdate()
    {
        // Move horizontally
        myRigidbody.linearVelocity = new Vector2(directionOfGuy * speed, myRigidbody.linearVelocity.y);

        // Flip sprite to face moving direction
        transform.localScale = new Vector3(-directionOfGuy, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy both enemies if they collide
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            GameObject.Find("GOCanvas").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
