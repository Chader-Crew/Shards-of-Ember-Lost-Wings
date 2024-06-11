using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/TesteSD")]
public class testeSD : SkillBase
{
    [SerializeField]private GameObject bola;

    public override void Activate(SkillData context)
    {
        Projetil projetil;
        projetil=Instantiate(bola,context.owner.transform.position,context.owner.transform.rotation).GetComponent<Projetil>();
        projetil.skillDT=context;
    }
}
