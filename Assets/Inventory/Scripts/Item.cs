using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory System/Item")]
public class Item : ScriptableObject
{
    [Header("Item Description")]
    public string itemName;
    [TextArea(10,10)]
    public string description;
    public ItemType type;

    public Sprite image;

}

public enum ItemType{
    Bomb,
    Potion,
    EnemyDrop
}
