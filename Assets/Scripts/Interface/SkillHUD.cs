using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillHUD : MonoBehaviour
{
    [SerializeField] private SkillTreeHolder skillTree;
    private Dictionary<SkillBase,CooldownDisplay> cdDisplayDictionary;

    [SerializeField] private GameObject selectArrow;        //seta indicando a skill que sera selecionada
    private int selectBuffer;   //ultima direcao de input do eixo, selecionara essa skill ao soltar o stick

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

        skillTree.SetActiveSkill(0);
        //inscricao de evento no OnDragonState pra saber quando ligar e desligar
        //precisa acontecer DEPOIS da inscricao do player.
        InputReader.OnDragonStateEvent += PlayerStateUpdate;
        InputReader.OnSkillSelectEvent += SkillSelectPreview;
        InputReader.OnConfirmSkillEvent += ChangeSkillSelection;
        PlayerStateUpdate(0);
    }

    //  desabilita ou abilita os graficos dessa interface, alem de atualizar a inscricao ao OnCast do player.
    //  (a inscricao e desinscricao ao OnCast e para evitar chamadas nulas do dicionario)
    private void PlayerStateUpdate(int stateIndex)
    { 
        //  checa se o state do player e a skillTree dessa interface.
        //  funciona por que a inscricao do player acontece no AWAKE, e a inscricao do SkillHud acontece no START,
        //  por isso o player, que se inscreveu primeiro, sempre muda de modo antes dessa checagem acontecer.
        //Debug.Log("SHUD Player Update");
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

    //ativa e atualiza o indicador de seleção de skill
    private void SkillSelectPreview(int i)
    {
        selectArrow.SetActive(true);

        //rotaciona a seta de acordo com a direcao
        switch(i){
        case 0: selectArrow.transform.rotation = Quaternion.Euler(0,0,180); break;
        case 1: selectArrow.transform.rotation = Quaternion.Euler(0,0,-90); break;
        case 2: selectArrow.transform.rotation = Quaternion.Euler(0,0,90); break;
        case 3: selectArrow.transform.rotation = Quaternion.Euler(0,0,0); break;
        }
        selectBuffer = i;
    }

    //efetua a troca da skill selecionada e desabilita o preview
    private void ChangeSkillSelection()
    {
        selectArrow.SetActive(false);
        
        //bem gambiarrado mas isso vai pegar uma imagem no pai do cooldownDisplay e trocar a ativacao da que estava selecionado para a nova selecao.
        cdDisplayDictionary[skillTree.ActiveSkill].transform.parent.GetComponent<Image>().enabled = false;
        skillTree.SetActiveSkill(selectBuffer);
        cdDisplayDictionary[skillTree.ActiveSkill].transform.parent.GetComponent<Image>().enabled = true;
    }
}
