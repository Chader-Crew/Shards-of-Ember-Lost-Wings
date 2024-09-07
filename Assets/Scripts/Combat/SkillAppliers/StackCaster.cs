using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;

public class StackCaster : MonoBehaviour
{
    [SerializeField]
    [Header("display nao e outro prefab, tem que ter nesse.")]
    private CooldownDisplay cooldownDisplay;

    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private Vector3 displayOffset;
    private Character target;

    [SerializeField]
    private SkillBase skill;
    private SkillData context;

    private int stacks;

    [SerializeField]
    private int stacksToTrigger;

    public void Initialize(SkillData context) 
    {
        this.context = context;
        target = this.context.targets[0];

        transform.SetParent(target.transform);
        transform.localPosition = displayOffset;

        stacks = 0;
        AddStack();
        cooldownDisplay.CallBack += EndTime;
    }

    private void EndTime()
    {
        Destroy(gameObject);
    }

    public void AddStack()
    {
        stacks++;
        if(stacks >= stacksToTrigger)
        {
            TriggerSkill();
            return;
        }
        text.text = $"{stacks}";
        cooldownDisplay.StartTimer(context.duration);
    }

    private void TriggerSkill()
    {
        skill.Activate(context);
        Destroy(gameObject);
    }
}
