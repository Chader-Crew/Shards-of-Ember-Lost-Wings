using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
   public string bonfireName;
   public Transform bonfireLocation;
   public GameObject interact;
   public BonfireRegister bonfireRegister;
   public InterfaceBonfire interfaceBonfire;

   public void BonfireActive()
   {
        bonfireRegister.BonfireOn(this); 
        interfaceBonfire.OpenBonfireMenu();
   }
}
