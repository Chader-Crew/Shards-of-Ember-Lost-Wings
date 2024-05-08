using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Bomb Object", menuName = "Inventory System/Items/Bomb")]
public class BombObject : ItemObject
{
    // sobreescrevemos o Reset()
    public void Reset(){ //Reset() pq quando cria um ScriptableObject a Unity chama um Reset() e muda tudo que foi escrito dentro de um Awake()
        type = ItemType.Bomb;
    }
}
