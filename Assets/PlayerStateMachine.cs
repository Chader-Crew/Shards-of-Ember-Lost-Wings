using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //estado inicial (atribui no awake e nao executa o BeforeChangeState)
    [SerializeField] private CharStateBase startingState;
    [SerializeField] private CharStateBase staggerState;

    //estado atual (coisas como atacando, levando dano, desviando, bloqueando)
    private CharStateBase _currentState;    //use a propriedade, nao a variavel. Referencias da implementacao atual: 5 (4 na propriedade, 1 no awake)
    public CharStateBase CurrentState 
    { 
        get => _currentState;
        
        set
        {
            _currentState.BeforeChangeState();
            _currentState = value;
            _currentState.OnEnterState();
        }
    }

    private void Awake() 
    {
        _currentState = startingState;
        CurrentState.OnEnterState();
    }

    public void Stagger(float time)
    {
        CurrentState = staggerState;
        StartCoroutine(StaggerCoroutine(time));
        IEnumerator StaggerCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            CurrentState = startingState;
        }
    }
}
