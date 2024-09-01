using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//classe que guarda logica de ataque e outros efeitos de combate.
public abstract class SkillBase : ScriptableObject
{
    public bool canCast;
    public float cooldown;
    #region Activation Methods
    public abstract void Activate(SkillData context);
    #endregion
    public abstract void Comprado(CharStats player);


}
