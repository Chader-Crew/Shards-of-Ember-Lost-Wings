using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Inventory System/Item/Health Potion")]
public class HealthPotion : UsableItem
{
    public int healthRestoreAmount;

    public override void Use()
    {
        PlayerController.Instance.character.restoreLife(healthRestoreAmount);
        Debug.Log("vida restaurada: " + healthRestoreAmount);
    }
}