using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Skills/Spawn Prefab")]
public class SpawnPrefab : SkillBase
{
    [SerializeField]
    private GameObject prefab;

    [Header("spawna em owner por padrao.")]
    [Header("toggle pra spawnar em target[0].")]
    [SerializeField]
    private bool _onTarget;

    [Header("spawna em worldspace por padrao.")]
    [Header("toggle pra spawnar filho do transform.")]
    [SerializeField]
    private bool _localSpace;
    
    [SerializeField]
    private Vector3 offset;
    
    public override void Activate(SkillData context)
    {
        //instancia e posiciona
        GameObject instance = Instantiate(prefab);
        Transform origin = _onTarget? context.targets[0].transform : context.owner.transform;
        instance.transform.position = origin.position + 
                origin.right * offset.x+
                origin.up * offset.y+
                origin.forward * offset.z;
        //atribui parentesco se necessario
        if(_localSpace)
        {
            instance.transform.SetParent(origin);
        }
    }

    public override void Comprado(CharStats player)
    {
        throw new System.NotImplementedException();
    }
}
