using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory System/Item")]
public class Item : ScriptableObject
{
    [Header("Item Description")]
    public string name;
    [TextArea(10,10)]
    public string description;
    public ItemType type;

    public Sprite image;

    //public abstract void Usage();
}

public enum ItemType{
    Bomb,
    Potion,
    EnemyDrop
}
