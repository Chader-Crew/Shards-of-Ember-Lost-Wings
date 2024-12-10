using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKillArtefato : MonoBehaviour
{
    public QuestManager questManager;
    public DialogueManager dialogueManager, dialogueManager2; //manager da nyxtra de dialogo
    //[SerializeField] private QuestData questData;

    public void OnDestroy()
    {
        //if (questData != questManager.GetCurrentQuest()) return;
        QuestManager.Instance._altarIsActive = true;
        questManager.CompleteCurrentQuest();

        dialogueManager.enabled = false; 
        dialogueManager2.enabled = true; 

    }
}
