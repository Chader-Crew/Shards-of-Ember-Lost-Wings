using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//classe controladora do player, qualquer comunicacao entre componentes deve passar por aqui.
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class PlayerController : Singleton<PlayerController>
{
    #region Declarations
    public PlayerMovement mov;
    public Character character;
    [SerializeField]public SkillTreeHolder state;
    [SerializeField] private InputReader input;
    private Animator animator;
    public AudioSource audioSource;
    [SerializeField] Image stateIMG;
    [SerializeField] GameObject skilltree;
    [SerializeField] TMP_Text spText;
    private AtakaStateBehaviour atakaState;
    public float dashSpeed;
    public float dashTime;
    public float dashCoolDown;
    public bool canDash = true;
    private SkillBase selectedSkill;
    private int _skillShards;
    public int SkillShards
    {
        get => _skillShards;
        private set => _skillShards = value;
    }
    private int _statShards;
    public int StatShards
    {
        get => _statShards;
        private set => _statShards = value;
    }
    [SerializeField] ShardsGotPopup shardPopupText;
    public Action<SkillBase> OnCastEvent;   
    [SerializeField] private SkinnedMeshRenderer armorMesh;
    [SerializeField] private SkinnedMeshRenderer bodyMesh;
    [SerializeField] private GameObject swapVFXObject;
    
    #endregion
    private void Awake() 
    {
        //stateIMG.sprite = state.stateIMG;
        //get components
        mov = GetComponent<PlayerMovement>();
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        atakaState = animator.GetBehaviour<AtakaStateBehaviour>();
        //shardPopupText = FindObjectOfType<ShardsGotPopup>(true);

        //restart da input
        input.Initialize();

        //inscricao em eventos e atribuicao de actions
        InputReader.OnMoveEvent += MoveInput;
        InputReader.OnAttackEvent += AttackInput;
        character.OnGotHitEvent += SkillHit;
        InputReader.OnSkillUseEvent += Cast;
        InputReader.OnDragonStateEvent += ChangeState;
        character.OnDiedEvent += Die;
        //input.OnPauseEvent += OpenSkillTree;
        //character.OnDiedEvent += FindObjectOfType<DeathScreenBehaviour>(true).OnPlayerDeath;

        //reinicializacao de eventos
        OnCastEvent = (SkillBase s)=>{};

        atakaState.AttackEndAction = AttackEnd;
        InputReader.OnDashEvent += DashAction;
    }
    void Start()
    {
        //spText.text="Skillpoints "+skillShards;
        SkillShards = 1;
    }

    //chamado quando o animator sai do state de ataque para destravar movimento (provavelmente devia ser mudado para |quando entra em idle|)
    private void AttackEnd()
    {
        mov.LockMovement(false);
    }

    private void MoveInput(Vector2 vector2)
    {
        if(vector2.magnitude > 0.1f)
        {
            animator.SetBool("isRunning", true);
            audioSource.enabled = true;
        }else
        {
            animator.SetBool("isRunning", false);
            audioSource.enabled = false;
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
        if(state.activeSkill!=null && !state.activeSkill._onCooldown)
        {
            //criacao de SkillData
            SkillData data = new SkillData();
            data.owner = character;

            //ativa a skill
            state.activeSkill.Activate(data);
            OnCastEvent(state.activeSkill);

            //se tem cooldown poe em cooldown
            if(state.activeSkill.cooldown >0){
                state.activeSkill._onCooldown = true;
            }
        }
    }

    //troca de modo elemental
    private void ChangeState(int i)
    {
        if(i==(-1))
        {
            state=state.prev;
        }
        else if(i==(1))
        {
            state=state.next;
        }else{
            return;
        }
        
        state.Enter();
        stateIMG.sprite = state.stateIMG;       //troca de interface, mesh e trigger de vfx
        bodyMesh.sharedMesh = state.BodyMesh;
        armorMesh.sharedMesh = state.ArmorMesh;
        swapVFXObject.SetActive(true);
    }

    public void GainShards(int ammount)
    {
        StatShards += ammount;
        shardPopupText.Popup(ammount);
    }
    public void SpendShards(int ammount)
    {
        StatShards -= ammount;
    }

    //coolDown, limita o tempo de uso do Dash
    private void DashAction()
    {   
        if(canDash)
        {
            StartCoroutine(DashSkill());
        }
    }

    //Tempo de coolDown, velocidade do Dash e tempo de duração
    IEnumerator DashSkill()
    {   
        canDash = false;
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            mov.characterController.Move(mov.movement * dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCoolDown);        
        canDash = true;
    }
}
