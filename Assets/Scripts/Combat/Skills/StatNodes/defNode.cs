using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/StatNode/defNode")]
public class defNode : SkillBase
{
    [SerializeField]private int  value;
    public override void Activate(SkillData context)
    {
        
    }

    public override void Comprado(CharStats player)
    {
        player.def+=value;
    }
}
