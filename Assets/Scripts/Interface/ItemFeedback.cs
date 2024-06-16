using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemFeedback : MonoBehaviour
{
    [SerializeField] private TMP_Text itemTextObj;
    [SerializeField] private Image imageObj;
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float textDuration;

    public void DisplayItem(Item item)
    {
        Debug.Log("item.name");
        canvasObj.SetActive(true);
        itemTextObj.text = item.itemName + " coletado";
        imageObj.sprite = item.image;
        //TMP_Text newText = GameObject.Instantiate(itemTextPrefab, transform.position, Quaternion.identity, transform);
        //newText.text = damage.ToString("F0");

        //newText.transform.localPosition = offset;
        //Destroy(newText, textDuration);
    }
}
