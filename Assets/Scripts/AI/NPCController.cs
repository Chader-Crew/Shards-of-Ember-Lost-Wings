using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class NPCController : MonoBehaviour
{
    private PlayerMovement movement;
    private Animator animator;
    private AtakaStateBehaviour atakaState;
    private Character character;

    private void Awake() 
    {
        //get components
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();

        //inscricao em eventos e atribuicao de actions
        atakaState = animator.GetBehaviour<AtakaStateBehaviour>();
        atakaState.AttackEndAction = AttackEnd;
        character.OnDiedEvent += EnemyDie;
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

    //morte do inimigo
    public void EnemyDie(){
        Destroy(gameObject);
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
