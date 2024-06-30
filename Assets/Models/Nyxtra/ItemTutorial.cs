using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTutorial : MonoBehaviour
{
    public GameObject canvasF, canvasTutorial;

    public void Awake(){
        canvasF.SetActive(false);
        canvasTutorial.SetActive(false);
    }

    public void OpenItemTutorial(){
        canvasTutorial.SetActive(true);
    }
}
