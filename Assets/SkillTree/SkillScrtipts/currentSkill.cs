using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentSkill : MonoBehaviour
{
    [SerializeField]private SkillBase skill;
    [SerializeField]private Image prev;
    [SerializeField]private Image thisSkill;

    public void BuySkill()
    {
        if(prev.gameObject.activeSelf)
        {
            skill.canCast=true;
        }
    }
}
