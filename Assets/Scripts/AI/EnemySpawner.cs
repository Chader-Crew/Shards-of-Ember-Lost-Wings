using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject spawnEffectPrefab;
    public float particleEffectDuration = 1f;
    public GameObject arenaExit;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    private BoxCollider spawnArea;
    public int currentEnemyCount = 0;

    public bool canSpawn = true;

    void Start()
    {
        instance = this;
        spawnArea = GetComponent<BoxCollider>();       
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {    
            if(canSpawn)
            {
                for (int i = 0; i <5; i++)
                {
                    Vector3 spawnPosition = GetRandomPositionInArea();
                    GameObject enemyPrefab = Random.value > 0.5f ? enemyType1 : enemyType2;
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    GameObject particleEffect = Instantiate(spawnEffectPrefab, spawnPosition, Quaternion.identity);
                    Destroy(particleEffect, particleEffectDuration);
                    currentEnemyCount++;
                }
                StartCoroutine(SpawnEnemies());
                canSpawn = false;
            }

            arenaExit.SetActive(true);
        }
    }

   public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (currentEnemyCount < maxEnemies)
            {
                Vector3 spawnPosition = GetRandomPositionInArea();
                GameObject enemyPrefab = Random.value > 0.5f ? enemyType1 : enemyType2;
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                GameObject particleEffect = Instantiate(spawnEffectPrefab, spawnPosition, Quaternion.identity);
                Destroy(particleEffect, particleEffectDuration);
                currentEnemyCount++;
            }
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

    public void StopSpawning()
    {
        Debug.Log("parou");
        StopAllCoroutines();
    }
}

