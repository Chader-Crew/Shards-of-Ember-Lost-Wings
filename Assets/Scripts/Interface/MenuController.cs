using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject panelSettings;
    public GameObject panelCredits;

    public void OpenSettings()
    {
        if(panelSettings.activeSelf)
        {
            panelSettings.SetActive(false);
        }
        else
        {
            panelSettings.SetActive(true);
        }
    }

    public void OpenCredits()
    {
          if(panelCredits.activeSelf)
        {
            panelCredits.SetActive(false);
        }
        else
        {
            panelCredits.SetActive(true);
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitou do Game");
        Application.Quit();
    }
}
