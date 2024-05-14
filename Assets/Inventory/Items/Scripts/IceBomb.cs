using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBomb : MonoBehaviour
{
    //Minha ideia é que na explosao ele tenha uma range, ele pega todos os inimigos presentes na range
    // e coloca o setdestination deles pra eles mesmos (por um enumerator)

    public float explosionRadius = 5f;
    public Vector3 explosionOffset = new Vector3(0, 0.5f, 0);
    public GameObject explosionEffectPrefab; //vfx

    /*void Start(){
        Explode();
    }*/

    void Explode(){
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position + explosionOffset, Quaternion.identity);
        Destroy(explosionEffect, 6f);
        Destroy(gameObject);
        //Freeze();
    }

    void Freeze(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider enemies in colliders){
            enemies.GetComponent<EnemyFollow>().enemy.SetDestination(transform.position);
        }
    }

    void OnTriggerEnter(Collider other){
        Explode();
    }
}
