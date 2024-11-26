using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AIStateMachine : MonoBehaviour
{
    #region Vars
    public NPCController controller;

    //estado inicial
    [SerializeField] private AIStateBase startingState;
    private AIStateBase currentState;
    public AIStateBase CurrentState { get => currentState; }

    //estados que esse agente vai usar
    [SerializeField] private List<AIStateBase> validStates;
    //estados de ataque para serem escolhidos aleatoriamente durante chase
    private List<AIAttackingState> attackStates;    
    //estados de ataque que sao validos mas nao sao escolhidos aleatoriamente
    [SerializeField] private List<AIAttackingState> protectedAttackStates;   
    
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
        foreach (AIStateBase state in validStates)
        {
            //adiciona o estado na lista de estados para escolher ataque
            if (state is AIAttackingState attackingState)
            {
                
                //checagem se ha estado no animator com o nome correto para o ataque (so acontece no editor)
#if UNITY_EDITOR
                    if (controller.animator.HasState(0, Animator.StringToHash(attackingState.animationStateName)))
                    {
#endif
                        
                    attackStates.Add(attackingState);
                    
#if UNITY_EDITOR                
                    }else
                    {
                        Debug.LogError($"|Entidade IA {gameObject} nao contem animacao adequada para {attackingState}|");
                    }   
#endif
            }
            else    //se nao for um ataque, adiciona no dicionario com tipo-chave
            {
                stateDictionary.Add(state.StateType, state);
            }
        }
        //checagem se o animator tem a animacao correta para os ataques que nao sao aleatorios (so acontece no editor)
#if UNITY_EDITOR
        foreach (AIAttackingState state in protectedAttackStates)
        {
            if (!controller.animator.HasState(0, Animator.StringToHash(state.animationStateName)))
            {
                Debug.LogError($"|Entidade IA {gameObject} nao contem animacao adequada para {state}|");
            }
        }
#endif
    }

    private void Start() 
    {
        if(startingState != null)
        {
            EnterState(startingState);
        }

        if(waypoints.Length == 0 || waypoints == null)
        {
            Transform newObj = new GameObject().transform;
            newObj.position = transform.position;
            newObj.rotation = Quaternion.identity;
            newObj.name = "AI Waypoint";
            waypoints = new Transform[]{ newObj };
        }
    }

    private void FixedUpdate() 
    {
        currentState.StateUpdate(this);
        cycleAttackTimer -= Time.fixedDeltaTime;
        timeInState += Time.fixedDeltaTime;
        if(cycleAttackTimer < 0) { ChooseAttack(); }
    }

    public void EnterState(AIStateBase state)
    {
        //Debug.Log("Trying to enter state: " + state);
        if(currentState) 
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
        if (!controller.aggroTarget) return;
        
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
    }
}
