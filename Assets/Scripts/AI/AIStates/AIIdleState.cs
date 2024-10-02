using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Idle")]
public class AIIdleState : AIStateBase
{
    public override AIStateType StateType => AIStateType.IDLE;

    [SerializeField] private AIStateType stateToTransition;
    [SerializeField] private float idleDuration;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        
        stateMachine.controller.PlayAnimation(StateType.ToString());
        stateMachine.controller.SetDestination(stateMachine.transform.position);
    }
    public override void StateUpdate(AIStateMachine stateMachine)
    {
        base.StateUpdate(stateMachine);

        if(stateMachine.timeInState >= idleDuration)
        {
            stateMachine.EnterStateType(stateToTransition);
        }
    }
}
