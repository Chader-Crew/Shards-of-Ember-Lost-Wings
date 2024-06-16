using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentSkill : MonoBehaviour
{
    [SerializeField]private SkillBase skill;
    [SerializeField]private Button prev;
    [SerializeField]private Image thisSkill;

    [SerializeField]private  CharStats player;

    public void BuySkill()
    {
        if(prev==null||prev.IsActive()==false)
        {
            skill.canCast=true;
            skill.Comprado(player);
            this.gameObject.GetComponent<Image>().enabled=false;
            this.gameObject.GetComponent<Button>().enabled=false;
        }
    }
}
