using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    bool _staggered;
    private PlayerMovement movement;
    private void Awake() 
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void MoveInput(Vector2 dir)
    {
        if (!_staggered)
        {
            movement.SetVel(new Vector3(dir.x, 0, dir.y));
        }
    }
}
