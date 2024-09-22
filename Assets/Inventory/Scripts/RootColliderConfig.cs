using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootColliderConfig : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GroundItem currentItem;
    private ItemFeedback itemFeedback;
    private bool _solas = false, _nyxtra = false, _bonfire = false;
    public SimpleTutorial simpleTutorial;
    public ItemTutorial itemTutorial;
    public Bonfire bonfire;

    private void Awake(){
        InputReader.OnItemInteractEvent += HandleItemInteract;
    }

    private void OnDestroy(){
        InputReader.OnItemInteractEvent -= HandleItemInteract;
    }

    void OnTriggerEnter(Collider other){
        var item = other.GetComponent<GroundItem>();
        var solas = other.GetComponent<SimpleTutorial>();
        var nyxtra = other.GetComponent<ItemTutorial>();
        var bonfire_obj = other.GetComponent<Bonfire>();


        if(item){
            currentItem = item;
            item.buttonCanva.SetActive(true);
            //item.animator.SetTrigger("open");
            
        }
        if(solas){
            _solas = true;
            simpleTutorial = solas;
            solas.fzim.SetActive(true);
        }
        if(nyxtra){
            _nyxtra = true;
            itemTutorial = nyxtra;
            nyxtra.canvasF.SetActive(true);
        }
        if(bonfire_obj){
            _bonfire = true;
            bonfire = bonfire_obj;
            bonfire_obj.interact.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        var item = other.GetComponent<GroundItem>();
        var solas = other.GetComponent<SimpleTutorial>();
        var nyxtra = other.GetComponent<ItemTutorial>();
        var bonfire_obj = other.GetComponent<Bonfire>();

        if(item && item == currentItem){
            item.buttonCanva.SetActive(false);
            //item.animator.SetTrigger("close");
            currentItem = null;
        }
        if(solas){
            _solas = false;
            simpleTutorial = null;
            solas.fzim.SetActive(false);
        }
        if(nyxtra){
            _nyxtra = false;
            itemTutorial = null;
            nyxtra.canvasF.SetActive(false);
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
        if(_solas){
            simpleTutorial.OpenTutorial();
        }
        if(_nyxtra){
            itemTutorial.OpenItemTutorial();
        }
        if(_bonfire){
            bonfire.BonfireActive();
        }
    }
}


//animação bau abrindo
//ativa mensagem de item coletado
//destroy bau