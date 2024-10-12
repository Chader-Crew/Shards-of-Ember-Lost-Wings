using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public List<GameObject> hearts; 
    public int heartsToDestroy = 3; 
    public GameObject door; 
    private int destroyedHeartsCount = 0;

    void Start()
    {
        
        door.SetActive(true);
    }

    
    public void HeartDestroyed()
    {
        destroyedHeartsCount++;

        Debug.Log("Corações destruídos: " + destroyedHeartsCount);

        
        if (destroyedHeartsCount >= heartsToDestroy)
        {
            OpenDoor();
        }
    }

    
    private void OpenDoor()
    {
        Debug.Log("Número necessário de corações destruído. Porta aberta!");
        door.SetActive(false); 
    }
}