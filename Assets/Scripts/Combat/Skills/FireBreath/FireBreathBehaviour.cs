using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathBehaviour : MonoBehaviour
{
    public float damageDelay = 1f, damage = 3f, lastDamage;
    [SerializeField] private SkillData skillData;


    void Start(){
        //alocando o firebreath na posicao do player
        GameObject player = GameObject.Find("Player");
        transform.SetParent(player.transform);
        transform.position -= new Vector3 (0,1,0);
        transform.rotation = player.transform.rotation;
    }

    void OnTriggerStay(Collider collider){
        if(collider.gameObject.CompareTag("Enemy")){
            if(Time.time - lastDamage >= damageDelay){ //dar dano de segundos em segundo if > 0
                Character enemy = collider.gameObject.GetComponent<Character>();
                enemy.GetHit(new SkillData().Damage(damage).Owner(PlayerController.Instance.character));
                lastDamage = Time.time;
            }
        }
    }

}
