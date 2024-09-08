using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateThorns : MonoBehaviour
{
    [SerializeField] private CooldownDisplay cooldownDisplay;
    [SerializeField] private SkillBase skill;
    [SerializeField] private SkillData skillData;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            skillData.Target(other.gameObject.GetComponent<Character>());
            skill.Activate(skillData);
        }
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
