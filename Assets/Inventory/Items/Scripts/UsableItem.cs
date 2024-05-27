using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : Item
{
    public abstract void Use();
}

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

[CreateAssetMenu(fileName = "Buff Potion", menuName = "Inventory System/Item/Buff Potion")]
public class BuffPotion : UsableItem
{
    public int damageBuffAmount;

    public override void Use()
    {
        Debug.Log("mais " + damageBuffAmount + " de dano");
    }
}

[CreateAssetMenu(fileName = "Ice Bomb", menuName = "Inventory System/Item/Ice Bomb")]
public class IceBomb : UsableItem
{
    public override void Use()
    {
        Debug.Log("bomba de gelo");
    }
}

[CreateAssetMenu(fileName = "Attraction Bomb", menuName = "Inventory System/Item/Attraction Bomb")]
public class AttractionBomb : UsableItem
{
    public override void Use()
    {
        Debug.Log("bomba de atração");
    }
}

[CreateAssetMenu(fileName = "History Page", menuName = "Inventory System/Item/History Page")]
public class HistoryPage : UsableItem
{
    public override void Use()
    {
        Debug.Log("pagina de historia"); //sei que nao vai estar no inventario 
    }
}