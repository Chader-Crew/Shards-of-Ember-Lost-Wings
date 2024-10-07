using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public Queue<string> sentences;
    public Dialogue _dialogue;

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

    public void StartDialogue()
    {

        animator.SetBool("IsOpen", true);

        nameText.text = _dialogue.name;

        //sentences.Clear();


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
        animator.SetBool("IsOpen", false);
        FillSentences();
    }
}
