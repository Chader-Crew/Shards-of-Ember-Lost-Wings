using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootColliderConfig : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GroundItem currentItem;
    public NPCInteract currentNPC;
    public RespecOpen respecItem;
    public LetterPage letterItem;
    private ItemFeedback itemFeedback;
    private bool _bonfire = false, _respec = false;
    private bool _letter = false;
    public Bonfire bonfire;

    private void Awake(){
        InputReader.OnItemInteractEvent += HandleItemInteract;
    }

    private void OnDestroy(){
        InputReader.OnItemInteractEvent -= HandleItemInteract;
    }

    void OnTriggerEnter(Collider other){ //tem formas tao melhores e menos pesadas de fazer essas interações, mas eu nao to mais a fim #cansei
                                        // por exemplo criar uma classe para de um tipo de interagivel e dentro dela definir qual é a interação
                                        // de cada item. quem sabe um dia.
                                        // - Ana
        var item = other.GetComponent<GroundItem>();
        var npcItem = other.GetComponent<NPCInteract>();
        var bonfire_obj = other.GetComponent<Bonfire>();
        var respec = other.GetComponent<RespecOpen>();
        var letter = other.GetComponent<LetterPage>(); 


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
        if(letter){
            _letter = true;
            letterItem = letter;
            letter.interactButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){ 
        var item = other.GetComponent<GroundItem>();
        var npcItem = other.GetComponent<NPCInteract>();
        var bonfire_obj = other.GetComponent<Bonfire>();
        var respec = other.GetComponent<RespecOpen>();
        var letter = other.GetComponent<LetterPage>(); 

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
        if(letter){
            _letter = false;
            letter.interactButton.SetActive(false);
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

            if(currentNPC.objReference != null){
                currentNPC.objReference.SetActive(true);
            }
        }
        if(_bonfire){
            bonfire.BonfireActive();
        }
        if(_respec){
            respecItem.ToggleSkillTree(true);
        }
        if(_letter){
            letterItem.ToggleLetterPage(true);
        }
    }
}