using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    private float damagePerSecond;
    private float duration;
    private Character target;

    public void Initialize(float damagePerSecond, float duration, Character target){
        this.damagePerSecond = damagePerSecond;
        this.duration = duration;
        this.target = target;
        StartCoroutine(ApplyPoison());
    }

    private IEnumerator ApplyPoison(){
        float elapsed = 0;
        while (elapsed < duration){
            target.GetHit(new SkillData { damage = damagePerSecond });
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
        Destroy(this);
    }
}
