using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateThorns : MonoBehaviour
{
    [SerializeField] private CooldownDisplay cooldownDisplay;
    [SerializeField] private SkillBase skill;
    [SerializeField] private SkillData skillData;
    [SerializeField] private float damagePerSecond = 0.3f;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Character enemy = other.gameObject.GetComponent<Character>();
            skillData.Target(enemy);
            enemy.stats.hp -= damagePerSecond; 
            skill.Activate(skillData);
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Character enemy = other.gameObject.GetComponent<Character>();
            enemy.stats.hp -= damagePerSecond * Time.deltaTime; 
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Character enemy = other.gameObject.GetComponent<Character>();
            if (enemy == null)
            {
                skill.Activate(skillData); //preciso de uma lógica aqui pra desativar a skill, ou fazer que o inimigo nao tome dano fora da area dela
            }
        }
    }
}
