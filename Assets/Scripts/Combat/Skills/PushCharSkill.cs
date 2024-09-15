using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AVISO RAPIDO DE QUE ISSO VAI BUGAR
//o NPCController que ta lidando com as direcoes e talz e ta parando o movimento extra por invoke.
//sempre que um "movimento extra" acaba, acaba todos os movimentos extras aplicados.
//assumo que nao vai ter muito empurra empurra pra isso virar um problema ENORME mas definitivamente vai
//ser perceptivel em gameplay normal.

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/PushCharacterSkill")]
public class PushCharSkill : SkillBase
{
    [Header("Afeta inimigos por padrao, toggle pra afetar o player no lugar")]
    [SerializeField]
    private bool _targetPlayer;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float pushStrength;

    //ativa essa skill depois de aplicar o efeito onhit para motivos de VFX (e outras coisas vai saber)
    [SerializeField]
    private SkillBase nextSkill;

    public override void Activate(SkillData context)
    {
        if(_targetPlayer)
        {
            throw new NotImplementedException();
        }
        else{
            context.duration = duration;
            foreach(Character character in context.targets)
            {
                //direcao do player ao alvo, com magnitude pushStrength
                Vector3 vector = (character.transform.position - PlayerController.Instance.transform.position).normalized;
                vector *= pushStrength;

                character.GetComponent<NPCController>().MoveInDirectionForSeconds(vector, context.duration);
            }
        }

        nextSkill?.Activate(context);
    }

    public override void Comprado(CharStats player)
    {
        throw new System.NotImplementedException();
    }
}
