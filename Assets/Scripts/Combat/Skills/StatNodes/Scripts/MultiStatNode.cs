using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatNode/StatNode")]
public class MultiStatNode : SkillBase
{
    [SerializeField] private int maxHP;
    [SerializeField] private int atk;
    [SerializeField] private int def;
    //[SerializeField] private int spd;
    
    public override void Activate(SkillData context)
    {
        
    }

    public override void Comprado(CharStats player)
    {
        player.maxHp += maxHP;
        player.hp += maxHP;
        player.atk += atk;
        player.def += def;
        //player.spd += spd;
    }
}