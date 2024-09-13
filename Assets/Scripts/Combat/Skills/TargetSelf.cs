using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/TargetSelf")]
public class TargetSelf : SkillBase
{
    [SerializeField] private SkillBase nextSkill;
    public override void Activate(SkillData context)
    {
        context.Target(context.owner);
        nextSkill.Activate(context);
    }

    public override void Comprado(CharStats player)
    {
        throw new System.NotImplementedException();
    }
}
