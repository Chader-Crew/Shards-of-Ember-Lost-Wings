using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Espinho", menuName = "Inventory System/Item/Espinho")]
public class Espinho : UsableItem
{
    public override void Use()
    {
        Debug.Log("espinho");
    }
}