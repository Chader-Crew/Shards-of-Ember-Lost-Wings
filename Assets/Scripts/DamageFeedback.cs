using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TMP_Text damageTextPrefab;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float textDuration;


    private void Start() 
    {
        character.OnGotHitEvent += DisplayDamage;
    }

    public void DisplayDamage(SkillData data, float damage)
    {
        TMP_Text newText = GameObject.Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
        newText.text = damage.ToString("F0");

        newText.transform.localPosition = offset;
        Destroy(newText, textDuration);
    }
}
