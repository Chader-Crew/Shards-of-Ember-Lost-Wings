using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classe controladora do player, qualquer comunicacao entre componentes deve passar por aqui.
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    bool _staggered;
    private PlayerMovement movement;
    [SerializeField] private InputReader input;

    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();

        input.OnMoveEvent += MoveInput;
        input.OnAttackEvent += AttackInput;
    }

    public void MoveInput(Vector2 dir)
    {
        if (!_staggered)
        {
            movement.Move(new Vector3(dir.x, 0, dir.y));
        } 
    }

    public void AttackInput()
    {
        if(!_staggered){
            movement.animator.SetBool("ataka", true);
        }
    }
}
