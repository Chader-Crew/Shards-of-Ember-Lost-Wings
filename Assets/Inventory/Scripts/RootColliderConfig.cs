using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootColliderConfig : MonoBehaviour
{
    public InventoryManager inventoryManager;
    [SerializeField] private InputReader input;
    private GroundItem currentItem;

    private void Awake(){
        input.OnItemInteractEvent += HandleItemInteract;
    }

    private void OnDestroy(){
        input.OnItemInteractEvent -= HandleItemInteract;
    }

    void OnTriggerStay(Collider other){
        var item = other.GetComponent<GroundItem>();
        if(item){
            currentItem = item;
            item.buttonCanva.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        var item = other.GetComponent<GroundItem>();
        if(item && item == currentItem){
            item.buttonCanva.SetActive(false);
            currentItem = null;
        }
    }

    private void HandleItemInteract(){
        if(currentItem != null){
            // animacao
            bool result = inventoryManager.AddItem(currentItem.item);
            if(result){
                Destroy(currentItem.gameObject);
                currentItem.buttonCanva.SetActive(false);
            }
        }
    }
}


//animação bau abrindo
//ativa mensagem de item coletado
//destroy bau