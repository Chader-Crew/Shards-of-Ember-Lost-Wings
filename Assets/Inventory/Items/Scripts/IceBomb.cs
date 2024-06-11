using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice Bomb", menuName = "Inventory System/Item/Ice Bomb")]
public class IceBomb : UsableItem
{
    public override void Use()
    {
        Debug.Log("bomba de gelo");
    }
}