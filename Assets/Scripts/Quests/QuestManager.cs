using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public List<QuestData> questList; // Lista de quests
    public QuestDisplay questDisplay;      
    private int currentQuestIndex = 0; // vai ser o indice da quest atual
    public bool _altarIsActive;
    public QuestData GetCurrentQuest()
    {
        return questList[currentQuestIndex];
    }

    void Start()
    {
        if (questList.Count > 0)
        {
            ActivateQuest(currentQuestIndex);
        }
        else
        {
            Debug.LogWarning("Nenhuma quest no QuestManager");
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
                Debug.Log("Todas as quests foram concluídas");
                questDisplay.UpdateQuestDisplay(null);
            }
        }
    }

    private void ActivateQuest(int questIndex)
    {
        var quest = questList[questIndex];
        Debug.Log($"Nova quest ativa: {quest.questName}");

        // Atualiza o display com a nova quest
        if (questDisplay != null)
        {
            questDisplay.UpdateQuestDisplay(quest);
        }
    }
}