using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public HeartManager heartManager;

    void OnDestroy()
    {
        
        heartManager.HeartDestroyed();
    }
}
