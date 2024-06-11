using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Inventory System/Item/Health Potion")]
public class HealthPotion : UsableItem
{
    public int healthRestoreAmount;

    public override void Use()
    {
        //colocar no player instance ou game manager a funcao de aumentar vida, aumentar dano e etc
        //ainda vou fazer tudo relaxa
        Debug.Log("vida restaurada: " + healthRestoreAmount);
    }
}