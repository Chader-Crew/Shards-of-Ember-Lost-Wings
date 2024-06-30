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
    
    public bool _stopMoving;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);

        stateMachine.controller.animator.Play(animationStateName);
        adjustRotTimer = adjustRotDuration;

        if(_stopMoving){ stateMachine.controller.UnSetDestination(); }
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
        if(!stateMachine.controller.animator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName)){ stateMachine.EnterStateType(AIStateType.CHASING); }
    }
}
