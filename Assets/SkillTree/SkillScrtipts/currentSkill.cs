using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentSkill : MonoBehaviour
{
    [SerializeField]private SkillBase skill;
    [SerializeField]private Button prev;

    [SerializeField]private  CharStats player;
    [SerializeField]PlayerController pc;

    public void BuySkill()
    {
        if(pc.skillpoints>0)
        {
            if(prev==null||prev.IsActive()==false)
            {
                skill.canCast=true;
                skill.Comprado(player);
                this.gameObject.GetComponent<Image>().enabled=false;
                this.gameObject.GetComponent<Button>().enabled=false;
            }
            else
            {
                return;
            }
            pc.skillpoints--;
            pc.UPDATESKILLS();
        }
        else
        {
            return;
        }
    }
}
