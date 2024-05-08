using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]

public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    // Fazendo um save e load próprio do inventário pq o id dos objetos mudam a cada vez que entra no jogo
    // entao nao da pra converter pra Json e voltar

    //A unity nao serializa dicionario, entao tem que fazer manualmente com esse ISerializationCallbackReceiver

    public ItemObject[] Items;
    public Dictionary<ItemObject, int> GetId = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<ItemObject, int>();
        GetItem = new Dictionary<int, ItemObject>();
        for(int i = 0; i < Items.Length; i++){
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize(){}

}
