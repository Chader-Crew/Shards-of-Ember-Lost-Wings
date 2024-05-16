using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ativa cada uma das skills subsequentes em ordem.
[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Compound Skill")]
public class CompoundSkill : SkillBase
{
    public List<SkillBase> nextSkills;  //Lista de skills a serem ativadas.
    public override void Activate(SkillData context)
    {
        foreach (SkillBase skill in nextSkills)
        {
            SkillData nextContext = context;    //eu sei que ta errado isso aqui, tenho que conversar com o roque ou alguma coisa
            skill.Activate(nextContext);
        }
    }
}
