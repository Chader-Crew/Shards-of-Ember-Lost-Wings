using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeUI : Singleton<SkillTreeUI>
{
    [SerializeField] private TMP_Text smallShardText;
    //[SerializeField] private TMP_Text bigShardText;
    [SerializeField] private GameObject skillTreePanel;

    private void OnEnable() 
    {
        AttUI();
    }

    public void AttUI()
    {
        smallShardText.text = PlayerController.Instance.StatShards.ToString();
        //bigShardText.text = PlayerController.Instance.SkillShards.ToString();
    }

    public void ShowUI(bool value)
    {
        skillTreePanel.SetActive(value);
    }

    public void ToggleUI()
    {
        ShowUI(!skillTreePanel.activeSelf);
    }
}
