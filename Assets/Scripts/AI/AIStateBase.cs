using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateBase : ScriptableObject
{
    //tipos de estado que serao transicionados para pelo state update
    public enum AIStateType
    {
        IDLE,
        PATROL,
        CHASING,
        ATTACKING,
        STAGGERED,
    }
    public abstract AIStateType StateType{get;}
    public virtual void OnStateEnter(AIStateMachine stateMachine)
    {
        
    }

    public virtual void StateUpdate(AIStateMachine stateMachine)
    {
        
    }

    public virtual void OnStateExit(AIStateMachine stateMachine)
    {

    }
}
