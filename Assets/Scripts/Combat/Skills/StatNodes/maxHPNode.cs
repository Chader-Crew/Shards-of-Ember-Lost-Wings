using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatNode/maxHPNode")]
public class maxHPNode : SkillBase
{
    [SerializeField]private int  value;
    public override void Activate(SkillData context)
    {
        
    }

    public override void Comprado(CharStats player)
    {
        player.maxHp+=value;
        player.hp+=(player.maxHp-player.hp);
    }
}