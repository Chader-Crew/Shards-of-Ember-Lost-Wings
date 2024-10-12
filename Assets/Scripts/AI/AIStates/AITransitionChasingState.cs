using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AITransitionChasingState : AIChasingState
{
	//animacao de loop que rota ate acontecer o ataque
	[SerializeField] private string transitionAnimationStateName;
	[SerializeField] private float transitionDelay;
	
	[SerializeField] private AIAttackingState exclusiveAttackingState;

	public override AIStateType StateType => AIStateType.TRANSITION;

	public override void OnStateEnter(AIStateMachine stateMachine)
	{
		base.OnStateEnter(stateMachine);
		
		//chama a animacao que loopa depois de um tempo
		stateMachine.CallWithDelay(() => 
				stateMachine.controller.PlayAnimation(transitionAnimationStateName),
				transitionDelay);
	}

	public override void StateUpdate(AIStateMachine stateMachine)
	{
		base.StateUpdate(stateMachine);
		if(stateMachine.controller.DistanceToTarget() > exitChaseRange)
		{
			stateMachine.controller.aggroTarget = null;
			stateMachine.EnterStateType(stateToTransition);
			return;
		}

		if(stateMachine.controller.DistanceToTarget() > exclusiveAttackingState.attackRange)
		{
			stateMachine.controller.SetDestination(stateMachine.controller.aggroTarget.transform.position);
			return;
		}

		if(stateMachine.controller.DistanceToTarget() < exclusiveAttackingState.avoidRange)
		{
			stateMachine.controller.FleeFrom(stateMachine.controller.aggroTarget.transform.position);
			return;
		}

		// Se a distancia esta correta mas ainda nao deu o tempo minimo, fica mexendo aleatoriamente no destino para o bixo nao ficar parado.
		if(stateMachine.timeInState < minimumTimeInState)
		{   
			stateMachine.controller.NudgeDestination(wanderDegrees);

		}else
		{
			stateMachine.EnterState(exclusiveAttackingState);
		}
	}
	
	
}
