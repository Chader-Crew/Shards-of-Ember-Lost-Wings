using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSkill : MonoBehaviour
{
    PlayerController playerController;    
    public float attackBoostDuration = 7f;
    public float boostedAttackSpeed = 1.5f; 
    private float originalSpeed;

    private void Start()
    {
        originalSpeed = playerController.animator.speed;
    }

    public void ActivateAttackBoost()
    {
        // Aumenta a velocidade de todas as animações
       playerController.animator.speed = boostedAttackSpeed;

       playerController.animator.SetBool("Ataka", true);

        // Reseta o efeito após a duração do boost
       Invoke("ResetAttackSpeed", attackBoostDuration);
    }

    private void ResetAttackSpeed()
    {
        // Restaura a velocidade original do Animator
        playerController.animator.speed = originalSpeed;

        // Desativa a animação de ataque (para interromper o combo)
        playerController.animator.SetBool("Ataka", false);
    }
}
