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
    [SerializeField] private Transform player;
    [SerializeField] private float moveRotationSpeed = 5.0f;
    private Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        Vector3 targetVelocity = moveVector * maxSpeed;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, acceleration);
        
        if (moveVector.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, moveRotationSpeed * Time.deltaTime);
        }
    }

    public void Move(Vector3 vector)
    {
        moveVector = vector;
    }
}