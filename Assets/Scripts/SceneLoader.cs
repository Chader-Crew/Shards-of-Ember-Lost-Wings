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
    }

    public static void LoadSave()
    {
        SceneManager.sceneLoaded += SaveGame.Load;

        SceneManager.LoadScene("Game");
        Time.timeScale = 1; //toda cena que loadar o time scale volta pra 1
    }
}
