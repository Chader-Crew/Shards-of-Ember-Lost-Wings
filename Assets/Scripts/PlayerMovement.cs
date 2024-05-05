using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//aplicacao de movimento no rigidbody do player.
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Vector3 moveVector;
    [SerializeField] private Transform model;
    [SerializeField] private float moveRotationSpeed = 5.0f;
    private Rigidbody rb;
    private bool _movementLocked;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        MovementUpdate();
    }

    //todos os calculos e atribuicoes de velocidade por frame.
    private void MovementUpdate()
    {
        if(_movementLocked){ return; }
        Vector3 targetVelocity = moveVector * maxSpeed;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, acceleration);
        if (moveVector.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, moveRotationSpeed * Time.deltaTime);
        }
    }

    public void Move(Vector3 vector)
    {
        moveVector = vector;
    }

    public void LockMovement(bool value)
    {
        _movementLocked = value;
        rb.velocity = value? Vector3.zero : rb.velocity;
    }
}