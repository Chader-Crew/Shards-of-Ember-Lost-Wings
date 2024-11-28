using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    public bool _cantMov;
    public bool _dashing;
    public CharacterController characterController;
    
    private Ray ray;
    private Plane raycastPlane;
    private Camera cam;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

     void Update()
    {
        Movement();
        Rotation();
        Gravity();
    }

    private void Movement()
    {
        if(_cantMov){return;}
        characterController.Move(movement * speed * Time.deltaTime);
    }

    public void Dash(float speed, float time)
    {
        _dashing = true;
        movement *= speed;
        this.CallWithDelay(() =>
        {
            _dashing = false;
            movement /= speed;
        }, time);
    }
    
    private void Rotation()
    {
        if (_cantMov)
        {
            float xRatio = (float)Screen.width / cam.targetTexture.width;

            ray = cam.ScreenPointToRay(Input.mousePosition/xRatio);// Lança um ray da posição do mouse pra pegar o ponto de clique
            
            raycastPlane = new Plane(Vector3.up, transform.position);   //struct de plano na altura do player
            float rayDist;
            if (raycastPlane.Raycast(ray, out rayDist))
            {
                Vector3 targetDirection = ray.GetPoint(rayDist) - transform.position;
                targetDirection.y = 0;

                transform.rotation =
                    Quaternion.LookRotation(targetDirection); // Rotaciona o player na direção do ponto clicado
            }

            return;
        }
        
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
        _cantMov = _lockMov;
    }

    public void SetMovement(Vector3 direction)
    {
        if (_dashing) return;
        movement = direction;
    }
}
