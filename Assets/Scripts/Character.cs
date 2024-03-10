using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe para personagens envolvidos em combate.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CharStats))]
public class Character : MonoBehaviour
{
    //estado atual (coisas como atacando, levando dano, desviando, bloqueando)
    private CharStateBase _currentState;
    public CharStateBase CurrentState { 
        get => _currentState;
        
        set{
            _currentState.BeforeChangeState();
            _currentState = value;
            _currentState.OnEnterState();
        }
    }


    
}
