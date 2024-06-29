using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeUI : Singleton<SkillTreeUI>
{
    [SerializeField] private TMP_Text smallShardText;
    [SerializeField] private TMP_Text bigShardText;

    private void OnEnable() 
    {
        smallShardText.text = PlayerController.Instance.statShards.ToString();
        bigShardText.text = PlayerController.Instance.skillShards.ToString();
    }
}
