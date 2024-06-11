using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Idle")]
public class AIIdleState : AIStateBase
{
    public override AIStateType StateType => AIStateType.IDLE;

    [SerializeField] private AIStateType stateToTransition;
    [SerializeField] private float idleDuration;
    private float timer;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        timer = 0;
        
        stateMachine.controller.animator.Play(StateType.ToString());
        stateMachine.controller.SetDestination(stateMachine.transform.position);
    }
    public override void StateUpdate(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        timer += Time.fixedDeltaTime;

        if(timer >= idleDuration)
        {
            stateMachine.EnterState(stateToTransition);
        }
    }
}
