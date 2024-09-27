using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaExit : MonoBehaviour
{
    public GameObject exits;
   void OnTriggerEnter(Collider other) 
   {
    if(other.CompareTag("Player"))
    {
        Debug.Log("bateu");
        EnemySpawner.instance.StopSpawning();
        EnemySpawner.instance.canSpawn = true;
        EnemySpawner.instance.currentEnemyCount = 0;
        EnemySpawner.instance.spawnArea.SetActive(false);
        exits.SetActive(false);
    }
   }
}
