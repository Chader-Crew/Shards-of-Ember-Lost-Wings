using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    private Vector2 moveInput;
    private Vector3 moveDirection;


    public void OnMove(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context){
        if(context.started){
             // Calcula o vetor de dash baseado na direção de movimento atual e na velocidade máxima
            Vector3 dash = (transform.forward + moveDirection) * maxSpeed * Time.deltaTime;
            // Aplica o dash à posição do jogador
            transform.position += dash;
        }
    }

    public void Update(){
       movePlayer();
    }

    public void movePlayer(){

        if(moveInput.magnitude > 0){

        moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(moveDirection), 0.15f);

        }
    }
}