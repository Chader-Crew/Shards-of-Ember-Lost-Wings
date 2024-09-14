using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnCollision : MonoBehaviour
{
    [SerializeField]private SkillBase skill;
    [SerializeField]private SkillData skillDT;
    public Character character;
    void Start()
    {
        skillDT.owner=character;
    }
    void OnCollisionEnter(Collision other)
    {
        skill.Activate(skillDT);
    }
}
