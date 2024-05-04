using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Codigo tambem temporario mas ainda pode ser usado
public class PlayerInventoryConfig : MonoBehaviour
{
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other){
        var item = other.GetComponent<Item>();
        if(item){
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    /* O Inventario por scriptable object mantem as informações mesmo depois
    que voce sai do Play.
    Pra isso nao acontecer usamos um CLear assim que saimos do Play
    */
    private void OnApplicationQuit(){
        inventory.Container.Clear();
    }
}
