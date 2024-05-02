using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//classe controladora do player, qualquer comunicacao entre componentes deve passar por aqui.
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    [SerializeField] private InputReader input;
    private Animator animator;
    private AtakaStateBehaviour atakaState;

    private void Awake() 
    {
        //get components
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

        //inscricao em eventos e atribuicao de actions
        input.OnMoveEvent += MoveInput;
        input.OnAttackEvent += AttackInput;
        atakaState = animator.GetBehaviour<AtakaStateBehaviour>();
        atakaState.AttackEndAction = AttackEnd;
    }

    //chamado quando o animator sai do state de ataque para destravar movimento (provavelmente devia ser mudado para |quando entra em idle|)
    private void AttackEnd()
    {
        movement.LockMovement(false);
    }

    public void MoveInput(Vector2 dir)
    {
        if(dir.magnitude == 0)
        {
            animator.SetBool("isRunning", false);
        }else
        {
            animator.SetBool("isRunning", true);
        }
        movement.Move(new Vector3(dir.x, 0, dir.y));
    }

    //ativa o ataque no animator e trava o movimento
    public void AttackInput()
    {
        animator.SetBool("ataka", true);
        movement.LockMovement(true);
    }


    //ativa stagger e trava o movimento, depois de x segundos desabilita.
    public void Stagger(float seconds)
    {
        StartCoroutine("Stagger", seconds);

        IEnumerator timer(float seconds)
        {
            movement.LockMovement(true);
            animator.SetBool("isStaggered", true);

            yield return new WaitForSeconds(seconds);
            
            movement.LockMovement(false);
            animator.SetBool("isStaggered", false);
        }
    }
}
