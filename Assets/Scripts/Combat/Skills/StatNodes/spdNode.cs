using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatNode/spdNode")]
public class spdNode : SkillBase
{
    [SerializeField]private int  value;
    public override void Activate(SkillData context)
    {
        
    }

    public override void Comprado(CharStats player)
    {
        player.spd+=value;
    }
}