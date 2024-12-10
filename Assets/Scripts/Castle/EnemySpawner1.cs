using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{
    public static EnemySpawner1 instance;
    public GameObject[] enemies;
    public GameObject spawnEffectPrefab;
    public float particleEffectDuration = 1f;
    public float spawnInterval = 5f;
    public int maxEnemies = 10;
    public GameObject spawnArea;
    public int currentEnemyCount = 0;
    public bool canSpawn = true;
    public int killCount = 0;
    public GameObject fire;
    public GameObject portal;
    public QuestManager questManager;

    public int enemiesToKill = 5;

    

    void Start()
    {
        instance = this;     
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {    
            if(canSpawn)
            {
                killCount = 0;
                spawnArea.SetActive(true);
                    for (int i = 0; i <2; i++)
                    {
                        Vector3 spawnPosition = GetRandomPositionInArea();
                        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];
                        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);           
                        GameObject particleEffect = Instantiate(spawnEffectPrefab, spawnPosition, Quaternion.identity);
                        Destroy(particleEffect, particleEffectDuration);
                        currentEnemyCount++;
                    }
                StartCoroutine(SpawnEnemies());
                canSpawn = false;
            }

  
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

            GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
            
            GameObject particleEffect = Instantiate(spawnEffectPrefab, spawnPosition, Quaternion.identity);
            Destroy(particleEffect, particleEffectDuration);
            
            currentEnemyCount++;
        }
    }
}

    Vector3 GetRandomPositionInArea()
    {
        Bounds bounds = spawnArea.GetComponent<BoxCollider>().bounds;
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

    public void AddKillCount()
    {
        killCount++;
        if(killCount >= enemiesToKill){
            StopSpawning();
            Destroy(fire);
            Destroy(portal);
            QuestManager.Instance.CompleteCurrentQuest();
        }
    }
}

