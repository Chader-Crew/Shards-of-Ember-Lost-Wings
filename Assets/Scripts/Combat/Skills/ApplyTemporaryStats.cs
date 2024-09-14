using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adiciona stats temporários com duracao tal. O stat de "hp" causa cura e dano.

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
        context.duration = duration;

        foreach(Character target in context.targets)
        {
            if (atk!=0) target.TemporaryAtk(atk, context.duration);
            if (def!=0) target.TemporaryAtk(def, context.duration);
            if (spd!=0) target.TemporaryAtk(spd, context.duration);
            if (maxHp!=0) target.TemporaryAtk(maxHp, context.duration);

            if (hp !=0)
            { 
                target.restoreLife(hp);
                target.CallWithDelay(() => target.GetHit(new SkillData().Damage(hp).Owner(target)), duration);
            }
        }

        nextSkill.Activate(context);
    }

    public override void Comprado(CharStats player)
    {
        throw new System.NotImplementedException();
    }
}
