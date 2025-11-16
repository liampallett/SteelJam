using UnityEngine;

public class singleSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnHeightOffset = 2f;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = transform.position + Vector3.up * spawnHeightOffset;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}