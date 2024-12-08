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
    public Queue<string> nomes;

    public Dialogue _dialogue;
    public QuestParent questparent;
    public QuestManager questManager;

    void Start()
    {
        sentences = new Queue<string>();
        nomes = new Queue<string>();
        FillSentences();
    }

    public void FillSentences(){
        foreach (string sentence in _dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string nome in _dialogue.nomes)
        {
            nomes.Enqueue(nome);
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

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        string nome = nomes.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, nome));
    }

    IEnumerator TypeSentence (string sentence, string nome)
    {
        dialogueText.text = "";
        nameText.text = nome;
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

        if (!questparent.questObjective){return;}  //cheque se o bixo tem quest pq ta dando null reference
        
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
