using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adiciona stats temporários com duracao tal. 
//O stat de "hp" causa cura e, no final da duracao, dano.


[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Temporary Stats")]
public class ApplyTemporaryStats : SkillBase
{
    [SerializeField] private float duration;

    [SerializeField] private int atk;
    [SerializeField] private int def;
    [SerializeField] private int spd;
    [SerializeField] private int maxHp;
    [SerializeField] private float hp;

    [SerializeField] private SkillBase nextSkill;
    

    public override void Activate(SkillData context)
    {
        //guarda a duracao por motivos de VFX (e qualquer outro efeito subsequente que precise sincronizar com o termino do buff.)
        context.duration = duration;

        foreach(Character target in context.targets)
        {
            if (atk!=0) target.TemporaryAtk(atk, context.duration);
            if (def!=0) target.TemporaryDef(def, context.duration);
            if (spd!=0) target.TemporarySpd(spd, context.duration);
            if (maxHp!=0) target.TemporaryMaxHp(maxHp, context.duration);

            if (hp !=0)
            { 
                target.restoreLife(hp);
                target.CallWithDelay(() => target.GetHit(new SkillData().Damage(hp).Owner(target)), duration);
            }
        }

        nextSkill?.Activate(context);
    }

}
