using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Items/Potion")]
public class PotionObject : ItemObject
{
    public int restoreHealthValue;
    public int strenghtBuffValue;
    public int defenseBuffValue;
    public int speedBonusValue;
    public void Reset(){
        type = ItemType.Potion;
    }
}
