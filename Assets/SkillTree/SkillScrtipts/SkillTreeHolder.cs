using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/SkillTreeHold")]
public class SkillTreeHolder: ScriptableObject
{
    public SkillBase[] skills;

    public Sprite stateIMG;
    public SkillTreeHolder prev;
    public SkillTreeHolder next;

    [SerializeField]
    private SkillBase _activeSkill; //variavel interna
    public SkillBase ActiveSkill    //encapsulei a activeSkill pra poder controlar a troca internamente
    {
        get => _activeSkill;
    }   
    
    public string nameState;

    public Mesh ArmorMesh;
    public Mesh BodyMesh;
    
    public bool _CanCastSkill
    {
        get
        {
            if (!ActiveSkill.canCast) return false;
            else
            if(ActiveSkill._onCooldown) return false;
            else
            if(ActiveSkill == null) return false;
            
            
            return true;
        }
    }

    public void ResetSkills()
    {
        foreach (SkillBase skill in skills)
        {
            skill.canCast = false;
        }
    }


    public void Enter()
    {
        Debug.Log(nameState);
    }

    public void SetActiveSkill(int i)
    {
        _activeSkill = skills[i];
    }

    public void UnSetSkill()
    {
        _activeSkill = null;
    }
}
