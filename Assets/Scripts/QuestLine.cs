using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestLine : MonoBehaviour
{

    public GameObject questPanel;
    public TMP_Text questText;

    void Start(){
        questPanel.SetActive(true);
        UpdateQuest("Converse com Solas");
    }

    public void UpdateQuest(string newText){
        questText.text = newText;
    }
    private void HandleVilaCheck(){
        UpdateQuest("");
    }

    IEnumerator CleanQuests(){
        yield return new WaitForSeconds(5f);
        Destroy(this, 2f); //por enqunato apagar o codigo funciona
    }
}
