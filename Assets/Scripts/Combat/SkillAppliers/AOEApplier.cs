using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEApplier : MonoBehaviour
{
    [SerializeField]
    private float explosionDelay = 3f;  // Tempo até a bomba explodir

    [SerializeField]
    private float explosionRadius = 5f; // Raio da explosão

    [SerializeField]
    private SkillBase skill;    // Skill aplicada

    [SerializeField]
    private GameObject explosionVFX;    // Efeito visual

    [SerializeField]
    private string tagFilter;

    [SerializeField]
    private bool _playerOwned;

    private void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
        explosionVFX.SetActive(false);
    }

    private IEnumerator ExplodeAfterDelay(){
        yield return new WaitForSeconds(explosionDelay);
        explosionVFX.SetActive(true);
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders){
            if(nearbyObject.tag != tagFilter) { continue; } //pula se nao for da tag

            Character character = nearbyObject.GetComponent<Character>();
            
            if (character != null){
                SkillData data = new SkillData().Target(character);
                if(_playerOwned) { data.Owner(PlayerController.Instance.character); }
                skill.Activate(data);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
