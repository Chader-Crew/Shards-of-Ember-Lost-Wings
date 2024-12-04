using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    public QuestData quests;
    public Text questNameText;
    public Text questObjectiveText;

     // Atualiza o display com os dados da nova quest
    public void UpdateQuestDisplay(QuestData quest)
    {
        if (quest != null)
        {
            questNameText.text = quest.questName;
            questObjectiveText.text = quest.objective;
        }
        else
        {
            questNameText.text = "Sem mais quests!";
            questObjectiveText.text = "";
        }
    }
}
