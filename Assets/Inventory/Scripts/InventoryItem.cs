using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class InventoryItem : MonoBehaviour
{
    [Header("UI")]
    public Image image;
    public TMP_Text countText;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;

    //Inicialzia o item
    public void InitializeItem(Item newItem){
        item = newItem;
        image.sprite = newItem.image;
        UpdateCount();
    }

    public void UpdateCount(){
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    //drag and drop dropado pq nao teremos mais inventario principal
    //apenas comentado pq podemos ter mais pra frente
    //, IBeginDragHandler, IDragHandler, IEndDragHandler
    
    /*//Drag and drop
    public void OnBeginDrag(PointerEventData eventData){
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        countText.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        countText.raycastTarget = true;
    }*/

    public void UseItem()
    {
        Debug.Log("Count: " + count);

        if (item is UsableItem usableItem){
            if (item.itemAudio != null){
                AudioManager.instance.PlaySFX(item.itemAudio);
            }

            if (item.itemVFX != null){
                GameObject vfxInstance = Instantiate(item.itemVFX, PlayerController.Instance.transform.position, Quaternion.identity);
                vfxInstance.transform.SetParent(PlayerController.Instance.transform);
                Destroy(vfxInstance, 3f);
            }

            usableItem.Use();
            count--;

            if (count <= 0){
                Destroy(gameObject);
            }

            UpdateCount();
        }else{
            Debug.Log("Este item não pode ser usado.");
        }
    }
}
