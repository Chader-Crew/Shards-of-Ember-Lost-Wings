using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Quest> activeQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        Debug.Log("Quest adicionada: " + quest.QuestName);
    }

    public void CompleteQuest(Quest quest)
    {
        quest.CompleteQuest();
        activeQuests.Remove(quest);
    }

    public void ShowActiveQuests()
    {
        foreach (var quest in activeQuests)
        {
            Debug.Log("Quest ativa: " + quest.QuestName + " - " + quest.Description); 
        }
    }
}