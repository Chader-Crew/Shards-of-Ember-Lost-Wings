using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//estado criado para a aranha com comportamento de "weeping angel" (ataca enquanto voce nao esta olhando)
[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/NoLookingChase")]
public class NotLookingChaseState : AIChasingState
{
    [SerializeField] private AIStateBase watchedState;
    [SerializeField] private float angleTreshold;
    public override void StateUpdate(AIStateMachine stateMachine)
    {
        //dot product da direcao que o player ta olhando com a direcao que o player ta do inimigo
        //pra saber se o player esta virado para o inimigo (tambem nao acontece se nao passar o tempo minimo)
        Vector3 playerToAI = stateMachine.transform.position - PlayerController.Instance.transform.position;
        if(Vector3.Dot(playerToAI.normalized, PlayerController.Instance.transform.forward.normalized) > angleTreshold 
                && stateMachine.timeInState > minimumTimeInState)
        {
            stateMachine.EnterState(watchedState);
            return;
        }

        base.StateUpdate(stateMachine);
    }
}
