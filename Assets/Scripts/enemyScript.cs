using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D myRigidbody;
    public float speed = 2f;
    public float chaseDistance = 10f;
    public float stopDistance = 1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;
    public float lookAheadDistance = 3f; // how far to check for platforms
    public int forwardRays = 5; // number of rays to cast for platform detection
    public float jumpGravity = 9.81f; // gravity used to calculate jump height

    void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        float currentSpeed = 0f;
        if (distance <= chaseDistance && distance > stopDistance)
        {
            currentSpeed = speed;
        }

        // Move horizontally
        myRigidbody.linearVelocity = new Vector2(direction.x * currentSpeed, myRigidbody.linearVelocity.y);

        // Flip sprite when moving
        if (currentSpeed != 0)
        {
            if (direction.x > 0) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0) transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

        // Jump if player is above and a platform is reachable
        if (IsGrounded() && player.position.y > transform.position.y + 0.1f)
        {
            Vector2? platformTarget = GetClosestPlatformToPlayer();
            if (platformTarget.HasValue)
            {
                float verticalDistance = platformTarget.Value.y - transform.position.y;
                float jumpVelocity = Mathf.Sqrt(2f * jumpGravity * verticalDistance);
                myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, jumpVelocity);
            }
        }
    }

    // Check if enemy is on ground
    bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    // Cast multiple rays forward to find the closest platform toward the player
    Vector2? GetClosestPlatformToPlayer()
    {
        float closestDistance = Mathf.Infinity;
        Vector2? targetPlatform = null;

        for (int i = 0; i < forwardRays; i++)
        {
            Vector2 rayOrigin = groundCheck.position + new Vector3(transform.localScale.x * i * (lookAheadDistance / forwardRays), 0);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, lookAheadDistance, groundLayer);

            if (hit.collider != null)
            {
                float distanceToPlayer = Mathf.Abs(hit.point.x - player.position.x);
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    targetPlatform = hit.point;
                }
            }

            // Draw debug rays
            Debug.DrawRay(rayOrigin, Vector2.down * lookAheadDistance, Color.blue);
        }

        return targetPlatform;
    }

}
