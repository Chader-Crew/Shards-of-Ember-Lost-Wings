using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public bool forest = false;
    public bool castle = true;
    void OnDestroy ()
    {
        if(castle){
        EnemySpawner.instance.AddKillCount();
        }
        else{
            EnemySpawner1.instance.AddKillCount();
        }
    }
}
