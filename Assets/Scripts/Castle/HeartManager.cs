using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;
    public int heartsToDestroy = 3; 
    public GameObject door; 
    public int destroyedHeartsCount = 0;

    public bool destroyableCrystal = false;

    void Start(){
        instance = this;
    }
    public void HeartDestroyed()
    {
        destroyedHeartsCount++;
        
        if (destroyedHeartsCount >= heartsToDestroy)
        {
            OpenDoor();
        }
    }

    
    private void OpenDoor()
    {
        door.SetActive(false); 
    }
}