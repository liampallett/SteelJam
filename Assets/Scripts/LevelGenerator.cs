using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs;   // Multiple platform sprites/prefabs
    public int maxPlatforms = 10;

    private List<GameObject> platforms = new List<GameObject>();
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (platforms.Count < maxPlatforms)
        {
            SpawnPlatform();
        }

        CleanupPlatforms();
    }

    void SpawnPlatform()
    {
        float posx = Random.Range(transform.position.x + 4, transform.position.x + 7);
        float posy = Random.Range(transform.position.y - 3, transform.position.y + 3);
        float size = Random.Range(3f, 6.5f);

        // Pick a random platform from the array
        int index = Random.Range(0, platformPrefabs.Length);
        GameObject p = Instantiate(platformPrefabs[index], new Vector2(posx, posy), Quaternion.identity);

        p.transform.localScale = new Vector2(size, 1);
        platforms.Add(p);

        transform.position = new Vector2(posx, posy);
    }

    void CleanupPlatforms()
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.x < mainCam.transform.position.x - 10f)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }
}