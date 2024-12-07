using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.playerMovement.characterController.enabled = false;
            PlayerController.Instance.transform.position = destination.position;
            PlayerController.Instance.playerMovement.characterController.enabled = true;
        }
    }
}
