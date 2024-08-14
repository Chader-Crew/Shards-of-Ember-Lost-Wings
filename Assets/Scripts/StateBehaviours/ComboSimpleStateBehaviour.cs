using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//comportamento de parte de combo linear. Desliga o trigger de proximo ataque se nao tiver apertado durante a janela de buffer.
public class ComboSimpleStateBehaviour : StateMachineBehaviour
{
    [SerializeField] private float bufferTime;
    private float normalizedBuffer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        normalizedBuffer = 1- bufferTime / stateInfo.length;
        //Debug.Log($"Normalized animation buffer for {animator.name}: {normalizedBuffer}");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("ataka") == true)
        {
            if(stateInfo.normalizedTime < bufferTime)
            {
                animator.ResetTrigger("ataka");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
