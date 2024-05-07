using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryObject inventory;

    public int x_startPosition, y_startPosition;
    public int x_between, y_between; //Espaco de um item para o outro no canvas
    public int columnn_number; // Numero de colunas para dar uma quebra de linha

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start(){
        CreateDisplay();
    }

    void FixedUpdate(){
        UpdateDisplay();
    }

    /*Nao quero que tenha um Update ou FixedUpdate nesse codigo, ainda vou
    resolver isso chamando o "UpdateDisplay" so quando colidir com o item.

    Tambem nao gostei muito de ter um CreateDisplay se o UpdateDisplay ja
    cria se nao tiver encontrado algum igual.... Pensarei depois, pois ele
    ainda sera util na parte de save e load de inventario.
    */

    public void CreateDisplay(){
        for(int i = 0; i < inventory.Container.Count; i++){
            var obj = Instantiate(inventory.Container[i].item.slotPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

    public void UpdateDisplay(){
        for(int i = 0; i < inventory.Container.Count; i++){
            if(itemsDisplayed.ContainsKey(inventory.Container[i])){
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }else{
                var obj = Instantiate(inventory.Container[i].item.slotPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public Vector3 GetPosition(int i){
        //calculo pra construir o grid bonitinho
        return new Vector3(x_startPosition + (x_between * (i % columnn_number)), y_startPosition + (-y_between * (i/columnn_number)), 0f);
    }
}
