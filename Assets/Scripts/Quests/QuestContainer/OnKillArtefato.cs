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
        dialogueManager.sentences.Enqueue("Aqui… eu consegui. Faça suas mágicas e acabe com isso logo.");
        dialogueManager.sentences.Enqueue("Paciência menino apressado, nessa vida as melhores coisas são feitas com calma");
        dialogueManager.sentences.Enqueue("Ouvi dizer que o caminho para a saída é no castelo... tenha cuidado rapaz");

        dialogueManager.nomes.Enqueue("Príncipe");
        dialogueManager.nomes.Enqueue("Nyxtra");
        dialogueManager.nomes.Enqueue("Nyxtra");

        dialogueManager.firstDialogue = true;

        dialogueManager.FillSentences();
    }
}
