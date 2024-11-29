using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePunchBehaviour : MonoBehaviour
{
    public float damageDelay = 0f, damage = 6f, lastDamage;
    [SerializeField] private SkillData skillData;


    void Start(){
        GameObject player = GameObject.Find("Player");
        transform.rotation = player.transform.rotation;
        transform.position -= new Vector3 (0,1,0);
    }

    void OnTriggerStay(Collider collider){
        if(collider.gameObject.CompareTag("Enemy")){
            if(Time.time - lastDamage >= damageDelay){ //dar dano de segundos em segundo if > 0//
                Character enemy = collider.gameObject.GetComponent<Character>();
                enemy.GetHit(new SkillData().Damage(damage).Owner(PlayerController.Instance.character));
                lastDamage = Time.time;
            }
        }
    }
}
