using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public NPCItem npcItem;
    public GameObject buttonCanva, objReference; //referencia de algum objeto se necessario
    public AudioSource chestAudio;
    public DialogueManager dialogueManager;

    public void Initialize(NPCItem npcItem){
        this.npcItem = npcItem;
    }
}
