using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetVel(Vector3 velocity)
    {
        rb.velocity = velocity;
    }
}