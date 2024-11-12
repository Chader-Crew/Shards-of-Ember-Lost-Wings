using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterPage : MonoBehaviour
{
    public TMP_Text displayText;
    public string letterText;
    public GameObject interactButton, letterImage;

    public void ToggleLetterPage(bool val){
        letterImage.SetActive(val);
        displayText.text = letterText;
    }
}
