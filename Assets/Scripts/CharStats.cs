using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//componente de personagens, que guarda os valores numericos de hp, ataque, etc.
public class CharStats : MonoBehaviour
{
    [HideInInspector] public float hp;
    public int atk, def, spd, maxHp;
}
