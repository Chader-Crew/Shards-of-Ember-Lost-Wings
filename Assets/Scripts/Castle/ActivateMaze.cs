using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMaze : MonoBehaviour
{

    public GameObject portals;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            portals.SetActive(true);
        }
    }
}
