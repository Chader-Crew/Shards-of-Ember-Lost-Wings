using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Fleeing")]
public class AIFleeingState : AIStateBase
{
    public override AIStateType StateType => AIStateType.FLEEING;

    [SerializeField] private float fleeTime;
    [SerializeField] private float fleeDistance;
    [SerializeField] private AIStateBase stateToTransition;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        stateMachine.controller.PlayAnimation(StateType.ToString());
    }

    public override void StateUpdate(AIStateMachine stateMachine)
    {
        base.StateUpdate(stateMachine);

        if(stateMachine.controller.DistanceToTarget() > fleeDistance)
        {
            stateMachine.EnterState(stateToTransition);
            return;
        }

        if(stateMachine.timeInState > fleeTime)
        {
            stateMachine.EnterState(stateToTransition);
            return;
        }

        stateMachine.controller.FleeFrom(stateMachine.controller.aggroTarget.transform.position);
    }

    public override void OnStateExit(AIStateMachine stateMachine)
    {
        base.OnStateExit(stateMachine);

        stateMachine.controller.aggroTarget = null;
    }
}
