using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    #region Vars
    public NPCController controller;

    //estado inicial
    [SerializeField] private AIStateBase startingState;
    private AIStateBase currentState;
    public AIStateBase CurrentState { get => currentState; }

    //estados que esse agente vai usar
    [SerializeField] List<AIStateBase> validStates;
    private List<AIAttackingState> attackStates;
    private AIAttackingState nextAttack;
    public AIAttackingState NextAttack { get => nextAttack; }
    [HideInInspector] public float cycleAttackTimer;
    [HideInInspector] public float timeInState;
    Dictionary<AIStateBase.AIStateType, AIStateBase> stateDictionary;

    
    //waypoints usados em alguns estados de navegacao
    [Space(5)]
    public Transform[] waypoints;
    public int waypointIndex;

    #endregion

    private void Awake() 
    {
        controller = GetComponent<NPCController>();
        currentState = null;
        attackStates = new List<AIAttackingState>();

        stateDictionary = new Dictionary<AIStateBase.AIStateType, AIStateBase>();
        foreach(AIStateBase state in validStates)
        {
            if(state is AIAttackingState)
            {
                attackStates.Add(state as AIAttackingState);
            }else{
                stateDictionary.Add(state.StateType, state);
            }
        }
    }

    private void Start() 
    {
        if(startingState != null)
        {
            EnterState(startingState);
        }
    }

    private void FixedUpdate() 
    {
        currentState.StateUpdate(this);
        cycleAttackTimer -= Time.fixedDeltaTime;
        timeInState += Time.fixedDeltaTime;
    }

    public void EnterState(AIStateBase state)
    {
        if(currentState != null) 
        {
            currentState.OnStateExit(this); 
        }
        currentState = state;
        currentState.OnStateEnter(this);
        timeInState = 0;
    }

    public void EnterStateType(AIStateBase.AIStateType type)
    {
        if(stateDictionary.ContainsKey(type))
        {
            EnterState(stateDictionary[type]);
        }
    }

    public void ChooseAttack()
    {
        List<AIAttackingState> inRangeAttacks = new List<AIAttackingState>();
        float targetDistance = controller.DistanceToTarget();

        foreach(AIAttackingState attack in attackStates)
        {
            if(targetDistance < attack.attackRange && targetDistance > attack.avoidRange)
            {
                inRangeAttacks.Add(attack);
            }
        }

        if(inRangeAttacks.Count > 0)
        {
            nextAttack = inRangeAttacks[UnityEngine.Random.Range(0, inRangeAttacks.Count)];
        }
        else
        {
            nextAttack = attackStates[UnityEngine.Random.Range(0, attackStates.Count)];
        }

        cycleAttackTimer = nextAttack.cycleTimer;
    }
}
