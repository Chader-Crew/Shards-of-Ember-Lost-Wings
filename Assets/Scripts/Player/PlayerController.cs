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
    public PlayerMovement playerMovement;
    public Character character;
    [SerializeField]public SkillTreeHolder state;
    [SerializeField] private InputReader input;
    public Animator animator;
    public AudioSource audioSource;
    [SerializeField] Image stateIMG;
    [SerializeField] GameObject skilltree;
    [SerializeField] TMP_Text spText;
    private AtakaStateBehaviour atakaState;
    public float dashSpeed;
    public float dashTime;
    public float dashCoolDown;
    public bool canDash = true; 
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
    [SerializeField] public ParticleSystem swapVFXParticles;
    
    #endregion
    private void Awake() 
    {
        //stateIMG.sprite = state.stateIMG;
        //get components
        playerMovement = GetComponent<PlayerMovement>();
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
        SkillShards = 4;
        GainShards(1000);
        ChangeState(1);
    }

    //chamado quando o animator sai do state de ataque para destravar movimento (provavelmente devia ser mudado para |quando entra em idle|)
    private void AttackEnd()
    {
        playerMovement.LockMovement(false);
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
        playerMovement.movement = new Vector3(vector2.x, 0, vector2.y);
    }

    //ativa o ataque no animator e trava o movimento
    private void AttackInput()
    {
        animator.SetBool("ataka", true);
        playerMovement.LockMovement(true);
    }

    private void SkillHit(SkillData skill, float damage)
    {
        StartCoroutine(Stagger(skill.stagger));
    }

    private void Die(){
        animator.SetTrigger("Die");
        character.enabled = false;
        character.SetInvul(1);
        input.SetUI();
    }

    //ativa stagger e trava o movimento, depois de x segundos desabilita.
    IEnumerator Stagger(float seconds)
    {
        playerMovement.LockMovement(true);
        animator.SetBool("isStaggered", true);
        yield return new WaitForSeconds(seconds);
        
        playerMovement.LockMovement(false);
        animator.SetBool("isStaggered", false);
    }

    //cast da skill equipada em state.activeSkill
    private void Cast()
    {
        if(state._CanCastSkill)
        {
            //criacao de SkillData
            SkillData data = new SkillData();
            data.owner = character;

            //ativa a skill
            state.ActiveSkill.Activate(data);
            OnCastEvent(state.ActiveSkill);
        }
    }

    //troca de modo elemental
    public void ChangeState(int i)
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

        ParticleSystem.MainModule module = swapVFXParticles.main;
        module.startColor = state.particleColor;
        
        swapVFXParticles.Play();
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

        animator.CrossFadeInFixedTime("ShoveDash", 0.1f);

        while(Time.time < startTime + dashTime)
        {
            playerMovement.characterController.Move(playerMovement.movement * dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCoolDown);        
        canDash = true;
    }
}
