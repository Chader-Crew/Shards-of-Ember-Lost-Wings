using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/ApplyRepeating")]
public class ApplyRepeating : SkillBase
{
    [SerializeField] private SkillBase nextSkill;
    [SerializeField] private int applyCount;
    [SerializeField] private float applyInterval;
    public override void Activate(SkillData context)
    {
        RepeatSkillOnTarget skillRepeater = new GameObject().AddComponent<RepeatSkillOnTarget>();
        skillRepeater.Initialize(nextSkill, context, applyCount, applyInterval);
    }

    public override void Comprado(CharStats player)
    {
        throw new System.NotImplementedException();
    }
}
