using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatNode/atkNode")]
public class atkNode : SkillBase
{
    [SerializeField]private int  value;
    public override void Activate(SkillData context)
    {
        
    }

    public override void Comprado(CharStats player)
    {
        player.atk+=value;
    }
}
