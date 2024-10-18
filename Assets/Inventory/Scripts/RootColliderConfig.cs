using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootColliderConfig : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GroundItem currentItem;
    public NPCInteract currentNPC;
    private ItemFeedback itemFeedback;
    private bool _bonfire = false;
    public Bonfire bonfire;

    private void Awake(){
        InputReader.OnItemInteractEvent += HandleItemInteract;
    }

    private void OnDestroy(){
        InputReader.OnItemInteractEvent -= HandleItemInteract;
    }

    void OnTriggerEnter(Collider other){
        var item = other.GetComponent<GroundItem>();
        var npcItem = other.GetComponent<NPCInteract>();
        var bonfire_obj = other.GetComponent<Bonfire>();


        if(item){
            currentItem = item;
            item.buttonCanva.SetActive(true);
            //item.animator.SetTrigger("open");
            
        }
        if(npcItem){
            currentNPC = npcItem;
            npcItem.buttonCanva.SetActive(true);
        }
        if(bonfire_obj){
            _bonfire = true;
            bonfire = bonfire_obj;
            bonfire_obj.interact.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        var item = other.GetComponent<GroundItem>();
        var npcItem = other.GetComponent<NPCInteract>();
        var bonfire_obj = other.GetComponent<Bonfire>();

        if(item && item == currentItem){
            item.buttonCanva.SetActive(false);
            //item.animator.SetTrigger("close");
            currentItem = null;
        }
        if(npcItem){
            npcItem.buttonCanva.SetActive(false);
            //item.animator.SetTrigger("close");
            currentNPC = null;
        }
        if(bonfire_obj){
            _bonfire = false;
            bonfire_obj.interact.SetActive(false);
        }
    }

    private void HandleItemInteract(){
        if(currentItem != null){
            // animacao
            bool result = inventoryManager.AddItem(currentItem.item);
            if(result){
                //itemFeedback.DisplayItem(currentItem.item);
                Destroy(currentItem.gameObject);
                currentItem.buttonCanva.SetActive(false);
                currentItem.chestAudio.Play();
            }
        }
        if(currentNPC != null){
            Debug.Log("talk");
        }
        if(_bonfire){
            bonfire.BonfireActive();
        }
    }
}