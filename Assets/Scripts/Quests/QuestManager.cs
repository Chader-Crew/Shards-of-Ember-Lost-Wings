using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> questList; // Lista de quests
    public QuestDisplay questDisplay;
    private int currentQuestIndex = 0; // Índice da quest atual

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

        // Verifica se a quest ativa é do tipo EnemyKiller
        if (quest is EnemyKiller enemyKillerQuest)
        {
            enemyKillerQuest.Initialize(); // Inicializa a quest de matar inimigos
        }

        // Atualiza o display com a nova quest
        if (questDisplay != null)
        {
            questDisplay.UpdateQuestDisplay(quest);
        }
    }

    // Método para obter a quest ativa
    public QuestData GetActiveQuest()
    {
        if (currentQuestIndex < questList.Count)
        {
            return questList[currentQuestIndex];
        }
        return null;
    }
}
