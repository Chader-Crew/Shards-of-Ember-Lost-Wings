using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    public float explosionDelay = 3f; // Tempo até a bomba explodir
    public float explosionRadius = 5f; // Raio da explosão
    public float explosionForce = 700f; // Força da explosão
    public int damage = 2; // Dano da explosão

    private void Start(){
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay(){
        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders){
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null){
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            Character character = nearbyObject.GetComponent<Character>();
            if (character != null){
                SkillData data = new SkillData { damage = damage, owner = null };
                character.GetHit(data);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
