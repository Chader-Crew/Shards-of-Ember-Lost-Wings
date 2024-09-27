using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
   public string bonfireName;
   public Transform bonfireLocation;
   public GameObject interact;
   public InterfaceBonfire interfaceBonfire;

   public void BonfireActive()
   {
        BonfireRegister.BonfireOn(this); 
        SaveGame.Save();
        interfaceBonfire.OpenBonfireMenu();
   }
}
