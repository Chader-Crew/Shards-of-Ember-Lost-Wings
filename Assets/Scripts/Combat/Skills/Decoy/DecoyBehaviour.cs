using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DecoyBehaviour : MonoBehaviour
{
    //cria um raio e qualquer inimigo na area eh atraido para o decoy

    private float radius = 7; //talvez esteja grande? gosto da ideia da area ser grande - ana
    private string enemyTag = "Enemy";
    private List<Collider> enemiesInRange;

    private void Start(){
        enemiesInRange = new List<Collider>();
        foreach (Collider enemy in Physics.OverlapSphere(transform.position, radius))
        {
            Debug.Log(enemy);
            if (enemy.CompareTag("Enemy"))
            {
                enemiesInRange.Add(enemy);
                NPCController enemyController = enemy.GetComponent<NPCController>();
                enemyController.SetAggroTarget("Decoy");
                enemyController.Character.OnDiedEvent += () => { enemiesInRange.Remove(enemy); };
            }
        }
    }

    void OnDisable()
    {
        try
        {
            foreach (Collider enemy in enemiesInRange)
            {
                enemy.GetComponent<NPCController>().SetAggroTarget("Player");
            }
        }catch{}
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
