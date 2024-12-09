using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKillArtefato : MonoBehaviour
{
    public QuestManager questManager;
    public DialogueManager dialogueManager; //manager da nyxtra de dialogo
    [SerializeField] private QuestData questData;

    public void OnDestroy()
    {
        if (questData != questManager.GetCurrentQuest()) return;

        questManager.CompleteCurrentQuest();

    }
}
