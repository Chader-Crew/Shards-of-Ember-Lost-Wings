using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParent : MonoBehaviour
{
    public QuestObjective questObjective;

    public void Initialize(QuestObjective questObjective){
        this.questObjective = questObjective;
    }
}
