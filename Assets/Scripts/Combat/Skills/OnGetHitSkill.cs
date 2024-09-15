using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adiciona uma skill para ser triggerada sempre que o target leva dano,
//Remove do evento depois de um tempo.

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/On Get Hit Skill")]
public class OnGetHitSkill : SkillBase
{
    [SerializeField] private float duration;
    
    //adiciona uma ativacao dessa skill ao OnGetHit do target.
    [SerializeField] private SkillBase skillToAdd;
    
    //ativa essa skill depois de aplicar o efeito onhit para motivos de VFX
    [SerializeField] private SkillBase nextSkill;

    public override void Activate(SkillData context)
    {
        //se a duracao daqui nao for maior que zero, usa a que ta no context.duration.
        if(duration > 0) {context.duration = duration;}

        foreach(Character target in context.targets)
        {
            //adiciona uma chamada ao evento, e depois de um tempo remove.
            target.OnGotHitEvent += EventSubscriber;
            target.CallWithDelay(() => target.OnGotHitEvent -= EventSubscriber, context.duration);

            //funcao local que se inscreve e remove
            void EventSubscriber(SkillData data, float damage)
            {
                if(data.owner != target)
                {
                    //target aqui e o personagem levando dano, data.owner e o personagem causando o dano, damage e o dano que levou.
                    //ativa a skill passando uma nova skilldata: owner quem levou dano, target quem deu dano, e o dano levado fica na variavel damage.
                    skillToAdd.Activate(new SkillData().Owner(target).Target(data.owner).Damage(damage));
                }

            };
        }

        nextSkill?.Activate(context);
    }

    public override void Comprado(CharStats player)
    {
        throw new System.NotImplementedException();
    }
}
