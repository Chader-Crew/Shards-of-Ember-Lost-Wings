using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe para personagens envolvidos em combate/dano.
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CharStats))]
public class Character : MonoBehaviour, IDamageable
{
    #region Declarations
    //vars
    [SerializeField] protected CharStats stats;   //Stat sheet
    [SerializeField] public CharStats Stats { get { return stats;} }    //Propriedade publica readonly
    [SerializeField] protected Collider col;      //Colisao de combate (hurtbox)

    public bool _invul; 
    

    //events
    public event Action<SkillData, float> OnGotHitEvent = (s,f)=>{};    //O FLOAT É A QUANTIDADE DE DANO LEVADO. É diferente do valor de dano na SkillData, que não leva em conta a defesa do personagem.
    public event Action OnDiedEvent = ()=>{};
    #endregion
    
    private void Awake() 
    {
        //getcomponents
        stats = GetComponent<CharStats>();
        col = GetComponent<Collider>();
        _invul = false;

        stats.hp = stats.maxHp;
    }

    #region Interfaces
    //Interface IDamageable
    public float HP => stats.hp;

    public virtual void Die()
    {
        OnDiedEvent();
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

        if(_invul){
            return;
        }

        float totalDamage = data.damage/(0.05f * stats.def + 1);
        totalDamage = Mathf.Round(totalDamage * 100)/100;
        /*if(skilltreeManager.skill1==true|| this.tag=="Enemy")
        {
            float dur=3;
            this.gameObject.GetComponent<canBurn>().Burn(dur);
        }*/
        stats.hp -= totalDamage;
        Debug.Log($"levou {totalDamage} de dano");
        OnGotHitEvent(data, totalDamage);

        if(stats.hp <= 0) { Die(); }

        
    }
    #endregion

    public void restoreLife(float value){
        stats.hp += value;
        HPGambiarra.instance.RestoreBar(stats.hp);
    }

    public void activeBuff(int amount){
        StartCoroutine(buffPotion(amount));
    }

    IEnumerator buffPotion(int _amount){
        stats.atk += _amount;
        yield return new WaitForSeconds(6);
        stats.atk -= _amount;
    }

}
