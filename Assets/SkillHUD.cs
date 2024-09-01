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

        //inscreve no OnCast do player TODO: inscrever e desinscrever com a mudança de modo
        PlayerController.Instance.OnCastEvent += StartSkillCooldown;
    }

    //acha um display com a skill tentando ser castada e liga ele
    private void StartSkillCooldown(SkillBase skill)
    {
        try{
            cdDisplayDictionary[skill].StartTimer(skill.cooldown);
            cdDisplayDictionary[skill].CallBack += ()=>{ skill._onCooldown = false; };  //manda o display tirar onCooldown quando acabar

        }
        catch{
            Debug.LogWarning("Player cast a skill with non-0 cooldown that has no corresponding CooldownDisplay.");

        }
    }
}
