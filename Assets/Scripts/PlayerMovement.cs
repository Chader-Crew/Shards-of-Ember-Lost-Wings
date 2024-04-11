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

    public void Move(Vector3 velocity)
    {
        rb.velocity = velocity;

        Vector3 movement = velocity * speed * Time.deltaTime;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity + movement, speed);
    }
}