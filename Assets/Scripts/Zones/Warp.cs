using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Triggers para mudança de GameZone
public class Warp : MonoBehaviour
{
    [SerializeField] private GameZone zone;
    [SerializeField] private int destinationIndex;
    private bool _warping;
    private void OnEnable()
    {
        _warping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_warping)
        {
            GameZone.GoToZone(zone, destinationIndex);
            _warping = true;
        }
    }
}
