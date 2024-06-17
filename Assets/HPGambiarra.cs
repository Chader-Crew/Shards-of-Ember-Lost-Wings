using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPGambiarra : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private void Start() 
    {
        Debug.Log("subscribe");
        PlayerController.Instance.character.OnGotHitEvent += UpdateBar;
    }

    private void UpdateBar(SkillData data, float number)
    {
        Debug.Log("update");
        slider.value = PlayerController.Instance.character.Stats.hp/ PlayerController.Instance.character.Stats.maxHp;
    }
}
