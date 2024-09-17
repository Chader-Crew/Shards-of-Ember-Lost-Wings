using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHUD : MonoBehaviour
{
    [SerializeField]
    private SkillTreeHolder skillTree;
    private Dictionary<SkillBase,CooldownDisplay> cdDisplayDictionary;


    private void Start() 
    {
        //boy i sure do love initializing my variables
        cdDisplayDictionary = new Dictionary<SkillBase, CooldownDisplay>();

        //lista os displays e relaciona cada skill com um pelo dicionario
        CooldownDisplay[] cdDisplays = GetComponentsInChildren<CooldownDisplay>(); 
        for(int i = 0; i< skillTree.skills.Length; i++)
        {
            cdDisplayDictionary.Add(skillTree.skills[i], cdDisplays[i]);
        }

        //inscricao de evento no OnDragonState pra saber quando ligar e desligar
        //precisa acontecer DEPOIS da inscricao do player.
        InputReader.OnDragonStateEvent += PlayerStateUpdate;
        PlayerStateUpdate(0);
    }

    //  desabilita ou abilita os graficos dessa interface, alem de atualizar a inscricao ao OnCast do player.
    //  (a inscricao e desinscricao ao OnCast e para evitar chamadas nulas do dicionario)
    private void PlayerStateUpdate(int stateIndex)
    { 
        //  checa se o state do player e a skillTree dessa interface.
        //  funciona por que a inscricao do player acontece no AWAKE, e a inscricao do SkillHud acontece no START,
        //  por isso o player, que se inscreveu primeiro, sempre muda de modo antes dessa checagem acontecer.
        if(PlayerController.Instance.state == skillTree)
        {
            //atribui o evento de cast e habilita os graficos
            PlayerController.Instance.OnCastEvent += StartSkillCooldown;    
            transform.GetChild(0).gameObject.SetActive(true);
        }else
        {
            PlayerController.Instance.OnCastEvent -= StartSkillCooldown;    
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    //acha um display com a skill tentando ser castada e liga ele
    private void StartSkillCooldown(SkillBase skill)
    {
        skill._onCooldown = true;
        cdDisplayDictionary[skill].StartTimer(skill.cooldown);
        cdDisplayDictionary[skill].CallBack += ()=>{ skill._onCooldown = false; };  //manda o display tirar onCooldown quando acabar
    }
}
