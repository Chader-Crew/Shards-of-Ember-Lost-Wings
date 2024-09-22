using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireRegister : MonoBehaviour
{
    public List<Bonfire> bonfireActive = new List<Bonfire>();

    public void BonfireOn(Bonfire bonfire)
    {
        if(bonfire != null)
        {
            bonfireActive.Add(bonfire);
        }
    }
}
