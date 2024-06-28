using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootColliderConfig : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [SerializeField] private InputReader input;
    public GroundItem currentItem;
    private ItemFeedback itemFeedback;

    private void Awake(){
        input.OnItemInteractEvent += HandleItemInteract;
    }

    private void OnDestroy(){
        input.OnItemInteractEvent -= HandleItemInteract;
    }

    void OnTriggerEnter(Collider other){
        var item = other.GetComponent<GroundItem>();
        if(item){
            currentItem = item;
            item.buttonCanva.SetActive(true);
            //item.animator.SetTrigger("open");
            item.chestAudio.Play();
        }
    }

    void OnTriggerExit(Collider other){
        var item = other.GetComponent<GroundItem>();
        if(item && item == currentItem){
            item.buttonCanva.SetActive(false);
            //item.animator.SetTrigger("close");
            currentItem = null;
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
            }
        }
    }
}


//animação bau abrindo
//ativa mensagem de item coletado
//destroy bau