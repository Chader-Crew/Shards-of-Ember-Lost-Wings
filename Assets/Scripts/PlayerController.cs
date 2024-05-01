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
    bool _canMove;
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

        //inicializacao misc
        _canMove = true;
    }

    //chamado quando o animator sai do state de ataque
    private void AttackEnd()
    {
        _canMove = true;
    }

    public void MoveInput(Vector2 dir)
    {
        if (_canMove)
        {
            if(dir.magnitude != 0)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                movement.Move(new Vector3(dir.x, 0, dir.y));
            }
        } 
    }

    public void AttackInput()
    {
        if(_canMove)
        {
            animator.SetBool("ataka", true);
            _canMove = false;
        }

    }


    //ativa stagger, depois de x segundos desabilita.
    public void Stagger(float seconds)
    {
        StartCoroutine("Stagger", seconds);
        IEnumerator timer(float seconds)
        {
            _canMove = false;
            animator.SetBool("isStaggered", true);
            yield return new WaitForSeconds(seconds);
            _canMove = true;
            animator.SetBool("isStaggered", false);
        }
    }
}
