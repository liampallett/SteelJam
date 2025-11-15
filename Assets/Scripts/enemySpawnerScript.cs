using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Your Enemy prefab
    public LayerMask groundLayer;        // Ground layer
    public int maxEnemies = 5;           // Maximum number of enemies to maintain
    public float minX = -10f;            // Left boundary of spawn area
    public float maxX = 10f;             // Right boundary of spawn area
    public float maxSpawnHeight = 20f;   // Y value above ground to start raycast

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemiesToMax();
    }

    void Update()
    {
        // Remove destroyed enemies
        enemies.RemoveAll(e => e == null);

        // Spawn new enemies to keep count at maxEnemies
        SpawnEnemiesToMax();
    }

    void SpawnEnemiesToMax()
    {
        while (enemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float spawnX = Random.Range(minX, maxX);
        Vector2 rayOrigin = new Vector2(spawnX, maxSpawnHeight);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, maxSpawnHeight * 2, groundLayer);

        if (hit.collider != null)
        {
            // Spawn enemy on top of the ground
            Vector3 spawnPosition = new Vector3(spawnX, hit.point.y, 0f);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemies.Add(enemy);
        }
        else
        {
            Debug.LogWarning("No ground found at X = " + spawnX);
        }

        Debug.DrawRay(rayOrigin, Vector2.down * maxSpawnHeight * 2, Color.red, 5f);
    }
}
