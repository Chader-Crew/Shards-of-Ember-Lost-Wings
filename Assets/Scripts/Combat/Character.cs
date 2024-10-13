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
    [SerializeField] private CharStats stats;   //Stat sheet
    [SerializeField] public CharStats Stats { get { return stats;} }    //Propriedade publica readonly
    [SerializeField] protected Collider col;      //Colisao de combate (hurtbox)
    [SerializeField] private Renderer renderer; //Renderer pra troca de materiais
    [SerializeField] protected Material damageFlashingMaterial;     //Material que faz piscar vermelho quando leva dano

    private bool _invul;
    private bool isPoisonBuffActive;
    private float poisonDuration = 2f; // Duração do efeito de veneno em segundos
    private float poisonDamagePerSecond = 0.5f; // Dano do veneno por segundo
    

    //events
    public event Action<SkillData, float> OnGotHitEvent = (s,f)=>{};    //O FLOAT É A QUANTIDADE DE DANO LEVADO. É diferente do valor de dano na SkillData, que não leva em conta a defesa do personagem.
    public event Action<float> OnHealEvent = (f) => {};
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

    /*
    calculo padrao de dano.
    funcao linear de dano que eu peguei na wiki de risk of rain 2 jogo bom (nao consegui achar funcao barata que faz com a proporcao certinha)
    cada ponto de defesa adicional diminui o ataque recebido em uma proporcao similar a anterior.
    com uma proporcao de 0.05 cada ponto de dano reduz o dano em cerca de 5%, e essa proporcao reduz quanto mais def.

    DanoFinal = dano/(proporcao * defesa + 1)
    */
    public virtual void GetHit(SkillData data)
    {

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
        //Debug.Log($"levou {totalDamage} de dano");
        OnGotHitEvent(data, totalDamage);
        StartCoroutine(DamageFlashing());

        if(stats.hp <= 0) 
        { 
            Die(); 
            _invul = true;
        }

        
    }
    #endregion

    //corotina para trocar os materiais pro feedback de dano e trocar de volta
    public IEnumerator DamageFlashing()
    {
        List<Material> previousMats = new List<Material>();
        renderer.GetMaterials(previousMats);

        //checagem para evitar duas corotinas rodarem e bugar
        if(previousMats[0] != damageFlashingMaterial)
        {
            //fazendo lista com so o material do flash pra substituir todos os materiais
            List<Material> nextMats = new List<Material>();
            for(int i = 0; i < renderer.materials.Length; i++)
            {
                nextMats.Add(damageFlashingMaterial);
            }
            //substitui todos os materiais e volta eles depois de um tempo
            renderer.SetMaterials(nextMats);
            yield return new WaitForSeconds(0.1f);
            renderer.SetMaterials(previousMats);
        }
    }

    public void restoreLife(float value){
        float healVal = Mathf.Clamp(value, 0, stats.maxHp - stats.hp);
        stats.hp += healVal;
        OnHealEvent(healVal);
    }

    public void SetInvul(int zeroOne)
    {
        _invul = zeroOne != 0;
    }

    public void activeBuff(int amount)
    {
        TemporaryAtk(amount, 6);
    }

    public void ActivatePoisonBuff(float duration){
        StartCoroutine(PoisonBuffCoroutine(duration));
    }

    private IEnumerator PoisonBuffCoroutine(float duration){
        isPoisonBuffActive = true;
        yield return new WaitForSeconds(duration);
        isPoisonBuffActive = false;
    }

    public bool IsPoisonBuffActive(){
        return isPoisonBuffActive;
    }

    public float GetPoisonDamagePerSecond(){
        return poisonDamagePerSecond;
    }

    public float GetPoisonDuration(){
        return poisonDuration;
    }
     //metodos de alteracao temporaria de stats (para alteracoes de stats com duracao fixa)
    #region Temporary Stats Methods
    public void TemporaryAtk(int variation, float time)
    {
        stats.atk += variation;
        this.CallWithDelay(() => stats.atk -= variation, time);
    }

    public void TemporaryDef(int variation, float time)
    {
        stats.def += variation;
        this.CallWithDelay(() => stats.def -= variation, time);
    }

    public void TemporarySpd(int variation, float time)
    {
        stats.spd += variation;
        this.CallWithDelay(() => stats.spd -= variation, time);
    }

    public void TemporaryMaxHp(int variation, float time)
    {
        stats.maxHp += variation;
        this.CallWithDelay(() => stats.maxHp -= variation, time);
    }
    #endregion
}
