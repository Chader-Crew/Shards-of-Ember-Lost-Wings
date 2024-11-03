using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


//estado criado para a aranha com comportamento de "weeping angel" (nesse caso ela foge quando voce ta olhando)
[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/LookingFlee")]
public class LookingFleeState : AIFleeingState
{
    [SerializeField] private AIStateBase ignoredState;
    [SerializeField] private float angleThreshold;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        
        //para a rotacao do navAgent pra ser manipulada manualmente
        stateMachine.controller.navAgent.updateRotation = false;
    }

    public override void StateUpdate(AIStateMachine stateMachine)
    {
        //dot product da direcao que o player ta olhando com a direcao que o player ta do inimigo
        //pra saber se o player esta virado para o inimigo
        Vector3 targetToAI = stateMachine.transform.position - stateMachine.controller.aggroTarget.transform.position;
        if(Vector3.Dot(targetToAI.normalized, PlayerController.Instance.transform.forward.normalized) < angleThreshold)
        {
            stateMachine.EnterState(ignoredState);
            return;
        }

        //ajusta a rotacao pra andar de costas
        stateMachine.transform.rotation = Quaternion.Euler(0,Quaternion.LookRotation(-targetToAI, Vector3.up).eulerAngles.y,0);

        base.StateUpdate(stateMachine);
    }

    public override void OnStateExit(AIStateMachine stateMachine)
    {
        //ajusta a rotacao de volta pra nao dar snap estranho
        Vector3 playerToAI = stateMachine.transform.position - stateMachine.controller.aggroTarget.transform.position;
        stateMachine.transform.rotation = Quaternion.LookRotation(-playerToAI, Vector3.up);
        
        //restaura a rotacao do navAgent
        stateMachine.controller.navAgent.updateRotation = true;

        //acontece depois da rotacao por que ela precisa do aggroTarget e isso limpa ele
        base.OnStateExit(stateMachine);
    }
}
