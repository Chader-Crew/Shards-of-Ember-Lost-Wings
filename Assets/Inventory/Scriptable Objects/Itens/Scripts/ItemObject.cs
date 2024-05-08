using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{ //Tipo do item
    EnemyDrop,
    Potion,
    Bomb

    /* poção de vida
    buff de força
    bomba de atração de inimigo - atrai o inimigo pro lugar do estouro
    bomba de gelo - congela os inimigos 
    cauda do magago - buff de veneno
    pó da gárgula - buff de resistência, imune a stagger
    */

}

public abstract class ItemObject : ScriptableObject
{
    public GameObject slotPrefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}
