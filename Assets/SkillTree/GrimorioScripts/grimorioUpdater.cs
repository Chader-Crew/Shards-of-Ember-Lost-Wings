using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grimorioUpdater : MonoBehaviour
{
    [SerializeField]private SkillTreeHolder fireTree;
    [SerializeField]private SkillTreeHolder thunderTree;
    [SerializeField]private SkillTreeHolder greenTree;
    [SerializeField]private Button[] grimorioButtons;
    [SerializeField]private GameObject image;

    void Start()
    {
        //reinicia o cooldown das skills (pq o awake do scriptable object nao acontece quando da play, so quando loada)
        foreach(SkillBase skill in fireTree.skills){ skill._onCooldown = false; }
        foreach(SkillBase skill in thunderTree.skills){ skill._onCooldown = false; }
        foreach(SkillBase skill in greenTree.skills){ skill._onCooldown = false; }
    }
    public void AttGrimorio()
    {   if(fireTree!=null)
        {
            for(int j=0; j<fireTree.skills.Length;j++)
            {
                if(fireTree.skills[0].canCast==true)
                {
                    grimorioButtons[0].interactable=true;
                }
            }

        }
        
    }
    public void AssignSkill()
    {
        if(image.activeSelf==true)
        {
            fireTree.UnSetSkill();
            image.SetActive(false);
        }
        else
        {
            image.SetActive(true);
            fireTree.SetActiveSkill(0);
        }
    }
}
