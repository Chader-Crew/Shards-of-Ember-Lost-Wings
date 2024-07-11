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
    [SerializeField] private Item itemToDrop;


    //deteccao de personagem pra aggro
    [Space(15)]
    [HideInInspector] public Character aggroTarget;
    [SerializeField] public bool _doCharDetection;
    [SerializeField] private AIStateBase charDetectedState;
    [SerializeField] private float charDetectionRadius;
    [SerializeField] private string aggroTargetTag;

    private float originalSpeed;
    private Vector3 extraMovement; //vetor LOCALIZADO. entao vector3.forward sempre move em transform.forward  

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        character.OnDiedEvent += Die;
        originalSpeed = navAgent.speed;
        extraMovement = Vector3.zero;
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

        navAgent.Move(transform.TransformDirection(extraMovement) * Time.fixedDeltaTime * navAgent.speed);
    }

    private void Die()
    {
        animator.Play("Die");
        this.enabled = false;
        character.enabled = false;
        navAgent.enabled = false;
        stateMachine.enabled = false;
        PlayerController.Instance.GainShards(UnityEngine.Random.Range(minDropShards,maxDropShards));
        if(itemToDrop != null)
        {
            if(UnityEngine.Random.Range(0,2) != 0)
            {
                InventoryManager.instance.AddItem(itemToDrop);
            }
        }
    }

    private void RemoveBody()
    {
        Destroy(gameObject);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, charDetectionRadius);
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

    //metodos de movimentacao
    public void SetDestination(Vector3 targetPos)
    {
        navAgent.SetDestination(targetPos);
    }
    public void UnSetDestination()
    {
        navAgent.ResetPath();
    }
    public void FleeFrom(Vector3 targetPos)
    {
        navAgent.SetDestination(transform.position + (transform.position - targetPos));
    }
    
    public void ChangeSpeed(float modifier)
    {
        navAgent.speed = navAgent.speed * modifier;
    }
    public void ResetSpeed()
    {
        navAgent.speed = originalSpeed;
    }

    public void MoveForwardForSeconds(float seconds)
    {
        extraMovement = Vector3.forward;
        Invoke("StopExtraMovement", seconds);
    }

    public void MoveBackwardForSeconds(float seconds)
    {
        extraMovement = Vector3.back;
        Invoke("StopExtraMovement", seconds);
    }

    public void StopExtraMovement()
    {
        extraMovement = Vector3.zero;
    }

    //coisas de animacao
    public void PlayAnimation(string animationStateName)
    {
       // Debug.Log("Trying to start animation "+ animationStateName);
        StopCoroutine("PlayAnimWaitForTransition");
        StartCoroutine("PlayAnimWaitForTransition", animationStateName);
    }
    private IEnumerator PlayAnimWaitForTransition(string animationStateName)
    {
        while(animator.IsInTransition(0))
        {
            //Debug.Log("Waiting for transition");
            //Debug.Log("Current state is: " + stateMachine.CurrentState);
            yield return new WaitForEndOfFrame();
        }
        animator.CrossFadeInFixedTime(animationStateName, 0.3f);
    }
}
