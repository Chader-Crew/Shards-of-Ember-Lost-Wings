using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainEffectController : MonoBehaviour
{
    
    void Update()
    {
        Shader.SetGlobalFloat("_PlayerHeight", PlayerController.Instance.transform.position.y);
    }
}
