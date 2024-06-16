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

    public SkillBase activeSkill;

    public string nameState;

    public void Enter()
    {
        Debug.Log(nameState);
    }

}
