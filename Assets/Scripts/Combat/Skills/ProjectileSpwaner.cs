using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Projectile Spawner")]

public class ProjectileSpwaner : SkillBase
{
    [SerializeField] private Vector3 offset;

    [SerializeField]private GameObject projectile;

    public override void Activate(SkillData context)
    {
	    Transform ownerTransf = context.owner.transform;
	    Projetil projetil = Instantiate
	    (projectile,
		    ownerTransf.position +
		    ownerTransf.right * offset.x +
		    ownerTransf.forward * offset.z +
		    ownerTransf.up * offset.y,
		    context.owner.transform.rotation
	    ).GetComponent<Projetil>();
	    projetil.skillDT = context;
    }

    public override void Comprado(CharStats player)
    {
        canCast = true;
    }
}
