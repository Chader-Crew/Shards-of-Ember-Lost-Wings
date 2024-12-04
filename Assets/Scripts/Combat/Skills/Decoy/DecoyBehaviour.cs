using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DecoyBehaviour : MonoBehaviour
{
    //cria um raio e qualquer inimigo na area eh atraido para o decoy

    private float radius = 7; //talvez esteja grande? gosto da ideia da area ser grande - ana
    private string enemyTag = "Enemy";
    private Collider[] enimiesInRange;

    private void Start(){
        enimiesInRange = Physics.OverlapSphere(transform.position, radius);
    }

    void FixedUpdate(){
        foreach(Collider enemy in enimiesInRange){
            if(enemy.CompareTag(enemyTag)){
                //set destination decoy
            }
        }
    }

    void OnDisable(){
        //set destination player
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
