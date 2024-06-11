using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//aplicacao de movimento no rigidbody do player.
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    public float currentVelocity;
    public float smoothTime = 0.05f;
    public float gravity = -9.81f;
    public float gravityMutiplier = 3.0f;
    public Vector3 movement;
    public float velocity;
    public bool _canMov;
    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

     void Update()
    {
        Movement();
        Rotation();
        Gravity();
    }

    private void Movement()
    {
        if(_canMov){return;}
        characterController.Move(movement * speed * Time.deltaTime);
    }
    private void Rotation()
    {
        if(movement.magnitude == 0) return;
        var targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void Gravity()
    {
        characterController.Move(velocity * Vector3.up);

        if(characterController.isGrounded && velocity < 0.0f)
        {
            velocity = 0.0f;
            
        }
        else
        {
            velocity += gravity * gravityMutiplier * Time.deltaTime;
        }
    }

    public void LockMovement(bool _lockMov)
    {
        _canMov = _lockMov;
    }
}
