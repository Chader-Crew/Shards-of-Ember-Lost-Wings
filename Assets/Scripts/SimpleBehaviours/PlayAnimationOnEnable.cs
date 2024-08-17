using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayAnimationOnEnable : MonoBehaviour
{
    private Animator anim;
    public string stateName;
    public bool _disableOnAnimationEnd;

    private void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.Play(stateName);
        if(_disableOnAnimationEnd)
        {
            Invoke("DisableObject", anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        }
    }
    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
