using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSkillOnTarget : MonoBehaviour
{
    private SkillBase skillToCast;
    private SkillData newContext;
    private Character target;
    int timesCast;
    public void Initialize(SkillBase skill, SkillData context, int count, float interval)
    {
        transform.SetParent(target.transform);
        skillToCast = skill;
        timesCast = 0;
        newContext = new SkillData().Target(context.targets[0]).Owner(context.owner);
        StartCoroutine(skillCoroutine(count, interval));
    }

    private IEnumerator skillCoroutine(int count, float interval)
    {
        while(timesCast < count)
        {
            yield return new WaitForSeconds(interval);
            timesCast++;

            skillToCast.Activate(newContext);
        }
    }
}
