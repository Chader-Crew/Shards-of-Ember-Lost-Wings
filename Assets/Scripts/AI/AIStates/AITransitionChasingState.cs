using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/TransitionChase")]
public class AITransitionChasingState : AIChasingState
{
	//animacao de loop que roda ate acontecer o ataque
	[SerializeField] private string transitionAnimationStateName;
	
	
	//estado de ataque seguinte, sempre sera o proximo estado
	[SerializeField] private AIAttackingState exclusiveAttackingState;

	public override AIStateType StateType => AIStateType.TRANSITION;
	
	public override void OnStateEnter(AIStateMachine stateMachine)
	{
		base.OnStateEnter(stateMachine);

		stateMachine.controller.PlayAnimation(transitionAnimationStateName);
	}

	// E uma versao modificada do update do AIChasingState
	public override void StateUpdate(AIStateMachine stateMachine)
	{
		base.StateUpdate(stateMachine);
		if(stateMachine.controller.DistanceToTarget() > exitChaseRange)
		{
			stateMachine.controller.aggroTarget = null;
			stateMachine.EnterState(exclusiveAttackingState);
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
