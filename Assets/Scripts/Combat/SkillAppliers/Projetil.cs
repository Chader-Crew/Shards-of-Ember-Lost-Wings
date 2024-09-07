using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public SkillBase skill;
    public SkillData skillDT;
    [SerializeField] private Rigidbody rb;
    [SerializeField] Vector3 vel;
    [SerializeField] string targetTag;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.velocity = transform.right * vel.x + transform.up * vel.y + transform.forward * vel.z;
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag(targetTag))
        {
            skillDT.Target(other.gameObject.GetComponent<Character>());
            skill.Activate(skillDT);
        }
        Destroy(gameObject);
    }
}
