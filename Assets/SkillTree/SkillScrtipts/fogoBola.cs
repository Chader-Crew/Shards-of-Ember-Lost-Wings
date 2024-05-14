using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogoBola : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Enemy")
        {
            other.gameObject.GetComponent<CharStats>().hp-= 5;

        }
    }
}
