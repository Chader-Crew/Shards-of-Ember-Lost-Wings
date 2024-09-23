using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    public Transform destination;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Teste"))
        {
            this.transform.position = destination.position;
            Debug.Log("bateu");
        }
    }
}
