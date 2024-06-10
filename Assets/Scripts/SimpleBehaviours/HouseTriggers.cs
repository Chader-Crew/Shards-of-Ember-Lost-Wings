using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTriggers : MonoBehaviour
{
    public GameObject houseTop;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            houseTop.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            houseTop.SetActive(true);
        }
    }
}
