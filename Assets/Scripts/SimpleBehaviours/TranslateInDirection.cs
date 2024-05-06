using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateInDirection : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    private void Update() 
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
