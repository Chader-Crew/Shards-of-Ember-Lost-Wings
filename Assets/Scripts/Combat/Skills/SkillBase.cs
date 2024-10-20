using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//classe que guarda logica de ataque e outros efeitos de combate.
public abstract class SkillBase : ScriptableObject
{
    public bool canCast;
    public float cooldown = 0;  //valor de cooldown pro timer
    public bool _onCooldown; 
    private void Awake() 
    {
        _onCooldown = false;    //garante que as skills ficam fora de cooldown quando loadadas
    }

    #region Activation Methods
    public abstract void Activate(SkillData context);
    #endregion

    public virtual void Comprado(CharStats player)
    {
        
    }


}
