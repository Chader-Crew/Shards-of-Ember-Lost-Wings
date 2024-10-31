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
    public Character aggroTarget;
    [SerializeField] public bool _doCharDetection;
    [SerializeField] private AIStateBase charDetectedState;
    [SerializeField] private float charDetectionRadius;
    [SerializeField] private string aggroTargetTag;

    private float originalSpeed;
    private Vector3 extraMovement;  //vetor LOCALIZADO. entao vector3.forward sempre move em transform.forward 
    private Vector3 forcedMovement; //vetor GLOBAL 

    [HideInInspector] public Vector3 wanderSmooth;   //vetor para implementação de algoritmos de vagueio

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
        navAgent.Move(forcedMovement * Time.fixedDeltaTime);
    }

    private void Die()
    {
        animator.Play("DIE");
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

    //aggro
    private void DetectCharacters()
    {
        //cancela se nao e pra detectar
        if(!_doCharDetection) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, charDetectionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag(aggroTargetTag))
            {
                Character otherChar = collider.GetComponent<Character>();
                aggroTarget = otherChar;

                if(charDetectedState)
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, charDetectionRadius);
    }


    public void ResetAttack()
    {
        if(aggroTarget)
        {
            stateMachine.EnterStateType(AIStateBase.AIStateType.CHASING);
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
    
    //move o destino da navmesh de acordo com um wandering suave
    public void NudgeDestination(float variance)
    {
        wanderSmooth = Quaternion.Euler(0, UnityEngine.Random.Range(-variance, variance), 0) * wanderSmooth.normalized;
        navAgent.destination +=  wanderSmooth * navAgent.speed/20;
    }

    public void ResetSpeed()
    {
        navAgent.speed = originalSpeed;
    }

    public void MoveForwardForSeconds(float seconds)
    {
        extraMovement += Vector3.forward;
        Invoke("StopExtraMovement", seconds);
    }

    public void MoveBackwardForSeconds(float seconds)
    {
        extraMovement += Vector3.back;
        Invoke("StopExtraMovement", seconds);
    }
    
    public void MoveInDirectionForSeconds(Vector3 direction, float seconds)
    {
        forcedMovement += direction;
        Invoke("StopExtraMovement", seconds);
    }
    public void StopExtraMovement()
    {
        extraMovement = Vector3.zero;
        forcedMovement = Vector3.zero;
    }

    //coisas de animacao


    //metodo publico para tocar uma animacao assim que possivel (nao interrompendo transicoes)
    public void PlayAnimation(string animationStateName)
    {
       // Debug.Log("Trying to start animation "+ animationStateName);
        StopCoroutine("PlayAnimWaitForTransition");
        StartCoroutine("PlayAnimWaitForTransition", animationStateName);
    }

    //corotina que espera qualquer transicao em progresso acabar antes de ativar a animacao com transicao.
    //necessario por que CrossFadeInFixedTime nao faz o fade corretamente se o animator ja estiver atualmente transicionando outras animacoes.
    private IEnumerator PlayAnimWaitForTransition(string animationStateName)
    {
        //checa se o animator tem um estado com o tal nome pra nao dar warning
        if(!animator.HasState(0, Animator.StringToHash(animationStateName)))
        {
            yield break;
        }

        //espera a transicao terminar antes de mandar o animator transicionar
        while(animator.IsInTransition(0))
        {
            //Debug.Log("Waiting for transition");
            //Debug.Log("Current state is: " + stateMachine.CurrentState);
            yield return new WaitForEndOfFrame();
        }

        //toca a animacao com transicao de tanto segundos
        animator.CrossFadeInFixedTime(animationStateName, 0.3f);
    }
}
