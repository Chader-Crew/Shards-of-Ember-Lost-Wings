using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canBurn : MonoBehaviour
{
     static float counter;
    
    public GameObject esse;

    public float vida;
    void Start()
    {
        vida=esse.GetComponent<CharStats>().hp;
    }
    // Start is called before the first frame update
    public void Burn(float duracao)
    {
        counter=duracao;
        InvokeRepeating("TakeBurn",1,duracao);
    }
    private  void TakeBurn()
    {
        
        vida--;
        Debug.Log("burn baby burn");
        esse.GetComponent<CharStats>().hp -= (esse.GetComponent<CharStats>().hp-vida);
        if(counter<=0)
        {
            CancelInvoke("TakeBurn");
        }
    }
}
