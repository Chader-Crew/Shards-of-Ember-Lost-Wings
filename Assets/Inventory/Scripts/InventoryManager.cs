using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private void Awake(){
        instance = this;
    }

    public bool AddItem(Item item){
        //Pra somar os itens
        for(int i = 0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null && itemInSlot.item == item){
                itemInSlot.count++;
                itemInSlot.UpdateCount();
                return true;
            }
        }

        //Acha um espaço vazio
        for(int i = 0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemObj = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemObj.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    //definir o botao pra usar cada item e chamar essa funcao nele
    public void UseItem(InventoryItem item){
        if(item.count <= 0){
            Destroy(item.gameObject);
        }else{
            item.count--;
        }

        item.UpdateCount();
    }
}
