using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe base para estados de personagens. E scriptable object pra poder colocar no inspetor.
public abstract class CharStateBase : ScriptableObject
{
    public abstract void OnEnterState();
    public abstract void BeforeChangeState();
}
