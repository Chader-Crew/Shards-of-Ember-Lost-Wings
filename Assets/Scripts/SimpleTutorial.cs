using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTutorial : MonoBehaviour
{
    public GameObject panelTutorial;

    void Awake()
    {
        panelTutorial.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            Debug.Log("Colidiu");
            panelTutorial.SetActive(true);
        }
    }
}