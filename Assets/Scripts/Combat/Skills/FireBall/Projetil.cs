using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public SkillBase skill;
    public SkillData skillDT;
    [SerializeField] private Rigidbody rb;
    [SerializeField]float vel;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.velocity =transform.forward*vel;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            skill.Activate(skillDT);
            Destroy(gameObject);
        }
    }
}
