using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireRegister : MonoBehaviour
{
    public static List<Bonfire> bonfireActive = new List<Bonfire>();  // mudei essa lista e o metodo pra estatico ja que sempre so vai ter 1 (pra atribuir pelo save)  
    public static List<BonfirePos> bonfirePos = new List<BonfirePos>();  // lista pra registrar as posições fixas da bonfire no mundo
    public static Dictionary<string, Bonfire> bonfireDictionary;   // fiz um dicionario de bonfires pro save game poder saber as bonfires a partir do bonfireName
                                                                  // -Alu     
    private void Awake()
    {
        bonfireDictionary = new Dictionary<string, Bonfire>();

        // Procurar todas as bonfires na cena
        foreach (Bonfire bonfire in FindObjectsByType<Bonfire>(FindObjectsSortMode.InstanceID))
        {
            bonfireDictionary.Add(bonfire.bonfireName, bonfire);
            bonfirePos.Add(new BonfirePos(bonfire.bonfireName, bonfire.bonfirePosition)); // Salvar posição como Vector3
        }
    }

    public static void BonfireOn(Bonfire bonfire)
    {
        if (!bonfireActive.Contains(bonfire))
        {
            bonfireActive.Add(bonfire);
        }

        // Verifica se já não adicionou a posição da bonfire na lista
        if (!bonfirePos.Exists(bp => bp.bonfireName == bonfire.bonfireName)) 
        {
            bonfirePos.Add(new BonfirePos(bonfire.bonfireName, bonfire.bonfirePosition));
        }
    }
}
