using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Attacking")]
public class AIAttackingState : AIStateBase
{
    public override AIStateType StateType => AIStateType.ATTACKING;
    public string animationStateName;
    public float attackRange;
    public float avoidRange;
    public float cycleTimer;
    [Range(0,45)]public float adjustRotSpeed;
    public float adjustRotDuration;
    private float adjustRotTimer;
    
    public bool _stopMoving = true;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        adjustRotTimer = adjustRotDuration;

        if(_stopMoving){ stateMachine.controller.UnSetDestination(); }

        stateMachine.controller.PlayAnimation(animationStateName);
    }

    public override void StateUpdate(AIStateMachine stateMachine)
    {
        base.StateUpdate(stateMachine);
        
        if(adjustRotTimer > 0)
        {
            stateMachine.transform.rotation = Quaternion.RotateTowards(
                    stateMachine.transform.rotation,
                    Quaternion.LookRotation(
                            stateMachine.controller.aggroTarget.transform.position -
                            stateMachine.transform.position, Vector3.up), adjustRotSpeed);
        
            adjustRotTimer -= Time.deltaTime;
        }

        //deus me livra desse bug por favor eu não aguento mais toma esse spaghetti e me deixa em paz
        //edit: essa vai ser a solucao final. Todo estado que depende de uma animacao terminar pra mudar vai checar todo frame pelo exit time automatico do animator.
        //unity animator voce me enganou me iludiu me traiu eu te odeio unity animator espero que vc seja deprecado.
        if(!stateMachine.controller.animator.IsInTransition(0) && !stateMachine.controller.animator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName))
        { 
            stateMachine.EnterStateType(AIStateType.CHASING); 
        }
    }

    public override void OnStateExit(AIStateMachine stateMachine)
    {
        base.OnStateExit(stateMachine);

        //stateMachine.controller.ResetAttack();
    }
}
