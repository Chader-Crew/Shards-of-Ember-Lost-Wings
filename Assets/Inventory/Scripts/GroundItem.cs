using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public Item item;
    public GameObject buttonCanva;
    public AudioSource chestAudio;
    //public Animator animator;

    public void Initialize(Item item){
        this.item = item;
    }
}
