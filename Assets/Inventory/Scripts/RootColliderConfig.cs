using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootColliderConfig : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GroundItem currentItem;
    public NPCInteract currentNPC;
    public RespecOpen respecItem;
    private ItemFeedback itemFeedback;
    private bool _bonfire = false, _respec = false;
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
        var respec = other.GetComponent<RespecOpen>();


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
        if(respec){
            _respec = true;
            respecItem = respec;
            respec.interactButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        var item = other.GetComponent<GroundItem>();
        var npcItem = other.GetComponent<NPCInteract>();
        var bonfire_obj = other.GetComponent<Bonfire>();
        var respec = other.GetComponent<RespecOpen>();

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
        if(respec){
            respec.ToggleSkillTree(false);
            _respec = false;
            respec.interactButton.SetActive(false);
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
            currentNPC.dialogueManager.StartDialogue();
            //ativa painel de dialogo
            //starta o dialogo
        }
        if(_bonfire){
            bonfire.BonfireActive();
        }
        if(_respec){
            respecItem.ToggleSkillTree(true);
        }
    }
}