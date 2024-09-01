using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//classe que guarda logica de ataque e outros efeitos de combate.
public abstract class SkillBase : ScriptableObject
{
    public bool canCast;
    public float cooldown = 0;  //valor de cooldown pro timer
    [HideInInspector] public bool _onCooldown; 
    private void Awake() 
    {
        _onCooldown = false;    //garante que as skills ficam fora de cooldown quando loadadas
    }

    #region Activation Methods
    public abstract void Activate(SkillData context);
    #endregion
    public abstract void Comprado(CharStats player);


}
