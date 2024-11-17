using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathBehaviour : MonoBehaviour
{
    public float damageDelay = 1f, damage = 3f, lastDamage;
    [SerializeField] private SkillData skillData;
    [SerializeField] private SkillBase skill;

    void OnTriggerStay(Collider collider){
        if(collider.gameObject.CompareTag("Enemy")){
            if(Time.time - lastDamage >= damageDelay){
                Character enemy = collider.gameObject.GetComponent<Character>();
                enemy.GetHit(new SkillData().Damage(damage).Owner(PlayerController.Instance.character));
                lastDamage = Time.time;
            }
        }
    }

}
