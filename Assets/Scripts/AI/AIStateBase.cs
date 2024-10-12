using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateBase : ScriptableObject
{   
    //modificacao de movespeed movida para StateBase pq eu achei necessario ter isso em estados demais
    [SerializeField] private float speedMod = 1;

    //tipos de estado que serao transicionados para pelo state update
    public enum AIStateType
    {
        IDLE,
        PATROL,
        CHASING,
        ATTACKING,
        STAGGERED,
        FLEEING,
        TRANSITION,
    }
    public abstract AIStateType StateType{get;}
    public virtual void OnStateEnter(AIStateMachine stateMachine)
    {
        stateMachine.controller.navAgent.speed *= speedMod;
    }

    public virtual void StateUpdate(AIStateMachine stateMachine)
    {
        
    }

    public virtual void OnStateExit(AIStateMachine stateMachine)
    {
        stateMachine.controller.navAgent.speed /= speedMod;
    }
}
