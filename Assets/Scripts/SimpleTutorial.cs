using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTutorial : MonoBehaviour
{
    public GameObject panelTutorial;
    public GameObject fzim;

    void Awake()
    {
        panelTutorial.SetActive(false);
        fzim.SetActive(false);
    }
    public void OpenTutorial()
    {      
        panelTutorial.SetActive(true);
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }
}