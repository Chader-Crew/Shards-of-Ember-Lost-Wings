using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Espinho", menuName = "Inventory System/Item/Espinho")]
public class Espinho : UsableItem
{
    public float poisonBuffDuration = 5f; //duracao buff de veneno

    public override void Use()
    {
        PlayerController.Instance.character.ActivatePoisonBuff(poisonBuffDuration); //timer do veneno
    }
}