using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Chasing")]
public class AIChasingState : AIStateBase
{
    public override AIStateType StateType => AIStateType.CHASING;

    [SerializeField] private float exitChaseRange;
    [SerializeField] private AIStateType stateToTransition;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        stateMachine.ChooseAttack();

        stateMachine.controller.PlayAnimation(StateType.ToString());
    }

    public override void StateUpdate(AIStateMachine stateMachine)
    {
        base.StateUpdate(stateMachine);

        if(stateMachine.cycleAttackTimer <= 0)
        {
            if(stateMachine.controller.DistanceToTarget() > exitChaseRange)
            {
                stateMachine.controller.aggroTarget = null;
                stateMachine.EnterStateType(stateToTransition);
                return;
            }
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
