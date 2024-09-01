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
        //lista os displays e relaciona cada skill com um pelo dicionario
        CooldownDisplay[] cdDisplays = GetComponentsInChildren<CooldownDisplay>(); 
        for(int i = 0; i< skillTree.skills.Length; i++)
        {
            cdDisplayDictionary.Add(skillTree.skills[i], cdDisplays[i]);
        }
    }
}
