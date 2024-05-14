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
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingSpeed;
    public LayerMask groundLayer;
    public float rayCastHeightOffSet = 0.5f;
    private bool _movementLocked;
    public bool isGrounded;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update(){
        Falling();
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

    public void Falling(){

        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if(!isGrounded){

            inAirTimer = inAirTimer + Time.deltaTime;
            rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
        }

        if(Physics.SphereCast(rayCastOrigin, 0.5f, -Vector3.up, out hit, groundLayer)){

            inAirTimer = 0;
            isGrounded = true;
        }
        else{

            isGrounded = false;
        }

    }
}