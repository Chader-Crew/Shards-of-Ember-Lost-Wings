using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTriggerStateBehaviour : MonoBehaviour
{    
    [SerializeField] private string triggerName;
    [SerializeField] private float time;
    private void OnEnable() 
    {
        this.CallWithDelay(()=>GetComponent<Animator>().SetTrigger(triggerName), time);
    }
}
