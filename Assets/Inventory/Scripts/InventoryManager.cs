using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject mainInventoryPanel;
    
    [Header("Panel de item coletado")]
    public TMP_Text itemTextObj;
    [SerializeField] private Image imageObj;
    public GameObject itemCollected;


    private void Start(){
        instance = this;
        InputReader.OnInventoryInteractEvent += UseItemFromSlot;
        //InputReader.OnPauseEvent += OpenInventory;
    }

    public bool AddItem(Item item){
        //Pra somar os itens
        for(int i = 0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot != null && itemInSlot.item == item){
                itemInSlot.count++;
                itemInSlot.UpdateCount();
                ShowCollected(item);
                return true;
            }
        }

        //Acha um espaço vazio
        for(int i = 0; i < inventorySlots.Length; i++){
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null){
                SpawnNewItem(item, slot);
                ShowCollected(item);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot){
        GameObject newItemObj = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemObj.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);

        // Ajusta a rotação dependendo do slot
        //AdjustItemRotation(inventoryItem, slot);
    }

    public void UseItemFromSlot(int slotIndex){
        if (slotIndex >= 0 && slotIndex < inventorySlots.Length){
            InventorySlot slot = inventorySlots[slotIndex];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null){
                // Chama o método de uso do item
                itemInSlot.UseItem();
            }
        }
    }

    void AdjustItemRotation(InventoryItem inventoryItem, InventorySlot slot){
        if (slot.CompareTag("MainInventorySlot")){
            inventoryItem.transform.rotation = Quaternion.Euler(0, 0, 0);
        }else{
            inventoryItem.transform.rotation = Quaternion.identity;
        }
    }

    /*public void OpenInventory(){
        if(mainInventoryPanel.activeSelf){
            mainInventoryPanel.SetActive(false);
        }else{
            mainInventoryPanel.SetActive(true);
        }
    }*/

    public void ShowCollected(Item item)
    {
        itemCollected.SetActive(true);
        itemTextObj.text = item.itemName + " coletado";
        imageObj.sprite = item.image;

        StartCoroutine("DesactiveCollected");
    }

    IEnumerator DesactiveCollected(){
        yield return new WaitForSeconds(2);
        itemCollected.SetActive(false);
    }
}
