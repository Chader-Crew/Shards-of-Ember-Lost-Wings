using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canBurn : MonoBehaviour
{
     static float counter;
    
    public GameObject esse;

    // Start is called before the first frame update
    public void Burn(float duracao)
    {
        counter=duracao;
        InvokeRepeating("TakeBurn",1,duracao);
    }
    private  void TakeBurn()
    {
        SkillData skillData= new SkillData();
        skillData.damage = 1;

        Debug.Log("burn baby burn");
        esse.GetComponent<Character>().GetHit(skillData);
        if(counter<=0)
        {
            CancelInvoke("TakeBurn");
        }
    }
}
