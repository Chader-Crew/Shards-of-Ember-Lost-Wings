using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Inventory System/Item/HealthPotion")]
public class HealthPotion : Item
{
    public int healingAmount = 5;
    /*public override void Usage(){
        //player.heal();
    }*/
}
