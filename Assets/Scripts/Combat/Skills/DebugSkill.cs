using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Debug")]
public class DebugSkill : SkillBase
{
    public string debugMessage;
    public override void Activate(SkillData context)
    {
        Debug.Log(debugMessage);
    }

    public override void Comprado(CharStats player)
    {
        throw new System.NotImplementedException();
    }
}
