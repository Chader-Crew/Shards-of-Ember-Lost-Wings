using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
   private List<QuestData> questList;
   private int currentQuestIndex = 0; // vai ser o indice da quest atual

   private void Start()
   {
        if (questList.Count > 0)
        {
            ActivateQuest(currentQuestIndex);
        }
        else
        {
            Debug.LogWarning("Nenhuma quest no QuestManager!");
        }
   }

    public void CompleteCurrentQuest()
    {
         if (currentQuestIndex < questList.Count)
        {
            var currentQuest = questList[currentQuestIndex];
            currentQuest.CompleteQuest();

            currentQuestIndex++;

            if (currentQuestIndex < questList.Count)
            {
                ActivateQuest(currentQuestIndex);
            }
            else
            {
                Debug.Log("Todas as quests foram completadas!");
            }
        }
    }

     private void ActivateQuest(int questIndex)
    {
        var quest = questList[questIndex];
        Debug.Log($"Nova quest ativa: {quest.questName} - {quest.description}");
    }
}
