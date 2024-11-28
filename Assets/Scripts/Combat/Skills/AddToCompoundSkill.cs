using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adiciona uma skill a lista de chamadas de uma CompoundSkill.
//Remove depois de certo tempo.
[CreateAssetMenu(menuName = "ScriptableObjects/Skills/AddToCompound")]
public class AddToCompoundSkill : SkillBase
{
    [SerializeField] private CompoundSkill compoundSkill;
    [SerializeField] private SkillBase skillToAdd;
    
    [SerializeField] private float durationOverwrite;   //depois desse tempo, remove a skillToAdd.
													    //quando 0, usa o valor ja no context.

    [SerializeField] private SkillBase nextSkill;
    public override void Activate(SkillData context)
    {
        compoundSkill.nextSkills.Add(skillToAdd);
        if (durationOverwrite != 0) context.duration = durationOverwrite;
        
        context.owner.CallWithDelay(() => compoundSkill.nextSkills.Remove(skillToAdd), context.duration);
        nextSkill?.Activate(context);
    }
}
