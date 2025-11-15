using UnityEngine;

public class Platform : MonoBehaviour
{
    public float destroyDistance = 10f; // distance behind player before platform is destroyed

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // If platform is far below the player, destroy it
        if (player.transform.position.y - transform.position.y > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}