using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{ //Tipo do item
    EnemyDrop,
    Potion,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject slotPrefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}
