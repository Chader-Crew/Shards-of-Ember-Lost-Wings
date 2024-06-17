using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSkillOnTarget : MonoBehaviour
{
    private SkillBase skillToCast;
    private SkillData newContext;
    int timesCast;
    public void Initialize(SkillBase skill, SkillData context, int count, float interval)
    {
        newContext = new SkillData().Target(context.targets[0]).Owner(context.owner);
        transform.SetParent(newContext.targets[0].transform);
        skillToCast = skill;
        timesCast = 0;
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
