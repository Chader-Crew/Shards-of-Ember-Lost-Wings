using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath = "/inventory.save";
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    //pra poder funiconar quando buildar e sem precisar passar o database no inspetor
    private void OnEnable(){
    #if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Databse.asset", typeof(ItemDatabaseObject));
    #else
        database = Resources.Load<ItemDatabaseObject>("Database");
    #endif
    }

    public void AddItem(ItemObject _item, int _amount){

        for(int i = 0; i < Container.Count; i++){
            if(Container[i].item == _item){
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(/*database.GetId[_item],*/ _item, _amount));
    }

    //save e load nao tem erro, mas nao ta funcionando ainda

    public void Save(){
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load(){
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    //erro ??? parou de dar erro dps que dei play e sai do play
    public void OnAfterDeserialize()
    {
        for(int i = 0; i < Container.Count; i++){
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize(){} //dependencia, nao apagar
}

[System.Serializable] //Serializa e mostra no editor quando está em um objeto da cena
public class InventorySlot{ //O inventario guarda slots
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(/*int _id,*/ ItemObject _item, int _amount){
        //ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value){
        amount += value;
    }
}
