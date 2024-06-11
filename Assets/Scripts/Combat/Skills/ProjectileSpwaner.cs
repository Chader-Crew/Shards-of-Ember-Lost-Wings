using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Projectile Spawner")]

public class ProjectileSpwaner : SkillBase
{
    
    [SerializeField]private GameObject projectile;

    public override void Activate(SkillData context)
    {
        if (canCast)
        {
            Projetil projetil;
            projetil=Instantiate(projectile,context.owner.transform.position,context.owner.transform.rotation).GetComponent<Projetil>();
            projetil.skillDT=context;
        }
    }
}
