using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//classe controladora do player, qualquer comunicacao entre componentes deve passar por aqui.
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerController : Singleton<PlayerController>
{
    private PlayerMovement mov;
    public Character character;
    [SerializeField]private SkillTreeHolder state;
    [SerializeField] private InputReader input;
    private Animator animator;
    [SerializeField] Image stateIMG;
    private AtakaStateBehaviour atakaState;
    private SkillBase selectedSkill;

    private void Awake() 
    {
        stateIMG.sprite = state.stateIMG;
        //get components
        mov = GetComponent<PlayerMovement>();
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
        atakaState = animator.GetBehaviour<AtakaStateBehaviour>();

        //restart da input
        input.Initialize();

        //inscricao em eventos e atribuicao de actions
        input.OnMoveEvent += MoveInput;
        input.OnAttackEvent += AttackInput;
        character.OnGotHitEvent += SkillHit;
        input.OnSkillUseEvent += Cast;
        input.OnDragonStateEvent += ChangeState;
        character.OnDiedEvent += Die;
        character.OnDiedEvent += FindObjectOfType<DeathScreenBehaviour>().OnPlayerDeath;

        atakaState.AttackEndAction = AttackEnd;
    }

    //chamado quando o animator sai do state de ataque para destravar movimento (provavelmente devia ser mudado para |quando entra em idle|)
    private void AttackEnd()
    {
        mov.LockMovement(false);
    }

    private void MoveInput(Vector2 vector2)
    {
        if(vector2.magnitude == 0)
        {
            animator.SetBool("isRunning", false);
        }else
        {
            animator.SetBool("isRunning", true);
        }
        mov.movement = new Vector3(vector2.x, 0, vector2.y);
    }

    //ativa o ataque no animator e trava o movimento
    private void AttackInput()
    {
        animator.SetBool("ataka", true);
        mov.LockMovement(true);
    }


    private void SkillHit(SkillData skill, float damage)
    {
        StartCoroutine(Stagger(skill.stagger));
    }

    private void Die(){
        animator.SetTrigger("Die");
        character.enabled = false;
        character._invul = true;
        input.SetUI();
    }

    //ativa stagger e trava o movimento, depois de x segundos desabilita.
    IEnumerator Stagger(float seconds)
    {
        mov.LockMovement(true);
        animator.SetBool("isStaggered", true);
        yield return new WaitForSeconds(seconds);
        
        mov.LockMovement(false);
        animator.SetBool("isStaggered", false);
    }
    private void Cast()
    {
        selectedSkill= this.gameObject.GetComponent<SelectedSkill>().skill;
        SkillData data = new SkillData();
        data.owner = character;
        selectedSkill=state.activeSkill;
        selectedSkill.Activate(data);
    }
    private void ChangeState(int i)
    {
        if(i==(-1))
        {
            state=state.prev;
            state.Enter();
            stateIMG.sprite = state.stateIMG;
        }
        else if(i==(1))
        {
            state=state.next;
            state.Enter();
            stateIMG.sprite = state.stateIMG;

        }
    }


}
