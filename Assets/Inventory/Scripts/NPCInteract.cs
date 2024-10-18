using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public NPCItem npcItem;
    public GameObject buttonCanva;
    public AudioSource chestAudio;

    public void Initialize(NPCItem npcItem){
        this.npcItem = npcItem;
    }
}
