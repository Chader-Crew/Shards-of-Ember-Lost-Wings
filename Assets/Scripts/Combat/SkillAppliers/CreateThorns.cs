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
            enemy.GetHit(new SkillData().Damage(damagePerSecond).Owner(PlayerController.Instance.character));   // substituido a atribuicao de hp por dano pelo metodo do character -Alu
            skill.Activate(skillData);
        }
        //Destroy(gameObject);  // movida a responsabilidade de destruir ele depois de x tempo pra skill de SpawnPrefab (funciona considerando que o tempo que dura sempre e o mesmo) -Alu
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Character enemy = other.gameObject.GetComponent<Character>();
            //enemy.stats.hp -= damagePerSecond * Time.deltaTime;               // Dps nao devia rodar todo frame por que conflita com o nosso feedback de dano e nao fica boa leitura -Alu
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
