using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireRegister : MonoBehaviour
{
    public static List<Bonfire> bonfireActive = new List<Bonfire>();    // mudei essa lista e o metodo pra estatico ja que sempre so vai ter 1 (pra atribuir pelo save)
    public static Dictionary<string, Bonfire> bonfireDictionary;    // fiz um dicionario de bonfires pro save game poder saber as bonfires a partir do bonfireName
                                                                    // -Alu
    private void Awake()
    {
        bonfireDictionary = new Dictionary<string, Bonfire>();
        foreach(Bonfire bonfire in FindObjectsByType<Bonfire>(FindObjectsSortMode.InstanceID))
        {
            bonfireDictionary.Add(bonfire.bonfireName, bonfire);
        }
    }

    public static void BonfireOn(Bonfire bonfire)
    {
        if(!bonfireActive.Contains(bonfire))
        {
            bonfireActive.Add(bonfire);
        }
    }
}
