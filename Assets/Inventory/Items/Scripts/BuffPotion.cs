using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff Potion", menuName = "Inventory System/Item/Buff Potion")]
public class BuffPotion : UsableItem
{
    public int damageBuffAmount;

    public override void Use()
    {
        PlayerController.Instance.character.TemporaryAtk(damageBuffAmount,6);
        //Debug.Log("mais " + damageBuffAmount + " de dano");
    }
}