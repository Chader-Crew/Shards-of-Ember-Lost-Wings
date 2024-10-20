using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentSkill : MonoBehaviour
{
    [SerializeField]private SkillBase skill;
    [SerializeField]private currentSkill prev;
    
    [SerializeField]private int bigCost;
    [SerializeField]private int smallCost;
    [SerializeField]private bool IsSkill;

    public Image image;
    public Button button;
    public List<currentSkill> nexts;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(BuySkill);
        nexts = new List<currentSkill>();
    }

    private void Start()
    {
        prev?.nexts.Add(this);
        if (!prev)
        {
            this.CallWithDelay(UpdateStatus,0.1f);
        }
    }

    private void UpdateStatus()
    {
        if(skill.canCast == true)
        {
           image.enabled=false;
           button.enabled=false;
        }
        else if (prev == null || !prev.image.enabled)
        {
            image.enabled = true;
            image.color = Color.white;
            button.enabled = true;
        }
        else
        {
            image.enabled = true;
            image.color = Color.gray;
            button.enabled=false;
        }

        foreach (currentSkill next in nexts)
        {
            next.UpdateStatus();
        }
    }

    public void BuySkill()
    {
        if(PlayerController.Instance.SkillShards >= bigCost && PlayerController.Instance.StatShards >= smallCost)
        {
            if(prev==null||prev.button.IsActive()==false)
            {
                
                skill.canCast = true;
                skill.Comprado(PlayerController.Instance.character.Stats);
                image.enabled=false;
                button.enabled=false;
                //PlayerController.Instance.SkillShards -= bigCost;
                PlayerController.Instance.SpendShards(smallCost);
                
                UpdateStatus();
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
