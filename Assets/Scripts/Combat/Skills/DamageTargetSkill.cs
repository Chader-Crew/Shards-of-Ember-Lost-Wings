using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//causa um valor de dano fixo em todos os personagens na lista targets.
[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Damage Targets")]
public class DamageTargetSkill : SkillBase
{
    [Tooltip("Valor de dano a ser dado, nao leva em consideracao ataque do dono nem outros modificadores.")]
    [SerializeField] private float damage;
    [SerializeField] private float stagger;
    public override void Activate(SkillData context)
    {
        context.damage = damage;
        context.stagger = stagger;
        foreach (Character target in context.targets)
        {
            target.GetHit(context);
        }
    }
}
