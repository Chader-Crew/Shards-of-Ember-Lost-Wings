using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TerrainEffectController : MonoBehaviour
{
    [SerializeField] private Color fogColor;
    private void OnEnable() {
       
        Shader.SetGlobalColor("_FogColor", fogColor);
    }
    void Update()
    {
        Shader.SetGlobalFloat("_PlayerHeight", PlayerController.Instance.transform.position.y);
    }
}
