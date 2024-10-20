using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/AddStackCaster")]
public class AddStackCaster : SkillBase
{
    [SerializeField] private StackCaster prefab;
    [SerializeField] private float duration;
    public override void Activate(SkillData context)
    {
        if(!canCast) { return; }
        if(context.targets.Count <=0 ) { return; }

        context.duration = duration;
        
        StackCaster stacker = context.targets[0].GetComponentInChildren<StackCaster>();
        
        if(stacker == null)
        {
            stacker = Instantiate(prefab);
            stacker.Initialize(context);
        }else
        {
            stacker.AddStack();
        }
    }

}
