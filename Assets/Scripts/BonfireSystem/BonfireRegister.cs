using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireRegister : MonoBehaviour
{
    public List<Bonfire> bonfireActive = new List<Bonfire>();
    public Bonfire bonfire;

    public void BonfireOn()
    {
        if(bonfire != null)
        {
            bonfireActive.Add(bonfire);
        }
    }
}
