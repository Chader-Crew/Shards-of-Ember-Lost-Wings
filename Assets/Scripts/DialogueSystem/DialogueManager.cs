using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    //public Animator animator; //para implementar uma animacao para a caixa de dialogo depois
    public Queue<string> sentences;

    public Dialogue _dialogue;
    public QuestParent questparent;
    public QuestManager questManager;

    void Start()
    {
        sentences = new Queue<string>();
        FillSentences();
    }

    public void FillSentences(){
        foreach (string sentence in _dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    //Dependendo pode ter que usar um sentences.Clear(), mas por enquanto funciona
    public void StartDialogue()
    {

        //animator.SetBool("IsOpen", true);
        dialoguePanel.SetActive(true);
        nameText.text = _dialogue.name;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue(){
        Debug.Log("Fim do diálogo");
        dialoguePanel.SetActive(false);
        FillSentences();
        CheckQuest();
    }

    public void CheckQuest(){
        //pegar qual é o questobjective do questparent e ver se ta requisito
        //se ta requisito, se nao ta completa
        //se eh a quest ativa

        if(questparent.questObjective._request && 
        !questparent.questObjective._isCompleted && 
        questparent.questObjective.questName == questManager.questList[0].questName){
            Debug.Log("completinha " + questparent.questObjective.questName);
            questManager.CompleteCurrentQuest();
        }else{
            Debug.Log("ainda nao completa " + questparent.questObjective.questName);
        }
    }
}
