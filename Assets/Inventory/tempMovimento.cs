using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//eu juro que se alguem mexer nesse codigo que ta escrito TEMPORARIO eu mato
public class tempMovimento : MonoBehaviour
{
    float speed = 5f;

    void Update(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position = transform.position + new Vector3(h * speed * Time.deltaTime, 0f, v * speed * Time.deltaTime);
    }
}
