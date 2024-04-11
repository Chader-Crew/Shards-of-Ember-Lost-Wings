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
    private Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() 
    {
        Vector3 targetVelocity = moveVector * maxSpeed;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, acceleration);
    }

    public void Move(Vector3 velocity)
    {
        moveVector = velocity;
        if (velocity.magnitude == 0) { moveVector = Vector3.zero; }
    }
}