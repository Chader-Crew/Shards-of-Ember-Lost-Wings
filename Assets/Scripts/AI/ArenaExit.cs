using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaExit : MonoBehaviour
{
   void OnTriggerEnter(Collider other) 
   {
    if(other.CompareTag("Player"))
    {
        Debug.Log("bateu");
        EnemySpawner.instance.canSpawn = true;
        gameObject.SetActive(false);
    }
   }
}
