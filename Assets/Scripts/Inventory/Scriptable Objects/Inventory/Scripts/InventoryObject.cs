using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemObject _item, int _amount){

        for(int i = 0; i < Container.Count; i++){
            if(Container[i].item == _item){
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(_item, _amount));
    }
}

[System.Serializable] //Serializa e mostra no editor quando está em um objeto da cena
public class InventorySlot{ //O inventario guarda slots
    public ItemObject item;
    public int amount;
    public InventorySlot(ItemObject _item, int _amount){
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value){
        amount += value;
    }
}
