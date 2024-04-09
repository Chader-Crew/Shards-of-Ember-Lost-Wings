using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//classe que guarda logica de ataque e outros efeitos de combate.
public abstract class SkillBase : ScriptableObject
{
    #region Activation Methods
    public abstract void Activate(SkillData context);
    #endregion
}
