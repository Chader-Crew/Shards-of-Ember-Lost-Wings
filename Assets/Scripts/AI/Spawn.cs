using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyType1;
    public GameObject enemyType2;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    private BoxCollider spawnArea;
    private int currentEnemyCount = 0;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                Vector3 spawnPosition = GetRandomPositionInArea();
                GameObject enemyPrefab = Random.value > 0.5f ? enemyType1 : enemyType2;
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                currentEnemyCount++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomPositionInArea()
    {
        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.min.y;
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}

