using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    bool _staggered;
    private PlayerMovement movement;
    [SerializeField] private InputReader input;
    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();

        input.OnMovePlayer += MoveInput;
    }

    public void MoveInput(Vector2 dir)
    {
        if (!_staggered)
        {
            movement.Move(new Vector3(dir.x, 0, dir.y));
        }
    }
}
