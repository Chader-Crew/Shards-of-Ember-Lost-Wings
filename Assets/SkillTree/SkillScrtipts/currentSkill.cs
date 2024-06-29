using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentSkill : MonoBehaviour
{
    [SerializeField]private SkillBase skill;
    [SerializeField]private Button prev;

    [SerializeField]private int bigCost;
    [SerializeField]private int smallCost;

    public void BuySkill()
    {
        if(PlayerController.Instance.skillShards >= bigCost && PlayerController.Instance.statShards >= smallCost)
        {
            if(prev==null||prev.IsActive()==false)
            {
                skill.canCast=true;
                skill.Comprado(PlayerController.Instance.character.Stats);
                this.gameObject.GetComponent<Image>().enabled=false;
                this.gameObject.GetComponent<Button>().enabled=false;
                PlayerController.Instance.skillShards -= bigCost;
                PlayerController.Instance.statShards -= smallCost;
            }
            else
            {
                return;
            }
            //pc.UPDATESKILLS();
        }
        else
        {
            return;
        }
    }
}
