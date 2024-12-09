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
    public GameObject blockIMG;
    
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
        if(skill.canCast)
        {
           ImgProp.color = Color.gray;
           BtnProp.enabled=false;
           blockIMG.SetActive(false);
        }
        else if (prev == null || prev.Skill.canCast)
        {
            ImgProp.color = Color.white;
            BtnProp.enabled = true;
            blockIMG.SetActive(false);
        }
        else
        {
            ImgProp.color = Color.gray;
            BtnProp.enabled=false;
            blockIMG.SetActive(true);
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
        BtnProp.enabled = false;
        UpdateStatus();
    }
}
