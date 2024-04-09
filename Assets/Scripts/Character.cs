using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe para personagens envolvidos em combate.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CharStats))]
[RequireComponent(typeof(PlayerStateMachine))]
public class Character : MonoBehaviour, IDamageable
{
    #region Declarations
    [SerializeField] protected CharStats stats;   //Stat sheet
    [SerializeField] protected Rigidbody rb;      //Rigidbody para movimentacao
    [SerializeField] protected Collider col;      //Colisao de combate (hurtbox) (vai ser a mesma que a de navegacao pq a gente nao precisa dessa complexidade)
    [SerializeField] protected PlayerStateMachine stateMachine; //state machine que leva stagger
    
    #endregion
    
    private void Awake() 
    {
        //getcomponents
        stats = GetComponent<CharStats>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    #region Interfaces
    //Interface IDamageable
    public float HP => stats.hp;

    public virtual void Die()
    {
        Debug.Log("Personagem morreu");
    }

    public virtual void GetHit(SkillData data)
    {
        /*
        calculo padrao de dano. 
        funcao linear de dano otimizada pq risk of rain 2 jogo bom (nao consegui achar funcao barata que faz com a proporcao certinha)
        cada ponto de defesa adicional diminui o ataque recebido em uma proporcao similar a anterior.
        com uma proporcao de 0.05 cada ponto de dano reduz o dano em cerca de 5%, e essa proporcao reduz quanto mais def.

        DanoFinal = dano/(proporcao * defesa + 1)
        */
        float totalDamage = data.damage/(0.05f * stats.def + 1);
        totalDamage = Mathf.Round(totalDamage * 100)/100;

        stats.hp -= totalDamage;
        Debug.Log($"levou {totalDamage} de dano");
        if(stats.hp <= 0) { Die(); }

        if(data.stagger > 0) { stateMachine.Stagger(data.stagger); }
    }
    #endregion
}
