
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public toAtivo dialoguePanel;

    public Queue<string> sentences;
    public Queue<string> nomes;
    public Dialogue _dialogue;

    public bool firstDialogue = true; //setar como true quando quiser que complete a quest

    public QuestManager questManager;

    void Start()
    {
        dialoguePanel = (toAtivo)FindObjectOfType(typeof(toAtivo), true);
        nameText = dialoguePanel.transform.GetChild(0).GetComponent<TMP_Text>();
        dialogueText = dialoguePanel.transform.GetChild(1).GetComponent<TMP_Text>();

        //dialoguePanel.SetActive(false);

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
        dialoguePanel.gameObject.SetActive(true);
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

        string nome = nomes.Dequeue();
        string sentence = sentences.Dequeue();
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
        dialoguePanel.gameObject.SetActive(false);
        if(firstDialogue){
            QuestManager.Instance.CompleteCurrentQuest();
            firstDialogue = !firstDialogue;
        }
        //animator.SetBool("IsOpen", false);
        FillSentences();
    }

    public void OnEnable(){
        NPCInteract npcInteract = GetComponent<NPCInteract>();
        npcInteract.dialogueManager = this;
    }
}
