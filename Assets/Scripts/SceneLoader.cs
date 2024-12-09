using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1; //toda cena que loadar o time scale volta pra 1
        
        //dia de entrega final ne fazer o q
        if (sceneName == "Game")
        {
            SceneManager.sceneLoaded += SaveGame.LoadBlank;
        }
    }

    public static void LoadSave()
    {
        SceneManager.sceneLoaded += SaveGame.Load;

        SceneManager.LoadScene("Game");
        Time.timeScale = 1; //toda cena que loadar o time scale volta pra 1
    }
}
