using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ScriptableObjects/AIStates/Patrol")]
public class AIPatrolState : AIStateBase
{
    public override AIStateType StateType => AIStateType.PATROL;
    private bool walkPointSet;
    [SerializeField] private AIStateType stateToTransition;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        walkPointSet = false;
        
        if(stateMachine.waypoints.Length <=1) { stateToTransition = AIStateType.IDLE; }

        stateMachine.controller.PlayAnimation(StateType.ToString());
    }

    public override void StateUpdate(AIStateMachine stateMachine)
    {
        base.StateUpdate(stateMachine);
        if (!walkPointSet) NextWalkPoint();

        Vector3 distanceToWalkPoint = stateMachine.transform.position - stateMachine.waypoints[stateMachine.waypointIndex].position;

        //Walkpoint alcançado
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            stateMachine.EnterStateType(stateToTransition);
        }
        //pega o proximo waypoint
        void NextWalkPoint()
        {
            stateMachine.waypointIndex = (stateMachine.waypointIndex +1) % stateMachine.waypoints.Length;
            stateMachine.controller.SetDestination(stateMachine.waypoints[stateMachine.waypointIndex].position);
            walkPointSet = true;
        }
    }

    public override void OnStateExit(AIStateMachine stateMachine)
    {
        base.OnStateExit(stateMachine);

        stateMachine.controller.SetDestination(stateMachine.transform.position);
    }
}
