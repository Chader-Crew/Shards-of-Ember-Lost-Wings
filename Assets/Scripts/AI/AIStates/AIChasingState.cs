using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Chasing")]
public class AIChasingState : AIStateBase
{
    public override AIStateType StateType => AIStateType.CHASING;

    [SerializeField] protected float exitChaseRange;          // Range de desistência da perseguicao
    [SerializeField] protected AIStateType stateToTransition; // Estado para transicionar quando o alvo sai da range
    [SerializeField] protected float minimumTimeInState;      // Tempo minimo no estado antes de iniciar um ataque
    [SerializeField] protected float wanderDegrees;           // Fator de variância em graus quanto ao wander enquanto dentro da range do ataque,
                                                            // antes de completar o tempo minimo do estado.

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        stateMachine.ChooseAttack();

        stateMachine.controller.PlayAnimation(StateType.ToString());

        //reseta a direcao de "vaguear" para ser em direcao ao alvo.
        stateMachine.controller.wanderSmooth = stateMachine.controller.aggroTarget.transform.position - stateMachine.transform.position;
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

        // Se a distancia esta correta mas ainda nao deu o tempo minimo, fica mexendo aleatoriamente no destino para o bixo nao ficar parado.
        if(stateMachine.timeInState < minimumTimeInState)
        {   
            stateMachine.controller.NudgeDestination(wanderDegrees);

        }else
        {
            stateMachine.EnterState(stateMachine.NextAttack);
        }
    }
}
