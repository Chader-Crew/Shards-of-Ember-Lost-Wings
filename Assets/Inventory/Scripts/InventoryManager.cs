using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject mainInventoryPanel;
    [SerializeField] private InputReader input;

    private void Awake(){
        input.OnInventoryInteractEvent += UseItemFromSlot;
        input.OnPauseEvent += OpenInventory;
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

        // Ajusta a rotação dependendo do slot
        AdjustItemRotation(inventoryItem, slot);
    }

    public void UseItemFromSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < inventorySlots.Length)
        {
            InventorySlot slot = inventorySlots[slotIndex];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            Debug.Log("Usou item " + slotIndex);
            if (itemInSlot != null)
            {
                // Chama o método de uso do item
                itemInSlot.UseItem();
            }
        }
    }

    void AdjustItemRotation(InventoryItem inventoryItem, InventorySlot slot)
    {
        // Ajuste a rotação conforme necessário. Aqui estamos assumindo que slots de toolbar e inventário são diferenciados por tags ou outro identificador.
        if (slot.CompareTag("MainInventorySlot"))
        {
            // Ajusta a rotação para a toolbar
            inventoryItem.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            // Reseta a rotação para o inventário normal
            inventoryItem.transform.rotation = Quaternion.identity;
        }
    }

    public void OpenInventory(){
        if(mainInventoryPanel.activeSelf){
            mainInventoryPanel.SetActive(false);
        }else{
            mainInventoryPanel.SetActive(true);
        }
    }

    /*//definir o botao pra usar cada item e chamar essa funcao nele
    public void UseItem(InventoryItem item){
        if(item.count <= 0){
            Destroy(item.gameObject);
        }else{
            item.count--;
        }

        item.UpdateCount();
    }

    public void GetSlotItem(){

    }*/
}
