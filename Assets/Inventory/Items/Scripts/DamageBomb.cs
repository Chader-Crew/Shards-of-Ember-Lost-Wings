using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage Bomb", menuName = "Inventory System/Item/Damage Bomb")]
public class DamageBomb : UsableItem
{
    public override void Use()
    {
        Debug.Log("bomba de dano");
    }
}