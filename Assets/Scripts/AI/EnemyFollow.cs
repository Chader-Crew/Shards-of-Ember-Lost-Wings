using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
public NavMeshAgent enemy;
public Transform player;
public Animator animator;
   
   private void Start() 
   {
        player = GameObject.FindGameObjectWithTag("Player").transform;
   }
    void Update()
    {
        enemy.SetDestination(player.position);
        if(Vector3.Distance(enemy.destination, transform.position) <2){ animator.SetTrigger("ataka"); }
    }
}
