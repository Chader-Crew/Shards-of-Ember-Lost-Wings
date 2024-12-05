using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/FirstTalkQuest")]
public class FirstTalkQuest : QuestObjective
{
    public override void Objective(){
        Debug.Log("falar com solas");
        //descrever quais os requisitos da sua quest
    }
}
