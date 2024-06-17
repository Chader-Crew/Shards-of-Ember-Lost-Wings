using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Chasing")]
public class AIChasingState : AIStateBase
{
    public override AIStateType StateType => AIStateType.CHASING;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        stateMachine.ChooseAttack();

        stateMachine.controller.animator.Play(StateType.ToString());
    }

    public override void StateUpdate(AIStateMachine stateMachine)
    {
        base.StateUpdate(stateMachine);

        if(stateMachine.cycleAttackTimer <= 0)
        {
            stateMachine.ChooseAttack();
        }

        if(stateMachine.controller.DistanceToTarget() > stateMachine.NextAttack.attackRange)
        {
            stateMachine.controller.SetDestination(stateMachine.controller.aggroTarget.transform.position);
            return;
        }

        if(stateMachine.controller.DistanceToTarget() < stateMachine.NextAttack.avoidRange)
        {
            stateMachine.controller.FleeFrom(stateMachine.controller.aggroTarget.transform.position);
            return;
        }

        stateMachine.EnterState(stateMachine.NextAttack);
    }
}
