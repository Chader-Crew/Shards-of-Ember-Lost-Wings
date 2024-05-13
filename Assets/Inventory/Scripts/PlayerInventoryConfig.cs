using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryConfig : MonoBehaviour
{
    public InventoryManager inventoryManager;

    void OnTriggerEnter(Collider other){
        var item = other.GetComponent<GroundItem>();
        if(item){
            bool result = inventoryManager.AddItem(item.item);
            if(result){
                Destroy(other.gameObject);
            }
        }
    }
}
