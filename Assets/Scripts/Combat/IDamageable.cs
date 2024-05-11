using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface para ataques darem dano em coisas
public interface IDamageable
{
    //propriedade publica HP pra ataques poderem ler o valor de HP de danificaveis
    public float HP
    {
        get;
    }
    
    //metodo de levar dano
    public void GetHit(SkillData data);
    //metodo de destruir
    public void Die();
}
