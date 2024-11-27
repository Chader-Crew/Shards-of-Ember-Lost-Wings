using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMaze : MonoBehaviour
{

    public GameObject portals;
    void Activate()
    {
        portals.SetActive(true);
    }
}
