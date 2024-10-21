using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentSkill : MonoBehaviour
{
    [SerializeField]private SkillBase skill;
    public SkillBase Skill => skill;
    [SerializeField]private currentSkill prev;
    
    [SerializeField]private int bigCost;
    [SerializeField]private int smallCost;
    [SerializeField]private bool IsSkill;
    private Image _imgProp;
    public Image ImgProp 
    {
        get 
        {
            if (!_imgProp) 
            {
                _imgProp = GetComponent<Image>();
            }
            return _imgProp;
        }
    }
    private Button _btnProp;
    public Button BtnProp 
    {
        get {
            if (!_btnProp)
            {
                _btnProp = GetComponent<Button>();
            }
            return _btnProp;
        }
    }
    public List<currentSkill> nexts;
    
    private void Awake()
    {
        BtnProp.onClick.AddListener(BuySkill);
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
           ImgProp.enabled=false;
           BtnProp.enabled=false;
        }
        else if (prev == null || !prev.ImgProp.enabled)
        {
            ImgProp.enabled = true;
            ImgProp.color = Color.white;
            BtnProp.enabled = true;
        }
        else
        {
            ImgProp.enabled = true;
            ImgProp.color = Color.gray;
            BtnProp.enabled=false;
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
            if(prev==null||prev.BtnProp.IsActive()==false)
            {
                
                skill.canCast = true;
                skill.Comprado(PlayerController.Instance.character.Stats);
                ImgProp.enabled=false;
                BtnProp.enabled=false;
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


    public void LoadSkill()
    {
        skill.canCast = true;
        skill.Comprado(PlayerController.Instance.character.Stats);
        ImgProp.enabled = false;
        BtnProp.enabled = false;
        UpdateStatus();
    }
}
