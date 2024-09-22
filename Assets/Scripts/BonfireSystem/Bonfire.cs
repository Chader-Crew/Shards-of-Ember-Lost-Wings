using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
   public string bonfireName;
   public Transform bonfireLocation;

//    private void OnCollisionEnter(Collider other)
//    {
//         if(other.CompareTag("Player"))
//         {

//         }
//    }

   private void BonfireActive()
   {
        BonfireRegister bonfireRegister = FindObjectOfType<BonfireRegister>();
        bonfireRegister.BonfireOn(this); 
   }
}
