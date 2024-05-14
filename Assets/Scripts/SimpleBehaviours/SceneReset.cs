using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneReset : MonoBehaviour
{
    
   public void OnTriggerEnter(Collider other){
    if(other.CompareTag("reset")){
        SceneManager.LoadScene("MovPrototipo");
    }
   }
}
