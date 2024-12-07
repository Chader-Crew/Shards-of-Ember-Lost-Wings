using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && (HeartManager.instance.destroyableCrystal == true))
        {
            HeartManager.instance.HeartDestroyed();
            HeartManager.instance.destroyableCrystal = false;
            Destroy(gameObject);
        }
    }
}
