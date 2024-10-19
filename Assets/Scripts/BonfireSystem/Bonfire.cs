using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public string bonfireName;
    public Vector3 bonfirePosition;  // Usar Vector3 pra a posição fixa
    public GameObject interact;
    public InterfaceBonfire interfaceBonfire;

   private void Awake()
   {
       bonfirePosition = transform.position;  // Transforma a posição da bonfire em um Vector3 fixo  
   }

    public void BonfireActive()
    {
        BonfireRegister.BonfireOn(this); 
        SaveGame.Save();
        interfaceBonfire.OpenBonfireMenu();
    }
}

