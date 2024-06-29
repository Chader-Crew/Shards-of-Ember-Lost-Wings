using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Character))]
[RequireComponent(typeof(AIStateMachine))]
public class NPCController : MonoBehaviour
{
    [SerializeField] public NavMeshAgent navAgent;
    [SerializeField] public Animator animator;
    [SerializeField] private Character character;
    [SerializeField] private AIStateMachine stateMachine;
    [SerializeField] private int minDropShards;
    [SerializeField] private int maxDropShards;


    //deteccao de personagem pra aggro
    [Space(15)]
    [HideInInspector] public Character aggroTarget;
    [SerializeField] public bool _doCharDetection;
    [SerializeField] private AIStateBase charDetectedState;
    [SerializeField] private float charDetectionRadius;
    [SerializeField] private string aggroTargetTag;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        character.OnDiedEvent += Die;
    }

    private void Die()
    {
        animator.Play("Die");
        this.enabled = false;
        character.enabled = false;
        navAgent.enabled = false;
        stateMachine.enabled = false;
        PlayerController.Instance.GainShards(UnityEngine.Random.Range(minDropShards,maxDropShards));
    }

    private void RemoveBody()
    {
        Destroy(gameObject);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if(aggroTarget == null)
        {
            DetectCharacters();
        }
    }

    private void DetectCharacters()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, charDetectionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag(aggroTargetTag))
            {
                Character otherChar = collider.GetComponent<Character>();
                aggroTarget = otherChar;

                if(charDetectedState != null)
                {
                    stateMachine.EnterState(charDetectedState);
                }else{
                    stateMachine.EnterStateType(AIStateBase.AIStateType.CHASING);
                }
                break;
            }
        }
    }

    public void SetDestination(Vector3 targetPos)
    {
        navAgent.SetDestination(targetPos);
    }

    private void ResetAttack()
    {
        if(aggroTarget != null)
        {
            if(charDetectedState != null)
            {
                    stateMachine.EnterState(charDetectedState);
                }else{
                    stateMachine.EnterStateType(AIStateBase.AIStateType.CHASING);
            }
        }else{
            stateMachine.EnterStateType(AIStateBase.AIStateType.IDLE);
        }

        stateMachine.ChooseAttack();
    }

    public float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, aggroTarget.transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, charDetectionRadius);
    }

    internal void FleeFrom(Vector3 targetPos)
    {
        navAgent.SetDestination(transform.position + (transform.position - targetPos));
    }
}
