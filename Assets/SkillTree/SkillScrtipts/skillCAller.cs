using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillCAller : MonoBehaviour
{
    public GameObject bola;
    public Transform spawn;
    public void Fireball()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || skilltreeManager.skill2==true)
        {

            Instantiate(bola,spawn);
        }
    }
    
}
