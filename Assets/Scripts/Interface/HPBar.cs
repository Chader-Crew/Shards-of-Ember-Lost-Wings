using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Character character;
    
    private void Awake()
    {
        character.OnGotHitEvent += (_,_) => UpdateBar();
        character.OnHealEvent += (_) => UpdateBar();
    }

    public void UpdateBar()
    {
        slider.value = character.Stats.hp/ character.Stats.maxHp;
    }

}
