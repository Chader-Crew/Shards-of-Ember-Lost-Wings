using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//causa um valor de dano em todos os personagens na lista targets.
[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Damage Targets")]
public class DamageTargetSkill : SkillBase
{
    [Header("Essa Skill Requer Target")]
    [Space(15)]
    [Tooltip("Valor de dano PURO. Nao escala com ataque.")]
    [SerializeField] private float damage;

    [Tooltip("Valor de dano baseado em ataque. Cada ponto de ataque adiciona esse valor ao dano.")]
    [SerializeField] private float perAtkDamage;

    [Tooltip("Valor de dano baseado em defesa. Cada ponto de defesa adiciona esse valor ao dano.")]
    [SerializeField] private float perDefDamage;

    [Tooltip("Valor de interrupcao. Em personagens pequenos, cada ponto e 1 segundo de stagger.")]
    [SerializeField] private float stagger;

    public override void Activate(SkillData context)
    {
        //calculo de dano
        context.damage = 
                damage +
                context.owner.Stats.atk * perAtkDamage +
                context.owner.Stats.def * perDefDamage
                ;
        
        context.stagger = stagger;
        foreach (Character target in context.targets)
        {
            target.GetHit(context);
        }
    }
}
