using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New EnemyDrop Object", menuName = "Inventory System/Items/EnemyDrop")]
public class EnemyDropObject : ItemObject
{
    public void Reset(){
        type = ItemType.EnemyDrop;
    }
}
