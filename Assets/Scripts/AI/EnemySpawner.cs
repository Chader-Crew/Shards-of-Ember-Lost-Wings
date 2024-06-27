using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject arenaExit;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;

    private BoxCollider spawnArea;
    public int currentEnemyCount = 0;

    public bool canSpawn = true;

    void Start()
    {
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
                    float x = Random.Range(114, 142);
                    float z = Random.Range(130, 147);
                    GameObject enemyPrefab = Random.value > 0.5f ? enemyType1 : enemyType2;
                    Instantiate(enemyPrefab, new Vector3 (x, 5.172f, z), Quaternion.identity);
                    currentEnemyCount++;
                }
                StartCoroutine(SpawnEnemies());
                canSpawn = false;
            }

            arenaExit.SetActive(true);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                float x = Random.Range(114, 142);
                float z = Random.Range(130, 147);
                GameObject enemyPrefab = Random.value > 0.5f ? enemyType1 : enemyType2;
                Instantiate(enemyPrefab, new Vector3 (x, 5.172f, z), Quaternion.identity);
                currentEnemyCount++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

