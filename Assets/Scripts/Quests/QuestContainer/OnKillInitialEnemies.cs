using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKillInitialEnemies : MonoBehaviour
{
    public QuestManager questManager;
    [SerializeField] private QuestData questData;

    public void OnDestroy()
    {
        if (questData != questManager.GetCurrentQuest()) return;

        questManager.CompleteCurrentQuest();

    }
}
