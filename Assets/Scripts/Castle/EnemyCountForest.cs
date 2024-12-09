using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountForest : MonoBehaviour
{
    void OnDestroy ()
    {
        EnemySpawner1.instance.AddKillCount();
    }
}
