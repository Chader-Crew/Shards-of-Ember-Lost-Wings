using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CooldownDisplay : MonoBehaviour
{
    private Image image;
    //tempo que falta e tempo total do cooldown
    private float remainingTime, totalTime;
    //action que e chamada quando o tempo acaba
    public Action CallBack;

    private void Awake() 
    {
        image = GetComponent<Image>();
        image.type = Image.Type.Filled; //tem que ser esse image type pra mostrar o fill
        image.fillAmount = 0;
        CallBack = ()=>{};
    }

    private void Update() 
    {
        //timer
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            image.fillAmount = remainingTime/totalTime;
            
        }else return;   //se nao tem timer nao checa se acabou o tempo

        //se acabou o tempo chama a funcao de callback e esvazia as chamadas da action
        if(remainingTime <=0)
        {
            CallBack();
            CallBack = () => {};
            image.fillAmount = 0;
        }
    }

    public void StartTimer(float duration)
    {
        totalTime = duration;
        remainingTime = duration;
    }
}
