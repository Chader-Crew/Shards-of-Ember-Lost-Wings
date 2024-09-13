using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//componente de personagens, que guarda os valores numericos de hp, ataque, etc.
public class CharStats : MonoBehaviour
{
    public float hp;
    public int atk, def, spd, maxHp;

    #region Temporary Stats Methods
    public void TemporaryAtk(int variation, float time)
    {
        atk += variation;
        this.CallWithDelay(() => atk -= variation, time);
    }

    public void TemporaryDef(int variation, float time)
    {
        def += variation;
        this.CallWithDelay(() => def -= variation, time);
    }

    public void TemporarySpd(int variation, float time)
    {
        spd += variation;
        this.CallWithDelay(() => spd -= variation, time);
    }

    public void TemporaryMaxHp(int variation, float time)
    {
        maxHp += variation;
        this.CallWithDelay(() => maxHp -= variation, time);
    }

    public void TemporaryHp(int variation, float time)
    {
        hp += variation;
        this.CallWithDelay(() => hp -= variation, time);
    }
    #endregion

}
